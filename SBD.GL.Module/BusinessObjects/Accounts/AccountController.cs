//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DevExpress.Data.Filtering;
//using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.Actions;
//using DevExpress.ExpressApp.Editors;
//using DevExpress.ExpressApp.Layout;
//using DevExpress.ExpressApp.Model.NodeGenerators;
//using DevExpress.ExpressApp.SystemModule;
//using DevExpress.ExpressApp.Templates;
//using DevExpress.ExpressApp.Utils;
//using DevExpress.Persistent.Base;
//using DevExpress.Persistent.Validation;
//using SBD.GL.Module.BusinessObjects;

//namespace SBD.GL.Module.Controllers
//{
//    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
//    public partial class AccountController : ViewController
//    {
//        private object _focusedObject;
//        private NewObjectViewController controller;
//        public AccountController()
//        {
//            InitializeComponent();
//            TargetObjectType = typeof(Account);
//            // Target required Views (via the TargetXXX properties) and create their Actions.
//        }
//        protected override void OnActivated()
//        {
//            base.OnActivated();
//            controller = Frame.GetController<NewObjectViewController>();
         
//            controller.ObjectCreated += controller_ObjectCreated;
//            // Perform various tasks depending on the target View.
//        }

//        private void controller_ObjectCreated(object sender, ObjectCreatedEventArgs e)
//        {
//            var view = ObjectSpace.Owner as View;

//            if (!(view?.CurrentObject is Account parent)) return;
//            var child = e.CreatedObject as Account;

//            if (child?.Parent?.Id == parent.Id )
//            {
//                Console.WriteLine("The parent is already set");  // arrives here
//            }

//            Console.WriteLine(parent.Children.Count);
//            //child.Parent = parent;
//            //parent.Children.Add(child);
//            // child.Parent_Id = parent.Id;
//            // child.Category = parent.Category;
//            // child.Code = "newcode";
//            // child.ObjectSpace.SetModified(child);


//            //ObjectSpace.SetModified(child);

//            var os = e.ObjectSpace;
//            child.Parent = os.GetObject(parent);
//            parent.Children.Add(child);

//        }

//        protected override void OnViewControlsCreated()
//        {
//            base.OnViewControlsCreated();
//            // Access and customize the target View control.
//        }
//        protected override void OnDeactivated()
//        {
//            // Unsubscribe from previously subscribed events and release other references and resources.
//            base.OnDeactivated();
//            controller.ObjectCreated -= controller_ObjectCreated;

//        }
//    }
//}
