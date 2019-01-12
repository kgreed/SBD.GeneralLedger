using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base.General;

namespace SBD.GL.Module.Controllers
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
            controller.ObjectCreated += controller_ObjectCreated;
            base.OnActivated();
        }

        private void controller_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            SetParent(e, View);
        }

        private static void SetParent(ObjectCreatedEventArgs e, View view)
        {
            var createdObject = e.CreatedObject as IHCategory;

            WorkAroundBug(createdObject);

            var propertyCollectionSource = (view as ListView)?.CollectionSource as PropertyCollectionSource;
            if (!(propertyCollectionSource?.MasterObject is IHCategory master)) return;

            var m = e.ObjectSpace.GetObject(master);
            createdObject.Parent = m;
           // m.Children.Add(createdObject); // this would cause the parent to want to save.
        }

        private static void WorkAroundBug(IHCategory createdObject)
        {
            // https://www.devexpress.com/Support/Center/Question/Details/T704563/xaf-entity-framework-self-referencing-table-example-with-aggregated-attribute-for
            var c2 = createdObject as IObjectSpaceLink;
            // Do not comment out. This is a bug work around
            var numModifiedObjects = c2.ObjectSpace.ModifiedObjects.Count;
        }

        protected override void OnDeactivated()
        {
            controller.ObjectCreated += controller_ObjectCreated;
            base.OnDeactivated();
        }
    }
}