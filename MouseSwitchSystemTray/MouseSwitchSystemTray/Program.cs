using System;
using System.Windows.Forms;

namespace MouseClickSwitch
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            using (var icon = new SwitchButtonsTrayIcon())
            {
                icon.Display();

                Application.Run();
            }
        }
    }
}
