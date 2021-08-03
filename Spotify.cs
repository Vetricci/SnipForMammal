using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Timers;

namespace SnipForMammal
{
    using SimpleJson;
    using System.Runtime.InteropServices;
    using Timer = System.Timers.Timer;

    public class Spotify
    {
        private readonly string spotifyAPIAuthURL = @"https://accounts.spotify.com/authorize";     // Spotify Auth API
        private readonly string spotifyAPICurrentlyPlayingURL = @"https://api.spotify.com/v1/me/player/currently-playing";
        private readonly string spotifyAPITokenURL = @"https://accounts.spotify.com/api/token";
        private readonly string localCallbackURL = @"http://localhost:8888/";                      // Callback address for Auth API. Must be registered on Spotify Dashboard.
        private readonly string scopes = "user-read-currently-playing";                           // Spotify API scopes.
        private readonly string responseType = "code";                                            // Required by Auth API.
        private readonly string spotify64BitKey = Convert.ToBase64String(Encoding.UTF8.GetBytes(ApplicationKeys.Spotify)); // Base64 auth keys "client_id:client_secret for auth"

        private string authorizationCode = string.Empty;        // Spotify Auth API Code. Provided by Auth API.
        private string authorizationToken = string.Empty;       // Spotify Auth API Token. Provided by Auth API.
        private double authorizationTokenExpiration = 0;        // Time until Spotify Auth API Token expires.
        private string refreshToken = string.Empty;             // Spotify Auth API Refresh Token.

        private Timer updateAuthToken;
        private Timer updateSpotifyTrackInfo;

        private bool spotifyRunning = false;
        private Process spotifyProcess;
        private IntPtr spotifyHandle = IntPtr.Zero;
        private string spotifyWindowTitle;

        private SpotifyTrack lastPlayedTrack;
        public SpotifyTrack LastPlayedTrack
        {
            get
            {
                return lastPlayedTrack;
            }
            set
            {
                this.lastPlayedTrack = value;
            }
        }

        private SpotifyTrack currentPlayingTrack;
        public SpotifyTrack CurrentPlayingTrack
        {
            get
            {
                return this.currentPlayingTrack;
            }
            set
            {
                this.currentPlayingTrack = value;
            }
        }


        public Spotify()
        {
            // Authorize SnipForMammal through Spotify API
            AuthorizeSnipForMammal();

            // Configure timer to update auth token
            ConfigureUpdateAuthTokenTimer();

            // Configure timer to update current playing track
            ConfigureUpdateCurrentPlayingTrackTimer();
        }

        private void ConfigureUpdateAuthTokenTimer()
        {
            this.updateAuthToken = new Timer(Global.updateAuthTokenInterval);
            this.updateAuthToken.Elapsed += this.UpdateAuthToken_Elapsed;
            this.updateAuthToken.AutoReset = true;
            this.updateAuthToken.Enabled = true;
            this.UpdateAuthToken_Elapsed(null, null); // Get initial token
            Global.debugConsole?.WriteLine("Timer updateAuthToken started. Interval " + Global.updateAuthTokenInterval + "ms");
        }

        private void ConfigureUpdateCurrentPlayingTrackTimer()
        {
            this.updateSpotifyTrackInfo = new Timer(Global.updateSpotifyTrackInfoInterval);
            this.updateSpotifyTrackInfo.Elapsed += this.UpdateSpotifyTrackInformation_Elapsed;
            this.updateSpotifyTrackInfo.AutoReset = true;
            this.updateSpotifyTrackInfo.Enabled = true;
            Global.debugConsole?.WriteLine("Timer updateSpotifyTrackInformation started. Interval " + Global.updateSpotifyTrackInfoInterval + "ms");
        }

        private void UpdateAuthToken_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Check for interval change.
            if (updateAuthToken.Interval != Global.updateAuthTokenInterval)
            {
                updateAuthToken.Interval = Global.updateAuthTokenInterval;
                Global.debugConsole?.WriteLine("Interval for timer updateAuthToken updated to " + Global.updateAuthTokenInterval + "ms");
            }

