using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using BenchmarkDotNet.Running;
using AADL.People;
using AADL.Users;
using AADL.Regulators;
using AADL.Lists;
namespace AADL
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
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            Console.WriteLine(connectionString);
            Application.Run(new frmLaunch());

        }
    }
}
