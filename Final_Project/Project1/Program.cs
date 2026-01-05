using Project1;
using System;
using System.Windows.Forms;

namespace Project1
{
    /// <summary>
    /// This is the main entry point for my stock analysis app
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main method that starts everything up
        /// </summary>
        [STAThread]
        static void Main()
        {
            // I need to enable visual styles so the app looks modern
            Application.EnableVisualStyles();
            // Setting this to false makes fonts render better
            Application.SetCompatibleTextRenderingDefault(false);
            // This starts my main form and runs the whole app
            Application.Run(new Form_Main());
        }
    }
}