            string downloadedJson = string.Empty;
            if (!string.IsNullOrEmpty(this.refreshToken))
            {
                Console.WriteLine("SpotifyAddressContactType.AuthorizationRefresh");
                downloadedJson = this.DownloadJson(spotifyAPITokenURL, SpotifyAddressContactType.AuthorizationRefresh);
            }
            else
            {
                Console.WriteLine("SpotifyAddressContactType.Authorization");
                downloadedJson = this.DownloadJson(spotifyAPITokenURL, SpotifyAddressContactType.Authorization);
            }

            Console.WriteLine(downloadedJson);

            // Set the token to be blank until filled
            this.authorizationToken = string.Empty;
            this.authorizationTokenExpiration = 0;

            if (!string.IsNullOrEmpty(downloadedJson))
            {
                dynamic jsonSummary = SimpleJson.DeserializeObject(downloadedJson);

                if (jsonSummary != null)
                {
                    this.authorizationToken = jsonSummary.access_token.ToString();
                    this.authorizationTokenExpiration = Convert.ToDouble((long)jsonSummary.expires_in);
                    if (jsonSummary.refresh_token != null)
                    {
                        this.refreshToken = jsonSummary.refresh_token.ToString();
                    }    

                    this.updateAuthToken.Interval = this.authorizationTokenExpiration * 1000.0;

                    // Debug output
                    Global.debugConsole?.WriteLine("authToken: " + this.authorizationToken);
                    Global.debugConsole?.WriteLine("authTokenExpiration: " + this.authorizationTokenExpiration);
                    Global.debugConsole?.WriteLine("refreshToken: " + this.refreshToken);
                }
            }
            else
            {
                Global.debugConsole?.WriteLine("Failed to obtain Json from Spotify Token API.");
                DisableAllTimers();
            }

            if (authorizationTokenExpiration > 0)
            {
                Global.debugConsole?.WriteLine("Auth token updated.");
            }
        }

        private void UpdateSpotifyTrackInformation_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Check for interval change
            if (updateSpotifyTrackInfo.Interval != Global.updateSpotifyTrackInfoInterval)
            {
                updateSpotifyTrackInfo.Interval = Global.updateSpotifyTrackInfoInterval;
                Global.debugConsole?.WriteLine("Interval for timer updateSpotifyTrackInfo updated to " + Global.updateSpotifyTrackInfoInterval + "ms");
            }

