using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using SBD.GL.Module.BusinessObjects.Accounts;

namespace SBD.GL.Module.BusinessObjects.Imports
{
    public static class BankRuleFunctions
    {
        public static void SaveAndApplyRule(BankImportRule rule)
        {
            var os = rule.ObjectSpace;
            rule.ApplyOnAdd = true;
        }

        public static void AddAccount(BankImportRule rule, GLCategoryEnum type)
        {
            var os = rule.ObjectSpace;
            var account = os.CreateObject<Account>();
            var glCriteria = CriteriaOperator.Parse("[Category] == ? ", type);
            var glCategories = os.GetObjects<GLCategory>(glCriteria);
            var glCategory = glCategories.FirstOrDefault();
            var criteria = CriteriaOperator.Parse("Parent_ID == null && [GLCategory_Id] == ? ", glCategory.Id);

            var rootExpenseAccount = os.FindObject<Account>(criteria);
            account.Parent = rootExpenseAccount;
            account.Category = glCategory;
            var isPandL = type == GLCategoryEnum.CostOfSales ||
                          type == GLCategoryEnum.Income ||
                          type == GLCategoryEnum.Expense ||
                          type == GLCategoryEnum.OtherExpense ||
                          type == GLCategoryEnum.OtherIncome;
            account.GstCategory = HandyDefaults.DefaultGstCategory(os, isPandL);
            account.Code = $"{account.Parent.Code} {rule.RuleName}";
            os.ModifiedObjects.Add(account);
            rule.ToAccount = account;
            rule.ObjectSpace.ReloadObject(rule);
        }
        public static void RunBankRules(BankImport bankImport, IObjectSpace objectSpace)
        {

            var rules = objectSpace.GetObjects<BankImportRule>();
            foreach (var rule in rules)
            {
                foreach (var line in bankImport.Lines)
                {
                    if (!MatchOK(line.Ref1, rule.Ref1))
                    {
                        continue;
                    }

                    if (!MatchOK(line.Ref2, rule.Ref2))
                    {
                        continue;
                    }

                    if (!MatchOK(line.Ref3, rule.Ref3))
                    {
                        continue;
                    }

                    if (!MatchOK(line.Ref4, rule.Ref4))
                    {
                        continue;
                    }

                    if (!MatchOK(line.Ref5, rule.Ref5))
                    {
                        continue;
                    }

                    line.Account = objectSpace.GetObject(rule.ToAccount);

                    if (line.Account != null)
                    {
                        objectSpace.ModifiedObjects.Add(line);
                    }
                }

            }





        }


        private static bool MatchOK(string lineRef, string ruleRef)
        {
            if (lineRef == null || ruleRef == null) return true;
            if (lineRef.Length == 0 || ruleRef.Length == 0) return true;
            return lineRef.Contains(ruleRef);
        }
    }
}