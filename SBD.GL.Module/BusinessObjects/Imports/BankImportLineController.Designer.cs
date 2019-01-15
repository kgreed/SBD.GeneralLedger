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
            this.popupBankImportWindowShowAction = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.popupWindowShowActionPickAccount = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupBankImportWindowShowAction
            // 
            this.popupBankImportWindowShowAction.AcceptButtonCaption = null;
            this.popupBankImportWindowShowAction.CancelButtonCaption = null;
            this.popupBankImportWindowShowAction.Caption = "Make Rule";
            this.popupBankImportWindowShowAction.Category = "Edit";
            this.popupBankImportWindowShowAction.ConfirmationMessage = null;
            this.popupBankImportWindowShowAction.Id = "Make Rule";
            this.popupBankImportWindowShowAction.ToolTip = null;
            this.popupBankImportWindowShowAction.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupBankImportWindowShowAction_CustomizePopupWindowParams);
            this.popupBankImportWindowShowAction.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupBankImportWindowShowAction_Execute);
            // 
            // popupWindowShowActionPickAccount
            // 
            this.popupWindowShowActionPickAccount.AcceptButtonCaption = null;
            this.popupWindowShowActionPickAccount.CancelButtonCaption = null;
            this.popupWindowShowActionPickAccount.Caption = "Pick Account";
            this.popupWindowShowActionPickAccount.ConfirmationMessage = null;
            this.popupWindowShowActionPickAccount.Id = "Pick Account";
            this.popupWindowShowActionPickAccount.ToolTip = null;
            this.popupWindowShowActionPickAccount.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionPickAccount_CustomizePopupWindowParams);
            this.popupWindowShowActionPickAccount.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionPickAccount_Execute);
            // 
            // BankImportLineController
            // 
            this.Actions.Add(this.popupBankImportWindowShowAction);
            this.Actions.Add(this.popupWindowShowActionPickAccount);

        }


        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupBankImportWindowShowAction;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionPickAccount;
    }
}
