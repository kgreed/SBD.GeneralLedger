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
            this.actClearAMatches = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actPost = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // actApplyRules
            // 
            this.actApplyRules.Caption = "Apply Rules";
            this.actApplyRules.ConfirmationMessage = null;
            this.actApplyRules.Id = "Apply Rules";
            this.actApplyRules.ImageName = "AddQuery";
            this.actApplyRules.QuickAccess = true;
            this.actApplyRules.Shortcut = "Control+Shift+A";
            this.actApplyRules.ToolTip = "Apply rules to match Accounts to import lines";
            this.actApplyRules.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actApplyRules_Execute);
            // 
            // actClearAMatches
            // 
            this.actClearAMatches.Caption = "Clear Matches";
            this.actClearAMatches.ConfirmationMessage = null;
            this.actClearAMatches.Id = "ClearMatches";
            this.actClearAMatches.ImageName = "DeleteQuery";
            this.actClearAMatches.QuickAccess = true;
            this.actClearAMatches.Shortcut = "Control+Shift+D";
            this.actClearAMatches.ToolTip = "Clear account matches from import lines";
            this.actClearAMatches.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actClearAccounts_Execute);
            // 
            // actPost
            // 
            this.actPost.Caption = "Post";
            this.actPost.ConfirmationMessage = null;
            this.actPost.Id = "Post";
            this.actPost.ImageName = "EditQuery";
            this.actPost.Shortcut = "Control+Shift+E";
            this.actPost.ToolTip = "Post Matched Import Lines to create account transactions";
            this.actPost.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actPost_Execute);
            // 
            // BankImportController
            // 
            this.Actions.Add(this.actApplyRules);
            this.Actions.Add(this.actClearAMatches);
            this.Actions.Add(this.actPost);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction actApplyRules;
        private DevExpress.ExpressApp.Actions.SimpleAction actClearAMatches;
        private DevExpress.ExpressApp.Actions.SimpleAction actPost;
    }
}
