using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.IO;
using iLostMouse;

namespace iLostMouse_Windows
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            createConfigIfNotExists();
            Application.Run(new Form1());
        }

        private static void createConfigIfNotExists()
        {
            string configFile = string.Format("{0}.config", Application.ExecutablePath);

            if (!File.Exists(configFile))
            {
                File.WriteAllText(configFile, Properties.Resources.App);
            }
        }
    }
}
