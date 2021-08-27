using System;
using System.Diagnostics;
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
            Process[] allProcesses = Process.GetProcesses();
            int numSnipInstancesRunning = 0;
            foreach (Process process in allProcesses)
            {
                if (process.ProcessName.Contains("SnipForMammal"))
                {
                    numSnipInstancesRunning++;
                }
            }
            Console.WriteLine("Snip Instances Running: " + numSnipInstancesRunning);

            if (numSnipInstancesRunning < 2)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Globals
                Global.log = new Log();
                Global.lastTrack = null;
                Global.currentTrack = null;
                Global.outputFormat = Properties.Settings.Default.outputFormat;
                Global.updateAuthTokenInterval = Properties.Settings.Default.updateAuthTokenInterval;
                Global.updateSpotifyTrackInfoInterval = Properties.Settings.Default.updateSpotifyTrackInfoInterval;
                Global.updateCurrentTrackPlayingInterval = Properties.Settings.Default.updateCurrentTrackPlayingInterval;
                Global.autoCloseAuthWindow = Properties.Settings.Default.autoCloseAuthWindow;
                Global.browser = Properties.Settings.Default.browser;
                Global.settings = new Settings();
                Global.customTextEntryForm = new CustomTextEntryForm();
                Global.spotify = new Spotify();
                //Global.snipForMammal = new SnipForMammal(); // Keep this line out of Application.Run() to ensure the form never shows and instantly minimizes.

                // Run
                Application.Run(new SnipForMammal());
            }
            else
            {
                MessageBox.Show("Error: There is already an instance of SnipForMammal.exe running.");
                Application.Exit();
            }



        }
    }

}
