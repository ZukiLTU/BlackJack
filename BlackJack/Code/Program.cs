using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string dir = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\\logs\\";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory($"{dir}");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File($"{dir}\\{DateTime.Now.ToShortDateString()}.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Application.Run(new FormBlackJack());            
        }
    }
}
