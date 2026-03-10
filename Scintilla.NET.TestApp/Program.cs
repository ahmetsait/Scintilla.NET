using System;
using System.Windows.Forms;

namespace ScintillaNET.TestApp;

static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        try
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
