using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeOk.Models;

namespace CodeOk
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CreateDirectories();
            Application.Run(new LoginForm());
        }

        static void CreateDirectories()
        {
            foreach (var folder in ConfigurationManager.AppSettings.GetValues("UserStorage"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(folder));
            }
        }
    }
}
