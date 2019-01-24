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
            // 
            // actImportNab
            // 
            this.actImportNab.Caption = "Import NAB CSV";
            this.actImportNab.ConfirmationMessage = null;
            this.actImportNab.Id = "ImportCSV";
            this.actImportNab.ImageName = "ImageImport";
            this.actImportNab.QuickAccess = true;
            this.actImportNab.Shortcut = "Control+Shift+I";
            this.actImportNab.TargetObjectsCriteria = "Header = false";
            this.actImportNab.ToolTip = "Import from a National Bank Transaction file";
            this.actImportNab.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actImportNab_Execute);
            // 
            // AccountController
            // 
            this.Actions.Add(this.actImportNab);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction actImportNab;
    }
}
