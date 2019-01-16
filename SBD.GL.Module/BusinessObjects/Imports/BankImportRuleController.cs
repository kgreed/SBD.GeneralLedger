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
    public partial class BankImportRuleController : ViewController
    {
        public BankImportRuleController()
        {
            InitializeComponent();
            TargetObjectType = typeof(BankImportRule);
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

        private void actAddExpenseAccount_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            AddAccount(e,GLCategoryEnum.Expense);
        }

        private void AddAccount(SimpleActionExecuteEventArgs e, GLCategoryEnum type)
        {
            HandyFunctions.AddAccount(e.CurrentObject as BankImportRule, type );
         
        }

        private void actAddLiabilityAccount_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            AddAccount(e, GLCategoryEnum.Liability);
        }

        private void actAddAssetAccount_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            AddAccount(e, GLCategoryEnum.Asset);
        }

        private void actAddIncomeAccount_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            AddAccount(e, GLCategoryEnum.Income);
        }

        private void actAddCogsAccount_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            AddAccount(e, GLCategoryEnum.CostOfSales);
        }
    }
}
