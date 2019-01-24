using System;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using SBD.GL.Module.BusinessObjects;

namespace SBD.GL.Module
{
    public static class HandySettings
    {

        public static string GetOrMakeSetting(IObjectSpace os, string settingName, string defaultValue)
        {

            var setting = os.FindObject<Setting>(CriteriaOperator.Parse("[Name]=? ", settingName));
            if (setting != null) return setting.Value;
            setting = MakeSetting(settingName, defaultValue);

            return setting.Value;

        }


        private static Setting MakeSetting(string settingName, string defaultValue)
        {

            using (var connect = new GLDbContext())
            {
                var setting = new Setting { Name = settingName, Value = defaultValue };
                connect.Settings.Add(setting);
                connect.SaveChanges();

                var newSetting = connect.Settings.SingleOrDefault(x => x.Name == setting.Name);
                if (newSetting == null)
                {
                    throw new Exception("Expected setting to be found");
                }

                return newSetting;
            }

        }
    }
}