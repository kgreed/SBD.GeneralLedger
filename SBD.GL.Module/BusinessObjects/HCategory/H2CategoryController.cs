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
    public partial class H2CategoryController : ViewController
    {
        private NewObjectViewController controller;
        public H2CategoryController()
        {
            InitializeComponent();
            TargetObjectType = typeof(H2Category);
        }
        protected override void OnActivated()
        {
            controller = Frame.GetController<NewObjectViewController>();
            controller.ObjectCreated += controller_ObjectCreated;
            base.OnActivated();
        }
        private void controller_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            var createdObject = e.CreatedObject as H2Category;

           
          //  var numModifiedObjects =createdObject.ObjectSpace.ModifiedObjects.Count;

            var propertyCollectionSource = (View as ListView)?.CollectionSource as PropertyCollectionSource;
            if (!(propertyCollectionSource?.MasterObject is H2Category master)) return;
         
            var m = e.ObjectSpace.GetObject(master);
           
            createdObject.Parent = m;
            createdObject.Name = "t";
            m.Children.Add(createdObject);
     
            Console.WriteLine(master.ID);

        }
        
        protected override void OnDeactivated()
        {
            controller.ObjectCreated += controller_ObjectCreated;
            base.OnDeactivated();
        }
    }
}
