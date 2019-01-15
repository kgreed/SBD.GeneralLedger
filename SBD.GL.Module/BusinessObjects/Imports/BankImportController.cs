using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace SBD.GL.Module.BusinessObjects.Imports
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class BankImportController : ViewController
    {
        public BankImportController()
        {
            InitializeComponent();
            TargetObjectType = typeof(BankImport);

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

        private void actApplyRules_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            

            var bankImport = e.CurrentObject as BankImport;

            var rules = View.ObjectSpace.GetObjects<BankImportRule>();
            foreach (var rule in rules)
            {
                foreach (var line in bankImport.Lines)
                {
                    if (!MatchOK(line.Ref1, rule.Ref1)) { continue;}
                    if (!MatchOK(line.Ref2, rule.Ref2)) { continue; }
                    if (!MatchOK(line.Ref3, rule.Ref3)) { continue; }
                    if (!MatchOK(line.Ref4, rule.Ref4)) { continue; }
                    if (!MatchOK(line.Ref5, rule.Ref5)) { continue; }
                   
                    line.Account = View.ObjectSpace.GetObject(rule.Account);

                    if ( line.Account != null)
                    { View.ObjectSpace.ModifiedObjects.Add(line);}
                }
            }
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private bool MatchOK(string lineRef, string ruleRef)
        {
            if (lineRef == null || ruleRef == null ) return true;
            if (lineRef.Length == 0 || ruleRef.Length == 0) return true;
            return lineRef.Contains(ruleRef);
        }

        private void actClearAccounts_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var import = View.CurrentObject as BankImport;
            foreach (var line in import.Lines)
            {
                line.Account = null;
                View.ObjectSpace.ModifiedObjects.Add(line);
            }
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }
    }
}
