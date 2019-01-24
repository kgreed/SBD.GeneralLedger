using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base.General;

namespace SBD.GL.Module.BusinessObjects.Accounts
{
    public partial class ICategoryController : ViewController
    {
        private NewObjectViewController controller;

        public ICategoryController()
        {
            InitializeComponent();
            TargetObjectType = typeof(IHCategory);
        }

        protected override void OnActivated()
        {
            controller = Frame.GetController<NewObjectViewController>();
            controller.LinkNewObjectToParentImmediately = true;
            base.OnActivated();
        }

         
    }
}