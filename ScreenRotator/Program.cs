using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenRotator
{
    static class Program
    {
        static Mutex mutex = new Mutex(false, "liebherr.com-screenrotation");

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Only single instance of application is allowed
            if (!mutex.WaitOne(TimeSpan.FromSeconds(10), false))
            {
                return;
            }

            try
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
