using System;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.EF;
using SBD.GL.Module.BusinessObjects;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SBD.GL.Win {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWinWinApplicationMembersTopicAll.aspx
    public partial class GLWindowsFormsApplication : WinApplication
    {
        public static string APP_NAME => "SBD.GL";

        #region Default XAF configuration options (https://www.devexpress.com/kb=T501418)
        static GLWindowsFormsApplication() {
            DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = true;
            DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = false;
			DevExpress.ExpressApp.Utils.ImageLoader.Instance.UseSvgImages = true;
        }
        private void InitializeDefaults() {
            LinkNewObjectToParentImmediately = false;
            OptimizedControllersCreation = true;
            UseLightStyle = true;
        }
        #endregion
        public GLWindowsFormsApplication() {
            InitializeComponent();
			InitializeDefaults();
        }

        // public static string FilePath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;

        //public string FilePath => Path.GetDirectoryName(GetType().Assembly.Location);

          string filePath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
        //string filePath = Application.LocalUserAppDataPath;
        // paste from  http://blog.delegate.at/2018/04/15/how-to-use-the-desktop-bridge-to-create-an-appx-package-for-xaf.html

        protected override string GetDcAssemblyFilePath()
            => Path.Combine(filePath, ApplicationName, DcAssemblyFileName);

        protected override string GetModelAssemblyFilePath()
            => Path.Combine(filePath, ApplicationName, ModelAssemblyFileName);

        protected override string GetModelCacheFileLocationPath()
            => Path.Combine(filePath, ApplicationName);

        protected override string GetModulesVersionInfoFilePath()
            => Path.Combine(filePath, ApplicationName, ModulesVersionInfoFileName);

        protected override void OnCustomGetUserModelDifferencesPath(CustomGetUserModelDifferencesPathEventArgs args)
            => args.Path = Path.Combine(filePath, ApplicationName);

        
        // end paste 


        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
			if(args.Connection != null) {
				args.ObjectSpaceProviders.Add(new EFObjectSpaceProvider(typeof(GLDbContext), TypesInfo, null, (DbConnection)args.Connection));
			}
			else {
				args.ObjectSpaceProviders.Add(new EFObjectSpaceProvider(typeof(GLDbContext), TypesInfo, null, args.ConnectionString));
			}
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }
        private void GLWindowsFormsApplication_CustomizeLanguagesList(object sender, CustomizeLanguagesListEventArgs e) {
            string userLanguageName = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            if(userLanguageName != "en-US" && e.Languages.IndexOf(userLanguageName) == -1) {
                e.Languages.Add(userLanguageName);
            }
        }
        private void GLWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
#if EASYTEST
            e.Updater.Update();
            e.Handled = true;
#else
            if(System.Diagnostics.Debugger.IsAttached) {
                e.Updater.Update();
                e.Handled = true;
            }
            else {
				string message = "The application cannot connect to the specified database, " +
					"because the database doesn't exist, its version is older " +
					"than that of the application or its schema does not match " +
					"the ORM data model structure. To avoid this error, use one " +
					"of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

				if(e.CompatibilityError != null && e.CompatibilityError.Exception != null) {
					message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;
				}
				throw new InvalidOperationException(message);
            }
#endif
        }
    }
}
