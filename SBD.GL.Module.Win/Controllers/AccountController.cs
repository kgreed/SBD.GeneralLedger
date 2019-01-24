using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

using DevExpress.XtraReports.UI;
using LumenWorks.Framework.IO.Csv;
using SBD.GL.Module.BusinessObjects;
using SBD.GL.Module.BusinessObjects.Accounts;
using SBD.GL.Module.BusinessObjects.Imports;

namespace SBD.GL.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class AccountController : ViewController
    {
        public AccountController()
        {
            InitializeComponent();
            TargetObjectType = typeof(Account);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void actImportNab_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog
            {
                Filter = "CSV Files|*.csv", Title = "Select a Bank Export File"
            };


            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            var fileName = openFileDialog1.FileName;


            var bankImport = View.ObjectSpace.CreateObject<BankImport>();
            bankImport.FileName = openFileDialog1.FileName;
            var fi = new FileInfo(bankImport.FileName);

           
            bankImport.Account= e.CurrentObject as Account;
            bankImport.FileDate = fi.CreationTime;
            bankImport.ImportedAt = DateTime.Now;

            using (var csv = new CachedCsvReader(new StreamReader(fileName), false))
            {
                csv.Columns.Add(new Column {Name = "Date", Type = typeof(DateTime)});
                csv.Columns.Add(new Column {Name = "Amount", Type = typeof(decimal)});
                csv.Columns.Add(new Column {Name = "Ref1", Type = typeof(string)});
                csv.Columns.Add(new Column {Name = "Ref2", Type = typeof(string)});
                csv.Columns.Add(new Column {Name = "Ref3", Type = typeof(string)});
                csv.Columns.Add(new Column { Name = "Ref4", Type = typeof(string) });
                csv.Columns.Add(new Column { Name = "Ref5", Type = typeof(string) });
                csv.ReadToEnd();
                foreach (var rec in csv.Records)
                {
                    var bil = new BankImportLine
                    {
                        Date = Convert.ToDateTime(rec[0]),
                        Amount = Convert.ToDecimal(rec[1]),
                        Ref1 = rec[2],
                        Ref2 = rec[3],
                        Ref4 = rec[4],
                        Ref5 = rec[5]
                    };
                    bankImport.Lines.Add(bil);
                }

               
            }

            View.ObjectSpace.CommitChanges();

            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(BankImport));
            //var createdView = Application.CreateListView(objectSpace, typeof(BankImport), true);
            //createdView.ObjectSpace.Refresh(); // does not help


            var bi = objectSpace.GetObject<BankImport>(bankImport);

            BankRuleFunctions.RunBankRules(bi, objectSpace);


            var createdDetailView = Application.CreateDetailView(objectSpace, bi);
          

            e.ShowViewParameters.CreatedView = createdDetailView;

        }

        
    }
}
