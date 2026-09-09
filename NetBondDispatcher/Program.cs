using Velopack;
using NetBondDispatcher.Forms;

namespace NetBondDispatcher;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Velopack startup hook for installs, uninstalls, and updates
        VelopackApp.Build().Run();

        // Standard Windows Forms Application configuration
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}