namespace SBD.GL.Module.BusinessObjects.Imports
{
    partial class BankImportLinePopUpActionController
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
            this.popupWindowShowActionPickAccount.Category = "Menu";
            this.popupWindowShowActionPickAccount.ConfirmationMessage = null;
            this.popupWindowShowActionPickAccount.Id = "Pick Account";
            this.popupWindowShowActionPickAccount.ImageName = "Action_Change_State";
            this.popupWindowShowActionPickAccount.Shortcut = "Control+Shift+A";
            this.popupWindowShowActionPickAccount.ToolTip = "Pick Matching Account for selected bank import lines";
            this.popupWindowShowActionPickAccount.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWindowShowActionPickAccount_CustomizePopupWindowParams);
            this.popupWindowShowActionPickAccount.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWindowShowActionPickAccount_Execute);
            // 
            // BankImportLinePopUpActionController
            // 
            this.Actions.Add(this.popupWindowShowActionPickAccount);

        }


        #endregion
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWindowShowActionPickAccount;
    }
}
