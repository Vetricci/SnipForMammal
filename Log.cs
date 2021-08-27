using System;
using System.IO;
using System.Windows.Forms;

namespace SnipForMammal
{
    public class Log
    {
        string logFile = Path.Combine(Path.GetTempPath(), "SnipForMammalLog.txt");

        public Log()
        {
            WriteLine("Initializing...");
            WriteLine(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss:fff"));
            WriteLine("Version " + Application.ProductVersion);
            WriteLine("Log file location: " + logFile);

            // Clear out log file.
            File.WriteAllText(this.logFile, string.Empty);
        }

        public void WriteLine(string text)
        {
            string message = "[" + DateTime.Now.ToString("HH:mm:ss:fff") + "] " + text;
            using (StreamWriter sw = File.AppendText(logFile))
            {
                sw.WriteLine(message);
            }
            Console.WriteLine(message);
        }
    }
}
