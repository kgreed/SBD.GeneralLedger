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
        //public static List<Account> CreateAccountAndChildAccount()
        //{

        //    using (var db = new GLDbContext())
        //    {

        //        var account = new Account
        //        {
        //            Code = Guid.NewGuid().ToString().Substring(20), GlCategory = GLCategoryEnum.Asset
        //        };

        //        db.Accounts.Add(account);
        //        db.SaveChanges();

        //        var child = new Account
        //        {
        //            Code = Guid.NewGuid().ToString().Substring(20),
        //            GlCategory = GLCategoryEnum.Asset,
        //            Parent_Id = account.Id
        //        };
        //        db.Accounts.Add(child);

        //        db.SaveChanges();

        //        return new List<Account> {account, child};

        //        // account.Children.Add(child);
        //    }
        //}

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
                var setting = new Setting {Name = settingName, Value = defaultValue};
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


        internal static bool IsValidEnum<T>(int category)
        {
            return Enum.IsDefined(typeof(T), category);
        }

        public static decimal GetOpeningBalance(TranHeader header)
        {
            using (var db = new GLDbContext())
            {
                try
                {
                    if (header.LinkedAccount == null) return 0;
                    var credits = db.Transactions
                        .Where(x => x.CreditAccount.Id == header.LinkedAccount.Id && x.TranHeader.StatementNumber < header.StatementNumber);
                        
                    var creditTotal    =  credits.Any() ? credits.Sum(y => y.Amount) :0;
                    var debits = db.Transactions
                        .Where(x => x.DebitAccount.Id == header.LinkedAccount.Id && x.TranHeader.StatementNumber < header.StatementNumber);
                    var debitTotal = debits.Any() ? debits.Sum(y => y.Amount) : 0;
                    return header.LinkedAccount.OpeningBalance + creditTotal - debitTotal;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }


        public static decimal GetNextStatementNumber(Account linkedAccount)
        {
            using (var db = new GLDbContext())
            {
                try
                {
                    var lastStatement = db.TranHeaders.Where(x => x.LinkedAccount.Id == linkedAccount.Id)
                  .OrderByDescending(x => x.StatementNumber).FirstOrDefault();


                    if (lastStatement == null)
                    {
                        return 1;
                    }

                    return lastStatement.StatementNumber + 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
              
            }
        }
    }
}
