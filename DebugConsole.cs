using System;
using System.IO;
using System.Windows.Forms;

namespace SnipForMammal
{
    public partial class DebugConsole : Form
    {
        string logFile = Path.Combine(Path.GetTempPath(), "SnipForMammalLog.txt");

        public DebugConsole()
        {
            InitializeComponent();
            WriteLine("Initializing...");
            WriteLine("Version " + Application.ProductVersion);

            // Prepare log file
            WriteLine("Preparing log file...");
            WriteLine("Log file location: " + logFile);
            File.WriteAllText(logFile, string.Empty);
        }

        private delegate void SafeCallTextDelegate(string text);
        public void WriteLine(string text)
        {
            if (ConsoleTextBox.InvokeRequired)
            {
                var del = new SafeCallTextDelegate(WriteLine);
                ConsoleTextBox.Invoke(del, text);
            }
            else
            {
                string message = "[" + GetTimestamp() + "] " + text;
                this.ConsoleTextBox.AppendText(message + Environment.NewLine);
                using (StreamWriter sw = File.AppendText(logFile))
                {
                    sw.WriteLine(message);
                }
                Console.WriteLine(message);
            }
        }


        private string GetTimestamp()
        {
            return DateTime.Now.ToString("HH:mm:ss:fff");
        }

        private void DebugConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

    }
}
