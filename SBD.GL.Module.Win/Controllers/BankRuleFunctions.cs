using System;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using SBD.GL.Module.BusinessObjects.Accounts;
 
using DevExpress.ExpressApp.Win.SystemModule;

namespace SBD.GL.Module.BusinessObjects.Imports
{
    public static class BankRuleFunctions
    {
        public static void SaveAndApplyRule(BankImport bankImport, BankImportRule rule)
        {
            try
            {
                var os = rule.ObjectSpace;
                os.CommitChanges();

                ApplyBankRule(bankImport, rule);
            }
            catch (Exception e)
            {

                throw;
            }
            
            bankImport.ObjectSpace.Refresh();

        }

        public static void AddAccount(BankImportRule rule, GLCategoryEnum categoryType)
        {
            var os = rule.ObjectSpace;
          
            var code = rule.RuleName;
            var op =  CriteriaOperator.Parse("[Code]=?",code);
            var account = os.FindObject<Account>(op) ?? CreateAccount(code, categoryType, os);

            rule.ToAccount = account;
            rule.ObjectSpace.ReloadObject(rule);
        }

        public static Account CreateAccount(string code, GLCategoryEnum type, IObjectSpace os)
        {
            
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
            account.Code =code;
            os.ModifiedObjects.Add(account);
            return account;
        }

        public static void RunBankRules(BankImport bankImport, IObjectSpace objectSpace)
        {

            var rules = objectSpace.GetObjects<BankImportRule>();
            foreach (var rule in rules)
            {
                ApplyBankRule(bankImport,  rule);
            }

        }

        public static void ApplyBankRule(BankImport bankImport,  BankImportRule rule)
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

                line.Account = line.ObjectSpace.GetObject(rule.ToAccount);
                
                if (line.Account != null)
                {
                    line.ObjectSpace.ModifiedObjects.Add(line);

                    Console.WriteLine(line.ObjectSpace.ModifiedObjects.Count);
                    line.ObjectSpace.CommitChanges();
                }
            }
        }


        private static bool MatchOK(string lineRef, string ruleRef)
        {
            if (lineRef == null || ruleRef == null) return true;
            if (lineRef.Length == 0 || ruleRef.Length == 0) return true;
            return lineRef.Contains(ruleRef);
        }

        public static void CopyRef5(BankImportRule rule)
        {
            rule.RuleName = rule.Ref5;
        }

        public static BankImportRule MakeRuleFromBankImportLine(BankImportLine importLine, IObjectSpace os)
        {
          
            var newRule = os.CreateObject<BankImportRule>();
            newRule.FromAccount = os.GetObject(importLine.BankImport.Account);
            newRule.Ref1 = importLine.Ref1;
            newRule.Ref2 = importLine.Ref2;
            newRule.Ref3 = importLine.Ref3;
            newRule.Ref4 = importLine.Ref4;
            newRule.Ref5 = importLine.Ref5;
            return newRule;
        }

    }
}