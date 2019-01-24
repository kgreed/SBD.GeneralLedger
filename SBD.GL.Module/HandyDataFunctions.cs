using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data.Filtering;
using DevExpress.XtraCharts.Design;
using SBD.GL.Module.BusinessObjects;
using SBD.GL.Module.BusinessObjects.Accounts;

namespace SBD.GL.Module
{
    public static class HandyDataFunctions
    {
      

    



        public static void EnsureDatabaseIsCreated()
        {
            try
            {
                using (var db = new GLDbContext())
                {
                    var account = db.Accounts.FirstOrDefault();
                    if (account == null)
                    {
                        HandyDataFunctions.Seed(db);
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static void Seed(GLDbContext context)
        {
            var balanceSheetGst = context.GstCategories.Add(new GstCategory { Code = "N-T", Percent = 0 });
            var pandlGst = context.GstCategories.Add(new GstCategory { Code = "GST", Percent = 10 });

            for (int i = (int)GLCategoryEnum.Asset; i <= (int)GLCategoryEnum.OtherIncome; i++)
            {
                var IsBalSheet = IsBalanceSheet(i);
                var cat = context.GLCategories.Add(new GLCategory { Category = i, IsBalanceSheet = IsBalSheet });
                var gstCategory = IsBalSheet ? balanceSheetGst : pandlGst;
                var account = new Account { Code = $"0{i}", Category = cat, GstCategory = gstCategory };
                var child1 = new Account { Code = $"0{i}-0100", Category = cat, GstCategory = gstCategory, Parent = account };
                account.Children.Add(child1);
                var child2 = new Account { Code = $"0{i}-0200", Category = cat, GstCategory = gstCategory, Parent = account };
                account.Children.Add(child2);
                var child3 = new Account { Code = $"0{i}-0300", Category = cat, GstCategory = gstCategory, Parent = account };
                account.Children.Add(child3);

                context.Accounts.Add(account);
                context.Accounts.Add(child1);
                context.Accounts.Add(child2);
                context.Accounts.Add(child3);

            }

            context.SaveChanges();
        }

        private static bool IsBalanceSheet(int i)
        {
        
            var cat = (GLCategoryEnum)i;
            return cat == GLCategoryEnum.Asset
                   || cat == GLCategoryEnum.Liability
                   || cat == GLCategoryEnum.Equity;
        }
    }
}
