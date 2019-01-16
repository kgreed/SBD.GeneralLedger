namespace SBD.GL.Module.BusinessObjects.Imports
{
    partial class BankImportController
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
            this.actApplyRules = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actClearAccounts = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actPost = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // actApplyRules
            // 
            this.actApplyRules.Caption = "Apply Rules";
            this.actApplyRules.ConfirmationMessage = null;
            this.actApplyRules.Id = "Apply Rules";
            this.actApplyRules.ToolTip = null;
            this.actApplyRules.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actApplyRules_Execute);
            // 
            // actClearAccounts
            // 
            this.actClearAccounts.Caption = "Clear Accounts";
            this.actClearAccounts.ConfirmationMessage = null;
            this.actClearAccounts.Id = "ClearAccounts";
            this.actClearAccounts.ToolTip = null;
            this.actClearAccounts.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actClearAccounts_Execute);
            // 
            // actPost
            // 
            this.actPost.Caption = "Post";
            this.actPost.ConfirmationMessage = null;
            this.actPost.Id = "Post";
            this.actPost.ToolTip = null;
            this.actPost.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actPost_Execute);
            // 
            // BankImportController
            // 
            this.Actions.Add(this.actApplyRules);
            this.Actions.Add(this.actClearAccounts);
            this.Actions.Add(this.actPost);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction actApplyRules;
        private DevExpress.ExpressApp.Actions.SimpleAction actClearAccounts;
        private DevExpress.ExpressApp.Actions.SimpleAction actPost;
    }
}
