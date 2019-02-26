namespace SBD.GL.Module.Win.Controllers
{
    partial class AccountController
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
            this.actImportNab = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actMakeParent = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.actMakeChild = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // actImportNab
            // 
            this.actImportNab.Caption = "Import NAB CSV";
            this.actImportNab.ConfirmationMessage = null;
            this.actImportNab.Id = "ImportCSV";
            this.actImportNab.ImageName = "ImageImport";
            this.actImportNab.QuickAccess = true;
            this.actImportNab.Shortcut = "Control+Shift+I";
            this.actImportNab.TargetObjectsCriteria = "";
            this.actImportNab.ToolTip = "Import from a National Bank Transaction file";
            this.actImportNab.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actImportNab_Execute);
            // 
            // actMakeParent
            // 
            this.actMakeParent.Caption = "Make Parent";
            this.actMakeParent.ConfirmationMessage = null;
            this.actMakeParent.Id = "MakeParent";
            this.actMakeParent.ToolTip = null;
            // 
            // actMakeChild
            // 
            this.actMakeChild.Caption = "Make Child";
            this.actMakeChild.ConfirmationMessage = null;
            this.actMakeChild.Id = "MakeChild";
            this.actMakeChild.ToolTip = null;
            this.actMakeChild.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actMakeChild_Execute);
            // 
            // AccountController
            // 
            this.Actions.Add(this.actImportNab);
            this.Actions.Add(this.actMakeParent);
            this.Actions.Add(this.actMakeChild);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction actImportNab;
        private DevExpress.ExpressApp.Actions.SimpleAction actMakeParent;
        private DevExpress.ExpressApp.Actions.SimpleAction actMakeChild;
    }
}
