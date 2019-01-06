using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    Code = Guid.NewGuid().ToString().Substring(20), GlCategory = GLCategory.Asset
                };

                db.Accounts.Add(account);
                db.SaveChanges();

                var child = new Account
                {
                    Code = Guid.NewGuid().ToString().Substring(20),
                    GlCategory = GLCategory.Asset,
                    Parent_Id = account.Id
                };
                db.Accounts.Add(child);

                db.SaveChanges();

                return new List<Account>{account,child};

         

                // account.Children.Add(child);
            }
        }
    }
}
