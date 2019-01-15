namespace SBD.GL.Module.Win.Controllers
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
            this.actImport = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // actImport
            // 
            this.actImport.Caption = "Import CSV";
            this.actImport.ConfirmationMessage = null;
            this.actImport.Id = "ImportCSV";
            this.actImport.ToolTip = null;
            this.actImport.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.actImport_Execute);
            // 
            // TranHeaderController
            // 
            this.Actions.Add(this.actImport);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction actImport;
    }
}
