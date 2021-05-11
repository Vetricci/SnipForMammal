using System;
using System.IO;
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

        private void ToolStripMenuItem_FileClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ToolStripMenuItem_FileOutput_Click(object sender, EventArgs e)
        {
            string timestamp = DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss");
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DebugConsoleOutput " + timestamp + ".txt");

            File.WriteAllText(filePath, this.ConsoleTextBox.Text);
        }
    }
}
