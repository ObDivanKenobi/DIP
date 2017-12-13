using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeSmells
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CreateDirectories();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
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
