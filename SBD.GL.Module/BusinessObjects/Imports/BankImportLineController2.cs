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
    public partial class BankImportLineController2 : ViewController
    {
        public BankImportLineController2()
        {
            InitializeComponent();
            TargetObjectType = typeof(BankImportLine);
            // Target required Views (via the TargetXXX properties) and create their Actions.
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
