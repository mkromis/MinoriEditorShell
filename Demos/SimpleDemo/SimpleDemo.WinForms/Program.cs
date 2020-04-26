using System;
using System.Windows.Forms;

namespace SimpleDemo.WinForms
{
    class MainClass
    {
        [STAThread]
        static void Main()
        {
            // initialize app
            var setup = new Setup();
            setup.InitializePrimary();
            //setup.InitializeSecondary();

            MessageBox.Show("Something");

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainForm());
        }
    }
}
