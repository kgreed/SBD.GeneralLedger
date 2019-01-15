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
    public partial class BankImportLineController : ViewController
    {
        public BankImportLineController()
        {
            InitializeComponent();
            TargetObjectType = typeof(BankImportLine);
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

        private void popupBankImportWindowShowAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
             e.PopupWindowView.ObjectSpace.CommitChanges();
        }

        private void popupBankImportWindowShowAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var newObjectSpace = Application.CreateObjectSpace(typeof(BankImportRule));
           
            var newRule = newObjectSpace.CreateObject<BankImportRule>();

            var importLine = View.CurrentObject as BankImportLine;
            newRule.Ref1 = importLine.Ref1;
            newRule.Ref2 = importLine.Ref2;
            newRule.Ref3 = importLine.Ref3;
            newRule.Ref4 = importLine.Ref4;
            newRule.Ref5 = importLine.Ref5;

            var createdView = Application.CreateDetailView(newObjectSpace, newRule);
            e.View = createdView;
        }

        private void popupWindowShowActionPickAccount_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {

            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(Account));
            string listViewId = Application.FindLookupListViewId(typeof(Account));
            CollectionSourceBase collectionSource = Application.CreateCollectionSource(objectSpace, typeof(Account), listViewId);
            e.View = Application.CreateListView(listViewId, collectionSource, true);

        }

        private void popupWindowShowActionPickAccount_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var account = e.CurrentObject as Account;
            var selectedAccount = View.ObjectSpace.GetObject(account);

            foreach (var obj in View.SelectedObjects)
            {
                var line = obj as BankImportLine;
                line.Account = selectedAccount;
                View.ObjectSpace.ModifiedObjects.Add(line);
            }
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

    }
}
