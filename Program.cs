using System;
using System.Windows.Forms;

namespace SnipForMammal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Run only if no other instance is already running.
            //System.Diagnostics.Process[] processSnipForMammalInstances = System.Diagnostics.Process.GetProcessesByName("SnipForMammal");

            if (System.Diagnostics.Process.GetProcessesByName("SnipForMammal").Length <= 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Globals
                Global.outputFormat = Properties.Settings.Default.outputFormat;
                Global.updateAuthTokenInterval = Properties.Settings.Default.updateAuthTokenInterval;
                Global.updateSpotifyTrackInfoInterval = Properties.Settings.Default.updateSpotifyTrackInfoInterval;
                Global.updateCurrentTrackPlayingInterval = Properties.Settings.Default.updateCurrentTrackPlayingInterval;
                Global.autoCloseAuthWindow = Properties.Settings.Default.autoCloseAuthWindow;
                Global.browser = Properties.Settings.Default.browser;
                Global.debugConsole = new DebugConsole();
                Global.settings = new Settings();
                Global.customTextEntryForm = new CustomTextEntryForm();
                Global.spotify = new Spotify();
                Global.snipForMammal = new SnipForMammal(); // Keep this line out of Application.Run() to ensure the form never shows and instantly minimizes.

                // Run
                Application.Run();
            }
            else
            {
                MessageBox.Show("Error: There is already an instance of SnipForMammal.exe running.");
                Application.Exit();
            }



        }
    }

}
