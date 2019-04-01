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
using SBD.GL.Module;

namespace SBD.GL.Win {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/DevExpressExpressAppWinWinApplicationMembersTopicAll.aspx
    public partial class GLWindowsFormsApplication : WinApplication
    {
       

        #region Default XAF configuration options (https://www.devexpress.com/kb=T501418)
        static GLWindowsFormsApplication() {
            DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = true;
            DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = false;
			DevExpress.ExpressApp.Utils.ImageLoader.Instance.UseSvgImages = true;
        }

        public string PreCompileOutputDirectory => Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), "PreCompile");

        private void InitializeDefaults() {
            LinkNewObjectToParentImmediately = false;
            OptimizedControllersCreation = true;
            UseLightStyle = true;
            EnableModelCache = true;
        }
        #endregion
        public GLWindowsFormsApplication() {
            InitializeComponent();
			InitializeDefaults();
        }

       

        public void SetupLocalPath(string preCompileOutputDirectory)
        {
            if (preCompileOutputDirectory.Length > 0)
            {
                SiteCache.Instance.LocalPath = preCompileOutputDirectory;
            }
            else
            {
                Console.WriteLine("here");
                SiteCache.Instance.LocalPath = GetLocalPath();
            }
        }

        private static string GetLocalPath()
        {
            try
            {
               return Windows.Storage.ApplicationData.Current.LocalFolder.Path; // For Desktop Bridge
            }
            catch (InvalidOperationException e)
            {
                
                return Application.LocalUserAppDataPath;  // for .Win project
            }
        }

        private string FilePath => SiteCache.Instance.LocalPath;

       
        protected override string GetDcAssemblyFilePath()
            => Path.Combine(FilePath, ApplicationName, DcAssemblyFileName);

        protected override string GetModelAssemblyFilePath()
            => Path.Combine(FilePath, ApplicationName, ModelAssemblyFileName);

        protected override string GetModelCacheFileLocationPath()
            => Path.Combine(FilePath, ApplicationName);

        protected override string GetModulesVersionInfoFilePath()
            => Path.Combine(FilePath, ApplicationName, ModulesVersionInfoFileName);

        protected override void OnCustomGetUserModelDifferencesPath(CustomGetUserModelDifferencesPathEventArgs args)
            => args.Path = Path.Combine(FilePath, ApplicationName);

        

        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {

      
            var connectionString = SiteCache.Instance.ConnectionString;
            args.ObjectSpaceProviders.Add(new EFObjectSpaceProvider(typeof(GLDbContext), TypesInfo, null,
                connectionString));

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
