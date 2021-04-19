using System;
using System.IO;
using System.Reflection;
using System.Timers;
using System.Windows.Forms;

namespace SnipForMammal
{
    using Timer = System.Timers.Timer;
    public partial class SnipForMammal : Form
    {
        private delegate void SafeCallTextDelegate(string text);
        private delegate void SafeCallToolStripMenuDelegate(ToolStripMenuItemSpotifyTrack item);

        private SpotifyTrack CurrentPlayingTrack;
        private SpotifyTrack lastHistoryTrackAdded;

        private Timer updateCurrentTrackPlayingTimer;

        private readonly string snipFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SnipForMammal.txt");

        public SnipForMammal()
        {
            InitializeComponent();
            InitializeSnipFile(); 
            ConfigureUpdateCurrentTrackPlayingTimer();
        }

        private void InitializeSnipFile()
        {
            if (File.Exists(this.snipFilePath))
            {
                File.WriteAllText(this.snipFilePath, String.Empty);
            }
            else
            {
                File.Create(this.snipFilePath);
            }
        }

        private void WriteToSnipFile(string text)
        {
            try
            {
                Global.debugConsole?.WriteLine("Writting to Snip File: " + text);
                File.WriteAllText(this.snipFilePath, text);
            }
            catch (FileNotFoundException e)
            {
                Global.debugConsole?.WriteLine("FileNotFoundException Caught in WriteToSnipFile: " + e.FileName);
                Global.debugConsole?.WriteLine("Attempting to reinitialize Snip file...");
                InitializeSnipFile();
            }
        }

        private void ConfigureUpdateCurrentTrackPlayingTimer()
        {
            this.updateCurrentTrackPlayingTimer = new Timer(Global.updateCurrentTrackPlayingInterval);
            this.updateCurrentTrackPlayingTimer.AutoReset = true;
            this.updateCurrentTrackPlayingTimer.Elapsed += this.UpdateCurrentTrackPlayingTimer_Elapsed;
            this.updateCurrentTrackPlayingTimer.Enabled = true;
        }

        private void UpdateCurrentTrackPlayingTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Check if the stored current playing track is different than the current
            if (this.CurrentPlayingTrack != Global.spotify?.CurrentPlayingTrack)
            {
                this.CurrentPlayingTrack = Global.spotify.CurrentPlayingTrack;

                if (this.CurrentPlayingTrack == null)
                {
                    SetNotifyIconText("Snip For Mammal");
                    WriteToSnipFile(String.Empty);
                }
                else
                {
                    SetNotifyIconText(this.CurrentPlayingTrack.fullTitle);
                    AddTrackToHistory(this.CurrentPlayingTrack);
                    WriteToSnipFile(this.CurrentPlayingTrack.fullTitle);
                }
            }

        }

        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            // Close the application.
            Application.Exit();
        }

        private void toolStripMenuItem_ForceUpdate_Click(object sender, EventArgs e)
        {
            Global.spotify.ForceUpdate();
        }

        private void toolStripMenuItem_ShowDebug_Click(object sender, EventArgs e)
        {
            Global.debugConsole?.Show();
        }

        private void toolStripMenuItemSettings_Click(object sender, EventArgs e)
        {
            Global.settings?.Show();
        }

        private void AddTrackToHistory(SpotifyTrack track)
        {
            if (track == null || track.id == lastHistoryTrackAdded?.id)
            {
                return;
            }

            // Track history menu
            ToolStripMenuItemSpotifyTrack toolStripMenuItem = new ToolStripMenuItemSpotifyTrack
            {
                Size = new System.Drawing.Size(180, 22),
                Tag = track,
                Text = track.fullTitle,
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            // Copy track link menu
            ToolStripMenuItemSpotifyTrack toolStripMenuItem_CopyLink = new ToolStripMenuItemSpotifyTrack
            {
                Enabled = true,
                Name = "toolStripMenuItem_CopyLink",
                Size = new System.Drawing.Size(208, 22),
                Tag = track,
                Text = "Copy link",
                ToolTipText = "Copies Spotify link to your clipboard.",
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            toolStripMenuItem_CopyLink.MouseClick += new MouseEventHandler(TrackHistoryClick_CopyLink);

            // Copy track full title menu
            ToolStripMenuItemSpotifyTrack toolStripMenuItem_CopyFullTitle = new ToolStripMenuItemSpotifyTrack
            {
                Enabled = true,
                Name = "toolStripMenuItem_CopyFullTitle",
                Size = new System.Drawing.Size(208, 22),
                Tag = track,
                Text = "Copy full title",
                ToolTipText = "Copies song name and artists to your clipboard.",
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };
            toolStripMenuItem_CopyFullTitle.MouseClick += new MouseEventHandler(TrackHistoryClick_CopyFullTitle);

            // Add the "Copy track" menus to our Track History menu
            toolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                toolStripMenuItem_CopyLink,
                toolStripMenuItem_CopyFullTitle
            });


            // Add new Track History item to the list.
            Global.debugConsole?.WriteLine("Adding track to history: " + track.fullTitle);
            lastHistoryTrackAdded = track;
            AddDropDownItemsToMenu(toolStripMenuItem);
        }

        private void AddDropDownItemsToMenu(ToolStripMenuItemSpotifyTrack item)
        {
            if (contextMenuStrip.InvokeRequired)
            {
                var del = new SafeCallToolStripMenuDelegate(AddDropDownItemsToMenu);
                contextMenuStrip.Invoke(del, item);
            }
            else
            {
                this.toolStripMenuItem_TrackHistory.DropDownItems.Add(item);
            }
        }

        private void TrackHistoryClick_CopyLink(object sender, MouseEventArgs e)
        {
            // Copy the track's URI to clipboard. The SpotifyTrack is stored in the Tag property of the control.
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            SpotifyTrack track = (SpotifyTrack)toolStripMenuItem.Tag;

            if (!string.IsNullOrEmpty(track.uri))
            {
                Clipboard.SetText(track.uri);
                Global.debugConsole?.WriteLine("Copied track URI - " + track.uri);
            }
        }

        private void TrackHistoryClick_CopyFullTitle(object sender, MouseEventArgs e)
        {
            // Copy the track's full title to clipboard. The SpotifyTrack is stored in the Tag property of the control.
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
            SpotifyTrack track = (SpotifyTrack)toolStripMenuItem.Tag;

            if (!string.IsNullOrEmpty(track.fullTitle))
            {
                Clipboard.SetText(track.fullTitle);
                Global.debugConsole?.WriteLine("Copied track FullTitle - " + track.fullTitle);
            }
        }

        private void SetNotifyIconText(string text)
        {
            if (this.InvokeRequired)
            {
                var del = new SafeCallTextDelegate(SetNotifyIconText);
                this.Invoke(del, text);
            }
            else
            {
                // NotifyIcon.Text is limited to 64 char by a Windows API limitation, this gets around it using System.Reflection.
                if (text.Length >= 128)
                {
                    text = text.Substring(0, 127);
                }

                Type t = typeof(NotifyIcon);
                BindingFlags hidden = BindingFlags.NonPublic | BindingFlags.Instance;
                t.GetField("text", hidden).SetValue(this.notifyIcon, text);
                if ((bool)t.GetField("added", hidden).GetValue(this.notifyIcon))
                {
                    t.GetMethod("UpdateIcon", hidden).Invoke(this.notifyIcon, new object[] { true });
                }
            }
        }

        private void SnipForMammal_Load(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SnipForMammal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }

            InitializeSnipFile();
        }

        private void toolStripMenuItem_CustomText_Click(object sender, EventArgs e)
        {
            Global.customTextEntryForm.Show();
        }

    }



}
