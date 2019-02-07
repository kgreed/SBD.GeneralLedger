using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;
using SBD.GL.Module;
using SBD.GL.Module.BusinessObjects;

namespace SBD.GL.Win {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
#if EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached;
            //if(Tracing.GetFileLocationFromSettings() == DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder) {
            //    Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath;
            //}
         //   string filePath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;

            

             

            GLWindowsFormsApplication winApplication = new GLWindowsFormsApplication();

            winApplication.SetupLocalPath("");
            winApplication.ConnectionString = SiteCache.Instance.ConnectionString;

            Tracing.LogName = Path.Combine(SiteCache.Instance.LocalPath, HandyDefaults.APP_NAME, "logs", "eXpressAppFramework");

            if (!Directory.Exists(Path.GetDirectoryName(Tracing.LogName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Tracing.LogName));
            }


            Tracing.Initialize();

            // Refer to the https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112680.aspx help article for more details on how to provide a custom splash form.
            //winApplication.SplashScreen = new DevExpress.ExpressApp.Win.Utils.DXSplashScreen("YourSplashImage.png");

            //if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            //{
            //    winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //}
#if EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#endif
#if DEBUG
            if(System.Diagnostics.Debugger.IsAttached && winApplication.CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema) {
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
            }
#endif
            try {

                // access db so it is created here.
                HandyDataFunctions.EnsureDatabaseIsCreated();
                // end of modification

                winApplication.Setup();
                winApplication.Start();
            }
            catch(Exception e) {
                winApplication.HandleException(e);
            }
        }
    }
}
