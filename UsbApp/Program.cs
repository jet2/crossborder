using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UsbApp
{
    static class Program
    {
        private static string appGuid = "19816719-201B-4172-92DA-E0478F991E02";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (System.Threading.Mutex mutex = new System.Threading.Mutex(false, "Global\\" + appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Приложение уже запущено!");
                    return;
                }
            }
                Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new XForm1());
        }
    }
}


