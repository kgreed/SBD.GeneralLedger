namespace SBD.GL.Module.BusinessObjects.Imports
{
    partial class BankImportLineController
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
            this.popupBankImportWindowShowActionMakeRule = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupBankImportWindowShowActionMakeRule
            // 
            this.popupBankImportWindowShowActionMakeRule.AcceptButtonCaption = null;
            this.popupBankImportWindowShowActionMakeRule.CancelButtonCaption = null;
            this.popupBankImportWindowShowActionMakeRule.Caption = "Make Rule";
            this.popupBankImportWindowShowActionMakeRule.Category = "Menu";
            this.popupBankImportWindowShowActionMakeRule.ConfirmationMessage = null;
            this.popupBankImportWindowShowActionMakeRule.Id = "Make Rule";
            this.popupBankImportWindowShowActionMakeRule.ToolTip = "Make a rule from the selected import line";
            this.popupBankImportWindowShowActionMakeRule.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupBankImportWindowShowAction_CustomizePopupWindowParams);
            this.popupBankImportWindowShowActionMakeRule.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupBankImportWindowShowAction_Execute);
            this.popupBankImportWindowShowActionMakeRule.Cancel += new System.EventHandler(this.popupBankImportWindowShowActionMakeRule_Cancel);
            // 
            // BankImportLineController
            // 
            this.Actions.Add(this.popupBankImportWindowShowActionMakeRule);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupBankImportWindowShowActionMakeRule;
    }
}