            // Ensure Spotify is running otherwise reset.
            CheckForSpotifyInstance();
            if (this.spotifyRunning)
            {
                // Get Currently Playing JSON from API.
                string downloadedJson = this.DownloadJson(this.spotifyAPICurrentlyPlayingURL, SpotifyAddressContactType.API);

                if (!string.IsNullOrEmpty(downloadedJson))
                {
                    // Parse Json
                    dynamic jsonSummary = SimpleJson.DeserializeObject(downloadedJson);

                    // Is track currently playing?
                    if ((bool)jsonSummary.is_playing)
                    {
                        // Is this a new track?
                        SpotifyTrack newTrack = new SpotifyTrack(jsonSummary);
                        if (newTrack.ToString() != currentPlayingTrack?.ToString())
                        {
                            // Update last played track
                            Global.IsTextOverriden = false;
                            this.lastPlayedTrack = this.currentPlayingTrack;
                            this.currentPlayingTrack = newTrack;

                            //ResetUpdateAuthTokenTimer();
                            Global.debugConsole?.WriteLine("Now playing: " + currentPlayingTrack.ToString());
                        }
                        else
                        {
                            RequestReset(ResetReason.NotNewSong);
                        }
                    }
                    else
                    {
                        RequestReset(ResetReason.SpotifyPaused);
                    }
                }
                else
                {
                    RequestReset(ResetReason.InvalidJson);
                }
            }
            else
            {
                RequestReset(ResetReason.SpotifyNotLaunched);
            }
        }

        private void ResetUpdateAuthTokenTimer()
        {
            Global.debugConsole?.WriteLine("Resetting timer updateAuthToken.");
            this.updateAuthToken.Enabled = false;
            this.updateAuthToken.Interval = Global.updateAuthTokenInterval;
            this.updateAuthToken.Enabled = true;
        }

        private void ResetUpdateSpotifyTrackInformationTimer()
        {
            Global.debugConsole?.WriteLine("Resetting timer updateSpotifyTrackInformation.");
            this.updateSpotifyTrackInfo.Enabled = false;
            this.updateSpotifyTrackInfo.Interval = Global.updateSpotifyTrackInfoInterval;
            this.updateSpotifyTrackInfo.Enabled = true;
        }

        private void DisableAllTimers()
        {
            updateAuthToken.Enabled = false;
            updateSpotifyTrackInfo.Enabled = false;
            Global.debugConsole?.WriteLine("Disabling all Snip timers");
        }

        private bool IsSpotifyWindowDefaultText()
        {
            // Checks if Spotify's Window Title is either the last track played or contains the default text indicating not playing

            bool isWindowTitleLastTrackOrDefault = false;

            if ((this.spotifyWindowTitle.Length > 0) && (this.spotifyWindowTitle.Contains("Spotify")))
            {
                isWindowTitleLastTrackOrDefault = true;
            }

            return isWindowTitleLastTrackOrDefault;
        }

        private void CheckForSpotifyInstance()
        {
            // Fetch all Spotify processes
            Process[] allRunningProcesses = Process.GetProcessesByName("Spotify");

            if (allRunningProcesses.Length > 0)
            {
                foreach (Process proc in allRunningProcesses)
                {
                    // Check processes for the one with text in the window title. There are multiple Spotify processes but only one will have a non-empty window title.
                    if (proc.MainWindowTitle.Length > 0)
                    {
                        this.spotifyRunning = true;
                        this.spotifyProcess = proc;
                        this.spotifyHandle = proc.MainWindowHandle;

                        StringBuilder sb = new StringBuilder(256);
                        GetWindowText(this.spotifyHandle, sb, sb.Capacity);
                        this.spotifyWindowTitle = sb.ToString();
                    }
                }
            }
            else
            {
                this.spotifyRunning = false;
                this.spotifyProcess = null;
                this.spotifyHandle = IntPtr.Zero;
                this.spotifyWindowTitle = string.Empty;
            }
; }

        private Process OpenURL(string url)
        {
            string browser = Global.browser.ToLower();
            Process process;

            switch (browser)
            {
                case "chrome":
                    try
                    {
                        process = Process.Start("chrome", url + " --new-window");
                    }
                    catch
                    {
                        goto default;
                    }
                    break;
                case "firefox":
                    try
                    {
                        process = Process.Start("firefox", "-new-window " + url);
                    }
                    catch
                    {
                        goto default;
                    }
                    break;
                case "opera":
                    try
                    {
                        process = Process.Start("opera", "--new-window " + url);
                    }
                    catch
                    {
                        goto default;
                    }
                    break;
                case "edge":
                    try
                    {
                        process = Process.Start("msedge", "--new-window " + url);
                    }
                    catch
                    {
                        goto default;
                    }
                    break;
                default:
                    process = Process.Start(url);
                    break;
            }

            return process;
        }
        
        private void AuthorizeSnipForMammal()
        {
            try
            {
                // Start up Process to begin authorization with Spotify API
                string authURL = string.Format(CultureInfo.InvariantCulture, "{0}?client_id={1}&response_type={2}&redirect_uri={3}&scope={4}", this.spotifyAPIAuthURL, ApplicationKeys.client, this.responseType, this.localCallbackURL, this.scopes);
                Process authorizationProcess = OpenURL(authURL);

                using (HttpListener callbackListener = new HttpListener())
                {
                    HttpListenerTimeoutManager timeoutManager = callbackListener.TimeoutManager;
                    timeoutManager.IdleConnection = new TimeSpan(0, 0, 5); // hr, min, s
                    timeoutManager.HeaderWait = new TimeSpan(0, 0, 5);

                    callbackListener.Prefixes.Add(this.localCallbackURL);
                    callbackListener.Start();

                    HttpListenerContext context = callbackListener.GetContext();
                    HttpListenerRequest request = context.Request;
                    HttpListenerResponse response = context.Response;
                    NameValueCollection nameValueCollection = request.QueryString;

                    string spotifyAPIAccessGranted = "Access granted. You may close this window now.";
                    string spotifyAPIAccessDenied = "Access denied. In order to use this application you must grant it access.";
                    string callbackHtmlStart = @"<!doctype html><html lang=en><head><meta charset=utf-8><style>div { font-size:xx-large; }</style><title>SnipForMammal Auth</title></head><body><div>";
                    string callbackHtmlEnd = @"</div></body></html>";
                    string callbackCloseWindowScript = @"<script>window.close();</script>";

                    string outputString = string.Empty;

                    foreach (string keyValue in nameValueCollection.AllKeys)
                    {
                        switch (keyValue.ToLower())
                        {
                            case "code":
                                if (Global.autoCloseAuthWindow)
                                {
                                    outputString = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}{3}", callbackHtmlStart, callbackCloseWindowScript, spotifyAPIAccessGranted, callbackHtmlEnd);

                                }
                                else
                                {
                                    outputString = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", callbackHtmlStart, spotifyAPIAccessGranted, callbackHtmlEnd);

                                }
                                this.authorizationCode = nameValueCollection[keyValue];
                                Global.debugConsole?.WriteLine("Successfully authorized through Spotify Auth API.");
                                break;
                            case "error":
                                outputString = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", callbackHtmlStart, spotifyAPIAccessDenied, callbackHtmlEnd);
                                Global.debugConsole?.WriteLine("Failed authorizing through Spotify Auth API.");
                                break;
                            default:
                                break;
                        }
                    }

                    byte[] buffer = Encoding.UTF8.GetBytes(outputString);
                    response.ContentLength64 = buffer.Length;
                    Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();

                    callbackListener.Stop();
                }
            }
            catch (Exception e)
            {
                Global.debugConsole?.WriteLine(e.ToString());
            }
        }

        private string DownloadJson(string jsonAddress, SpotifyAddressContactType spotifyAddressContactType)
        {
            using (WebClientWithShortTimeout jsonWebClient = new WebClientWithShortTimeout())
            {
                try
                {
                    // Auth uses POST instead of GET
                    bool usePostMethodInsteadOfGet = false;
                    string postParameters = string.Empty;

                    // Modify HTTP headers based on what's being contacted
                    switch (spotifyAddressContactType)
                    {
                        case SpotifyAddressContactType.Authorization:
                            Global.debugConsole?.WriteLine("Downloading authorization json.");
                            usePostMethodInsteadOfGet = true;
                            postParameters = string.Format(CultureInfo.InvariantCulture, "grant_type=authorization_code&code={0}&redirect_uri={1}", this.authorizationCode, this.localCallbackURL);
                            jsonWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                            jsonWebClient.Headers.Add("Authorization", string.Format(CultureInfo.InvariantCulture, "Basic {0}", this.spotify64BitKey));
                            break;

                        case SpotifyAddressContactType.AuthorizationRefresh:
                            Global.debugConsole?.WriteLine("Downloading refresh authorization json.");
                            usePostMethodInsteadOfGet = true;
                            postParameters = string.Format(CultureInfo.InvariantCulture, "grant_type=refresh_token&refresh_token={0}", this.refreshToken);
                            jsonWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                            jsonWebClient.Headers.Add("Authorization", string.Format(CultureInfo.InvariantCulture, "Basic {0}", this.spotify64BitKey));
                            break;

                        case SpotifyAddressContactType.API:
                            jsonWebClient.Headers.Add("Authorization", string.Format(CultureInfo.InvariantCulture, "Bearer {0}", this.authorizationToken));
                            break;

                        default:
                            break;
                    }

                    jsonWebClient.Headers.Add("User-Agent", "SnipForMammal/v1");
                    jsonWebClient.Encoding = Encoding.UTF8;

                    string downloadedJson = string.Empty;
                    if (usePostMethodInsteadOfGet)
                    {
                        downloadedJson = jsonWebClient.UploadString(jsonAddress, "POST", postParameters);
                    }
                    else
                    {
                        downloadedJson = jsonWebClient.DownloadString(jsonAddress);
                    }

                    if (downloadedJson.Length > 0 & downloadedJson.Length < 1000)
                    {
                        Global.debugConsole?.WriteLine(downloadedJson);
                    }
                    return downloadedJson;
                }
                catch (WebException webException)
                {
                    Global.debugConsole?.WriteLine("WebException thrown in Spotify.DownloadJson()");
                    Global.debugConsole?.WriteLine("     Exception Message: " + webException.Message);

                    // Catch "(503) Server Unavailable"

                    WebResponse webResponse = webException.Response;
                    WebHeaderCollection webHeaderCollection = webResponse.Headers;

                    for (int i = 0; i < webHeaderCollection.Count; i++)
                    {
                        if (webHeaderCollection.GetKey(i).ToUpperInvariant() == "RETRY-AFTER")
                        {
                            // Set the timer to the retry seconds. Plus 1 for safety.
                            this.updateSpotifyTrackInfo.Enabled = false;
                            this.updateSpotifyTrackInfo.Interval = (Double.Parse(webHeaderCollection.Get(i) + 2, CultureInfo.InvariantCulture)) * 1000;
                            this.updateSpotifyTrackInfo.Enabled = true;
                        }
                    }

                    return string.Empty;
                }
                catch (Exception e)
                {
                    Global.debugConsole?.WriteLine("Generic exception thrown in DownloadJson()");
                    Global.debugConsole?.WriteLine("     Exception Message: " + e.Message);
                    return string.Empty;
                }
            }
        }

        private void RequestReset(ResetReason resetReason)
        {
            switch (resetReason)
            {
                case ResetReason.SpotifyNotLaunched:
                    Global.debugConsole?.WriteLine("Reset requested. Reason: Spotify is not launched.");
                    this.spotifyRunning = false;
                    this.spotifyHandle = IntPtr.Zero;
                    break;

                case ResetReason.SpotifyPaused:
                    Global.debugConsole?.WriteLine("Reset requested. Reason: Spotify is paused.");
                    this.spotifyRunning = false;
                    this.spotifyHandle = IntPtr.Zero;
                    if (!Global.IsTextOverriden) { this.CurrentPlayingTrack = null; }
                    break;

                case ResetReason.NotNewSong:
                    Global.debugConsole?.WriteLine("Reset requested. Reason: Current song playing is not new.");
                    this.spotifyRunning = false;
                    this.spotifyHandle = IntPtr.Zero;
                    break;

                case ResetReason.InvalidJson:
                    Global.debugConsole?.WriteLine("Reset requested. Reason: Invalid Json.");
                    this.spotifyRunning = false;
                    this.spotifyHandle = IntPtr.Zero;
                    break;

                case ResetReason.ForceUpdateRequested:
                    Global.debugConsole?.WriteLine("Reset requested. Reason: Force update requested by user.");
                    this.spotifyRunning = false;
                    this.spotifyHandle = IntPtr.Zero;
                    ResetUpdateSpotifyTrackInformationTimer();
                    break;
                default:
                    break;
            }
        }

        public void ForceUpdate()
        {
            RequestReset(ResetReason.ForceUpdateRequested);
        }

        public void CustomTrack(string text)
        {
            Global.debugConsole.WriteLine("Setting custom track: " + text);
            this.CurrentPlayingTrack = new SpotifyTrack(text);
        }

        #region Extern

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int GetWindowText([In] IntPtr windowHandle, [Out] StringBuilder windowText, [In] int maxCount);

        #endregion Extern

        #region Enums

        private enum SpotifyAddressContactType
        {
            Authorization,
            AuthorizationRefresh,
            API,
            Default
        }

        private enum ResetReason
        {
            SpotifyNotLaunched,
            SpotifyPaused,
            NotNewSong,
            ForceUpdateRequested,
            InvalidJson
        }

        #endregion Enums
    }
}

