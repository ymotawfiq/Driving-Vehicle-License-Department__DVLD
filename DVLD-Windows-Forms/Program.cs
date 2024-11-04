using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Windows_Forms
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Login())
            {
                Application.Run(new frmMain());
            }
            
        }

        static bool Login()
        {
            frmLoginScreen loginForm;
            string filePath = @"D:\My Code And Projects\Programming Advices Roadmap\course 19\DVLD-Solution\DVLD-Windows-Forms\RememberMe.txt";
            string[] line = new string[6];
            try
            {
                line = File.ReadAllLines(filePath)[0].Split(',');
            }

            catch (Exception) { }

            if (line != null && line[0] != "")
            {
                loginForm = new frmLoginScreen(line[2], line[3]);
                Application.Run(loginForm);
            }
            else
            {
                loginForm = new frmLoginScreen();
                Application.Run(loginForm);
            }
            return loginForm.IsLoggedIn;
        }

    }
}
