using System;
using System.Windows.Forms;

namespace SnipForMammal
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            textBoxSettingsOutputFormat.Text = Global.outputFormat;
            textBoxSettingsInternalAPIUpdateInterval.Text = Global.updateCurrentTrackPlayingInterval.ToString();
            textBoxSettingsSpotifyAPIUpdateInterval.Text = Global.updateSpotifyTrackInfoInterval.ToString();
            textBoxSettingsSpotifyAuthAPIUpdateInterval.Text = Global.updateAuthTokenInterval.ToString();
            comboBox_AutoCloseAuthWindow.Text = Global.autoCloseAuthWindow.ToString();
            comboBox_defaultBrowser.Text = Global.browser;
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            bool success;
            int newInt;
            bool newBool;

            // OutputFormat
            if (textBoxSettingsOutputFormat.Text != Properties.Settings.Default.outputFormat)
            {
                Global.outputFormat = textBoxSettingsOutputFormat.Text;
                Properties.Settings.Default.outputFormat = textBoxSettingsOutputFormat.Text;
                Global.debugConsole?.WriteLine("Setting \"OutputFormat\" new value: " + Global.outputFormat);
            }

            // InternalAPIUpdateInterval
            success = int.TryParse(textBoxSettingsInternalAPIUpdateInterval.Text, out newInt);
            if (success && (newInt != Properties.Settings.Default.updateCurrentTrackPlayingInterval))
            {
                Global.updateCurrentTrackPlayingInterval = newInt;
                Properties.Settings.Default.updateCurrentTrackPlayingInterval = newInt;
                Global.debugConsole?.WriteLine("Setting \"InternalAPIUpdateInterval\" new value: " + Global.updateCurrentTrackPlayingInterval);
            }

            // SpotifyAPIUpdateInterval
            success = int.TryParse(textBoxSettingsSpotifyAPIUpdateInterval.Text, out newInt);
            if (success && (newInt != Properties.Settings.Default.updateSpotifyTrackInfoInterval))
            {
                Global.updateSpotifyTrackInfoInterval = newInt;
                Properties.Settings.Default.updateSpotifyTrackInfoInterval = newInt;
                Global.debugConsole?.WriteLine("Setting \"SpotifyAPIUpdateInterval\" new value: " + Global.updateSpotifyTrackInfoInterval);
            }

            // SpotifyAuthAPIUpdateInterval
            success = int.TryParse(textBoxSettingsSpotifyAuthAPIUpdateInterval.Text, out newInt);
            if (success && (newInt != Properties.Settings.Default.updateAuthTokenInterval))
            {
                Global.updateAuthTokenInterval = newInt;
                Properties.Settings.Default.updateAuthTokenInterval = newInt;
                Global.debugConsole?.WriteLine("Setting \"SpotifyAuthAPIUpdateInterval\" new value: " + Global.updateAuthTokenInterval);
            }

            // AutoCloseAuthWindow
            success = bool.TryParse(comboBox_AutoCloseAuthWindow.Text, out newBool);
            if (success && (newBool != Properties.Settings.Default.autoCloseAuthWindow))
            {
                Global.autoCloseAuthWindow = newBool;
                Properties.Settings.Default.autoCloseAuthWindow = newBool;
                Global.debugConsole?.WriteLine("Setting \"AutoCloseAuthWindow\" new value: " + Global.autoCloseAuthWindow);
            }

            // DefaultBrowser
            if (comboBox_defaultBrowser.Text != Properties.Settings.Default.browser)
            {
                Global.browser = comboBox_defaultBrowser.Text;
                Properties.Settings.Default.browser = comboBox_defaultBrowser.Text;
                Global.debugConsole?.WriteLine("Setting \"DefaultBrowser\" new value: " + Global.browser);
            }

            // Save everything
            Properties.Settings.Default.Save();

            // Hide form after button press
            this.Hide();
        }

        private void buttonRestoreDefaults_Click(object sender, EventArgs e)
        {
            Global.debugConsole?.WriteLine("Restoring default settings.");

            // OutputFormat
            textBoxSettingsOutputFormat.Text = Properties.Settings.Default.defaultOutputFormat;
            Global.outputFormat = textBoxSettingsOutputFormat.Text;
            Properties.Settings.Default.outputFormat = textBoxSettingsOutputFormat.Text;

            // InternalAPIUpdateInterval
            textBoxSettingsInternalAPIUpdateInterval.Text = Properties.Settings.Default.defaultUpdateCurrentTrackPlayingInterval.ToString();
            Global.updateCurrentTrackPlayingInterval = Properties.Settings.Default.defaultUpdateCurrentTrackPlayingInterval;
            Properties.Settings.Default.updateCurrentTrackPlayingInterval = Properties.Settings.Default.defaultUpdateCurrentTrackPlayingInterval;

            // SpotifyAPIUpdateInterval
            textBoxSettingsSpotifyAPIUpdateInterval.Text = Properties.Settings.Default.defaultUpdateSpotifyTrackInfoInterval.ToString();
            Global.updateSpotifyTrackInfoInterval = Properties.Settings.Default.defaultUpdateSpotifyTrackInfoInterval;
            Properties.Settings.Default.updateSpotifyTrackInfoInterval = Properties.Settings.Default.defaultUpdateSpotifyTrackInfoInterval;

            // SettingsSpotifyAuthAPIUpdateInterval
            textBoxSettingsSpotifyAuthAPIUpdateInterval.Text = Properties.Settings.Default.defaultUpdateAuthTokenInterval.ToString();
            Global.updateAuthTokenInterval = Properties.Settings.Default.defaultUpdateAuthTokenInterval;
            Properties.Settings.Default.updateAuthTokenInterval = Properties.Settings.Default.defaultUpdateAuthTokenInterval;

            // AutoCloseAuthWindow
            comboBox_AutoCloseAuthWindow.Text = Properties.Settings.Default.defaultAutoCloseAuthWindow.ToString();
            Global.autoCloseAuthWindow = Properties.Settings.Default.defaultAutoCloseAuthWindow;
            Properties.Settings.Default.autoCloseAuthWindow = Properties.Settings.Default.defaultAutoCloseAuthWindow;

            // DefaultBrowser
            comboBox_defaultBrowser.Text = Properties.Settings.Default.defaultBrowser;
            Global.browser = Properties.Settings.Default.defaultBrowser;
            Properties.Settings.Default.browser = Properties.Settings.Default.defaultBrowser;

            // Save everything
            Properties.Settings.Default.Save();
        }
    }
}
