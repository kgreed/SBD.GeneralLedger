using SBD.GL.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBD.GL.Win
{
    public class WarmupCacheFactory
    {

        public object CreateApplication()
        {

            GLWindowsFormsApplication winApplication = new GLWindowsFormsApplication();

            winApplication.SetupLocalPath("");
            winApplication.ConnectionString = SiteCache.Instance.ConnectionString;
            return winApplication;
        }
    }
}
