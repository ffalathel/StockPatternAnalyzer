using System;
using System.Windows.Forms;

namespace Project3
{
    /// <summary>
    /// Main entry point class for the Stock Pattern Analyzer application.
    /// Project 3 adds pattern recognition capabilities to the stock charting application.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Initializes Windows Forms and starts the main form.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Enable visual styles for modern Windows look
            Application.EnableVisualStyles();
            // Use compatible text rendering
            Application.SetCompatibleTextRenderingDefault(false);
            // Run the main form
            Application.Run(new Form_Main());
        }
    }
}
