using System;
using System.Diagnostics;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Win;
using DevExpress.Persistent.Base;

namespace SBD.GL.Module.BusinessObjects.Imports
{
    public class BankRuleActionInPopupController : ViewController
    {
        public BankRuleActionInPopupController()
        {
            //Dennis: Refer to the http://documentation.devexpress.com/#Xaf/CustomDocument2815 help article to see how to reorder Actions within the PopupActions container.

            TargetObjectType = typeof(BankImportRule);
            SetUpActions();
        }
        

        private void SetUpActions()
        {
            var actionInPopupAddAssetAccount =
                new SimpleAction(this, "Asset", PredefinedCategory.PopupActions)
                {
                    ImageName = "Actions_Arrow2LeftUp",
                    Shortcut = "Control+Shift+A",
                    ToolTip = "Create Asset Account and Assign it (Control+Shift+A)"
                };
            actionInPopupAddAssetAccount.Execute += ActionAddAsset_Execute;

            var actionInPopupAddLiabilityAccount = new SimpleAction(this, "Liability", PredefinedCategory.PopupActions)
            {
                ImageName = "Actions_Arrow2LeftDown",
                Shortcut = "Control+Shift+L",
                ToolTip = "Create Liability Account and Assign it (Control+Shift+L)"
            };
            actionInPopupAddLiabilityAccount.Execute += ActionAddLiability_Execute;

            var actionInPopupAddIncomeAccount = new SimpleAction(this, "Income", PredefinedCategory.PopupActions)
            {
                ImageName = "Actions_Arrow4RightUp",
                Shortcut = "Control+Shift+I",
                ToolTip = "Create Income Account and Assign it (Control+Shift+I)"
            };
            actionInPopupAddIncomeAccount.Execute += ActionAddIncome_Execute;

            var actionInPopupAddCogsAccount = new SimpleAction(this, "COGS", PredefinedCategory.PopupActions)
            {
                ImageName = "Actions_Arrow1RightDown",
                Shortcut = "Control+Shift+L",
                ToolTip = "Create Cost of Sales Account and Assign it (Control+Shift+L)"
            };
            actionInPopupAddCogsAccount.Execute += ActionAddCogs_Execute;

            var actionInPopupAddExpenseAccount = new SimpleAction(this, "Expense", PredefinedCategory.PopupActions)
            {
                ImageName = "Actions_Arrow4RightDown",
                Shortcut = "Control+Shift+E",
                ToolTip = "Create Expense Account and Assign it (Control+Shift+E)"
            };
            actionInPopupAddExpenseAccount.Execute += ActionAddExpense_Execute;


            var actionSaveAndApply = new SimpleAction(this, "Save && Apply", PredefinedCategory.PopupActions)
            {
                ImageName = "Import",
                Shortcut = "Control+Shift+S",
                ToolTip = "Save and Apply (Control+Shift+S)"
            };
            actionSaveAndApply.Execute += ActionSaveAndApply_Execute;

            var actionCopyRef5 = new SimpleAction(this, "CopYref5", PredefinedCategory.PopupActions)
            {
                ImageName = "Copy",
                Shortcut = "Control+Shift+Y",
                ToolTip = "Copy Ref 5 to Rule Name (Control+Shift+Y)"
            };
            actionCopyRef5.Execute += ActionCopyRef5_Execute;


        }
        void ActionAddExpense_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BankRuleFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Expense );
        }
        void ActionAddIncome_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BankRuleFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Income );
        }

        void ActionAddAsset_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BankRuleFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Asset );
        }
        void ActionAddLiability_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BankRuleFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Liability );
        }
        void ActionAddCogs_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BankRuleFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.CostOfSales);
        }

        void ActionSaveAndApply_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            var importLine = View.Tag as BankImportLine;
            BankRuleFunctions.SaveAndApplyRule(importLine.BankImport, e.CurrentObject as BankImportRule);
            View.Close();
        }

        void ActionCopyRef5_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            BankRuleFunctions.CopyRef5(e.CurrentObject as BankImportRule);
            View.Refresh();
        }
    }
}