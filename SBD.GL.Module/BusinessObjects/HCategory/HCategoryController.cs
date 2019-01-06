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
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;

namespace SBD.GL.Module.BusinessObjects.HCategory
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class HCategoryController : ViewController
    {
        private NewObjectViewController controller;
        public HCategoryController()
        {
            InitializeComponent();
            TargetObjectType = typeof(H2Category);
        }
        protected override void OnActivated()
        {
            controller = Frame.GetController<NewObjectViewController>();
            controller.ObjectCreated += controller_ObjectCreated;
            base.OnActivated();
         
            // Perform various tasks depending on the target View.
        }
        private void controller_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            var view = ObjectSpace.Owner as View;

            if (!(view?.CurrentObject is H2Category parent)) return;
            var child = e.CreatedObject as H2Category;
            var os = e.ObjectSpace;

            //uncommenting this will cause the error
            //child.Parent = os.GetObject(parent);
            //parent.Children.Add(child);

        }
        
        protected override void OnDeactivated()
        {
            controller.ObjectCreated += controller_ObjectCreated;
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
