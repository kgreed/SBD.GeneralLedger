namespace SBD.GL.Module.BusinessObjects.Imports
{
    partial class BankImportRuleController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.actAddExpenseAccount = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actAddLiabilityAccount = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actAddAssetAccount = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actAddIncomeAccount = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actAddCogsAccount = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // actAddExpenseAccount
            // 
            this.actAddExpenseAccount.Caption = "Add Expense Account";
            this.actAddExpenseAccount.Category = "Edit";
            this.actAddExpenseAccount.ConfirmationMessage = null;
            this.actAddExpenseAccount.Id = "Add Expense Account";
            this.actAddExpenseAccount.ToolTip = null;
            this.actAddExpenseAccount.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actAddExpenseAccount_Execute);
            // 
            // actAddLiabilityAccount
            // 
            this.actAddLiabilityAccount.Caption = "Add Liability Account";
            this.actAddLiabilityAccount.ConfirmationMessage = null;
            this.actAddLiabilityAccount.Id = "Add Liability Account";
            this.actAddLiabilityAccount.ToolTip = null;
            this.actAddLiabilityAccount.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actAddLiabilityAccount_Execute);
            // 
            // actAddAssetAccount
            // 
            this.actAddAssetAccount.Caption = "Add Asset Account";
            this.actAddAssetAccount.ConfirmationMessage = null;
            this.actAddAssetAccount.Id = "Add Asset Account";
            this.actAddAssetAccount.ToolTip = null;
            this.actAddAssetAccount.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actAddAssetAccount_Execute);
            // 
            // actAddIncomeAccount
            // 
            this.actAddIncomeAccount.Caption = "Add Income Account";
            this.actAddIncomeAccount.ConfirmationMessage = null;
            this.actAddIncomeAccount.Id = "Add Income Account";
            this.actAddIncomeAccount.ToolTip = null;
            this.actAddIncomeAccount.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actAddIncomeAccount_Execute);
            // 
            // actAddCogsAccount
            // 
            this.actAddCogsAccount.Caption = "Add COGS Account";
            this.actAddCogsAccount.ConfirmationMessage = null;
            this.actAddCogsAccount.Id = "Add COGS Account";
            this.actAddCogsAccount.ToolTip = null;
            this.actAddCogsAccount.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actAddCogsAccount_Execute);
            // 
            // BankImportRuleController
            // 
            this.Actions.Add(this.actAddExpenseAccount);
            this.Actions.Add(this.actAddLiabilityAccount);
            this.Actions.Add(this.actAddAssetAccount);
            this.Actions.Add(this.actAddIncomeAccount);
            this.Actions.Add(this.actAddCogsAccount);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction actAddExpenseAccount;
        private DevExpress.ExpressApp.Actions.SimpleAction actAddLiabilityAccount;
        private DevExpress.ExpressApp.Actions.SimpleAction actAddAssetAccount;
        private DevExpress.ExpressApp.Actions.SimpleAction actAddIncomeAccount;
        private DevExpress.ExpressApp.Actions.SimpleAction actAddCogsAccount;
    }
}
