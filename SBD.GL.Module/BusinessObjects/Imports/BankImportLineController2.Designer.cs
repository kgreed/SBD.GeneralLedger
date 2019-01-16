namespace SBD.GL.Module.BusinessObjects.Imports
{
    partial class BankImportLineController2
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
            this.popupWindowShowActionPickAccount = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
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
            // BankImportLineController2
            // 
            this.Actions.Add(this.popupWindowShowActionPickAccount);

        }


        #endregion
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionPickAccount;
    }
}
