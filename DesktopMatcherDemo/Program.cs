using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopMatcherDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var f = new Form1();
            f.FormBorderStyle = FormBorderStyle.FixedDialog;
            // Set the MaximizeBox to false to remove the maximize box.
            f.MaximizeBox = false;
            // Set the MinimizeBox to false to remove the minimize box.
            f.MinimizeBox = false;
            // Set the start position of the form to the center of the screen.
            f.StartPosition = FormStartPosition.CenterScreen;

            Application.Run(f);
        }
    }
}
