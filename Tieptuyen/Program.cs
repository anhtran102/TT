using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tieptuyen.App_Start;

namespace Tieptuyen
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            UnityWebActivator.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);          
            Application.Run(new MainForm());             
        }
    }

}
