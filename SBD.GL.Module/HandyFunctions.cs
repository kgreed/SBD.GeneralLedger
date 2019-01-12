using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.XtraCharts.Design;
using SBD.GL.Module.BusinessObjects;

namespace SBD.GL.Module
{
    public static class HandyFunctions
    {
        public static List<Account> CreateAccountAndChildAccount()
        {

            using (var db = new GLDbContext())
            {

                var account = new Account
                {
                    Code = Guid.NewGuid().ToString().Substring(20), GlCategory = GLCategoryEnum.Asset
                };

                db.Accounts.Add(account);
                db.SaveChanges();

                var child = new Account
                {
                    Code = Guid.NewGuid().ToString().Substring(20),
                    GlCategory = GLCategoryEnum.Asset,
                    Parent_Id = account.Id
                };
                db.Accounts.Add(child);

                db.SaveChanges();

                return new List<Account>{account,child};

                // account.Children.Add(child);
            }
        }

        public static BindingList<Account> GetValidTransactionAccounts(IObjectSpace objectSpace)
        {
             
            //var criteria = CriteriaOperator.Parse("[header] = ? ", false);
            var criteria = CriteriaOperator.Parse("[Id] > 0 ");
            var results = objectSpace.GetObjects<Account>(criteria);

            var accounts = new BindingList<Account>();
            foreach (var result in results)
            {
                accounts.Add(result);
            }

            return accounts;
        }

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

        public static GstCategory DefaultGstCategory(IObjectSpace objectSpace, bool IsPandL)
        {
            var defaultCode = IsPandL ? "GST" : "N-T";
            var gstCode = GetOrMakeSetting(objectSpace, defaultCode, "GST");
            
            var gstCategory = objectSpace.FindObject<GstCategory>(
                CriteriaOperator.Parse("[Code]=?", gstCode));
            return gstCategory;
        }

        //public static bool TryParseEnum<TEnum>(this int enumValue, out TEnum retVal)
        //{
        //    retVal = default(TEnum);
        //    bool success = Enum.IsDefined(typeof(TEnum), enumValue);
        //    if (success)
        //    {
        //        retVal = (TEnum)Enum.ToObject(typeof(TEnum), enumValue);
        //    }
        //    return success;
        //}

        internal static bool IsValidEnum<T>(int category)
        {
            return Enum.IsDefined(typeof(T), category);
        }
    }
}
