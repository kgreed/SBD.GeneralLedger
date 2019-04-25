using System;
 
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
 
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
           
            var newRule = BankRuleFunctions.MakeRuleFromBankImportLine(importLine, newObjectSpace);

            e.View = Application.CreateDetailView(newObjectSpace, newRule);
            e.View.Tag = View.CurrentObject;  //user by apply and save

        }

       

        private void popupBankImportWindowShowActionMakeRule_Cancel(object sender, EventArgs e)
        {
            newObjectSpace.Rollback(false);
        }
    }
}
