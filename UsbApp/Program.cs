using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace kppApp
{
    
    static class Program
    {
        private enum WindowShowStyle : uint
        {
            Hide = 0,
            ShowNormal = 1,
            ShowMinimized = 2,
            ShowMaximized = 3,
            Maximize = 3,
            ShowNormalNoActivate = 4,
            Show = 5,
            Minimize = 6,
            ShowMinNoActivate = 7,
            ShowNoActivate = 8,
            Restore = 9,
            ShowDefault = 10,
            ForceMinimized = 11
        }
        private static string appGuid = "19816719-201B-4172-92DA-E0478F991E02";

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>

        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    var current = Process.GetCurrentProcess();

                    MessageBox.Show("Приложение " + current.ProcessName + " уже запущено!");

                    foreach (var process in Process.GetProcessesByName(current.ProcessName))
                    {
                        if (process.Id == current.Id) continue;

                        IntPtr handle = process.MainWindowHandle;
                        if (handle != IntPtr.Zero)
                        {
                            ShowWindow(handle, WindowShowStyle.Restore);
                            SetForegroundWindow(handle);
                        }
                        break;
                    }
                }
                else
                {
                    Application.EnableVisualStyles();
                    //Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainFormKPP());
                }
            }
        }
    }
}


