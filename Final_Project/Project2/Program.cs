using System;
using System.Windows.Forms;

namespace Project2
{
    /// <summary>
    /// Program entry point for Project2 application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Main method - starts the WinForms application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enable Windows visual styles for the app.
            Application.EnableVisualStyles();
            // Ensure text rendering compatible with older controls.
            Application.SetCompatibleTextRenderingDefault(false);
            // Run the main input form.
            Application.Run(new Form_Main());
        }
    }
}
