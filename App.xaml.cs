//heavenly v3.0
using System;
using System.Windows;
using JaysAi.Finale.Utility;
using JaysAi.Finale.SystemLogic;

namespace JaysAi.Finale
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppDomain.CurrentDomain.UnhandledException += (s, ex) =>
            {
                Logger.Log("Fatal crash", ex.ExceptionObject.ToString());
            };

            StartupManager.Initialize();
            Logger.Log("App started successfully.");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Logger.Log("App exited cleanly.");
        }
    }
}
