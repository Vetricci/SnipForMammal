using System;
using System.Windows.Forms;

namespace SnipForMammal
{
    public partial class DebugConsole : Form
    {
        public DebugConsole()
        {
            InitializeComponent();
            WriteLine("Initializing...");
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
                this.ConsoleTextBox.AppendText("[" + GetTimestamp() + "] " + text + Environment.NewLine);
                Console.WriteLine(text);
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
