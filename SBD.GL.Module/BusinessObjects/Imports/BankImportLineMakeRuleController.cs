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
    public partial class BankImportLineMakeRuleController : ViewController
    {
        IObjectSpace newObjectSpace;
        public BankImportLineMakeRuleController()
        {
            InitializeComponent();
            TargetObjectType = typeof(BankImportLine);
        }
       

        private void popupBankImportWindowShowAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
             newObjectSpace.CommitChanges();
        }

        private void popupBankImportWindowShowAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var importLine = View.CurrentObject as BankImportLine;
            newObjectSpace = Application.CreateObjectSpace(typeof(BankImportRule));
           
            var newRule = newObjectSpace.CreateObject<BankImportRule>();
            newRule.FromAccount = newObjectSpace.GetObject(importLine.BankImport.Account);
          
            newRule.Ref1 = importLine.Ref1;
            newRule.Ref2 = importLine.Ref2;
            newRule.Ref3 = importLine.Ref3;
            newRule.Ref4 = importLine.Ref4;
            newRule.Ref5 = importLine.Ref5;

            e.View = Application.CreateDetailView(newObjectSpace, newRule);
            e.View.Tag = View.CurrentObject;  //user by apply and save

        }

        

        private void popupBankImportWindowShowActionMakeRule_Cancel(object sender, EventArgs e)
        {
            newObjectSpace.Rollback(false);
        }
    }
}
