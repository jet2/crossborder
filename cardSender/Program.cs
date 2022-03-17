using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cardSender
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [STAThread]
        static void Main()
        {
           // Application.SetHighDpiMode(HighDpiMode.SystemAware);

            var needToCreateNew = true;
            using (Mutex xMutex = new Mutex(true, ThisNamespace(), out needToCreateNew)) ;

            if (needToCreateNew)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new Form1());
            }
            else
            {
                var current = Process.GetCurrentProcess();

                foreach (var process in Process.GetProcessesByName(current.ProcessName))
                {
                    if (process.Id == current.Id) continue;
                    SetForegroundWindow(process.MainWindowHandle);
                    break;
                }

            }
        }
        public static string ThisNamespace()
        {
            var myType = typeof(Program);
            return myType.Namespace;
        }
    }
}
