using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;

namespace SBD.GL.Module.BusinessObjects.Imports
{
    public class ActionInPopupController : ViewController
    {
        public ActionInPopupController()
        {
            //Dennis: Refer to the http://documentation.devexpress.com/#Xaf/CustomDocument2815 help article to see how to reorder Actions within the PopupActions container.

            TargetObjectType = typeof(BankImportRule);
            SetUpActions();
        }

        private void SetUpActions()
        {
            var actionInPopupAddAssetAccount = new SimpleAction(this, "addAssetAccount", PredefinedCategory.PopupActions);
            actionInPopupAddAssetAccount.Execute += actionAddAsset_Execute;

            var actionInPopupAddLiabilityAccount = new SimpleAction(this, "addLiabilityAccount", PredefinedCategory.PopupActions);
            actionInPopupAddLiabilityAccount.Execute += actionAddLiability_Execute;

            var actionInPopupAddExpenseAccount = new SimpleAction(this, "addExpenseAccount", PredefinedCategory.PopupActions);
            actionInPopupAddExpenseAccount.Execute += actionAddExpense_Execute;

            var actionInPopupAddIncomeAccount = new SimpleAction(this, "addIncomeAccount", PredefinedCategory.PopupActions);
             
            actionInPopupAddIncomeAccount.Execute += actionAddIncome_Execute;

            var actionInPopupAddCogsAccount = new SimpleAction(this, "addCogsAccount", PredefinedCategory.PopupActions);
            actionInPopupAddCogsAccount.Execute += actionAddCogs_Execute;

 

        }
        void actionAddExpense_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            HandyFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Expense );
        }
        void actionAddIncome_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            HandyFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Income );
        }

        void actionAddAsset_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            HandyFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Asset );
        }
        void actionAddLiability_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            HandyFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.Liability );
        }
        void actionAddCogs_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            HandyFunctions.AddAccount(e.CurrentObject as BankImportRule, GLCategoryEnum.CostOfSales);
        }
    }
}