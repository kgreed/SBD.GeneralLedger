using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base.General;
using SBD.GL.Module.BusinessObjects;

namespace SBD.GL.Module.Controllers
{
    public partial class TranHeaderController : ViewController
    {
        private DeleteObjectsViewController controller;

        public TranHeaderController()
        {
            InitializeComponent();
            TargetObjectType = typeof(TranHeader);
        }

        protected override void OnActivated()
        {
            controller = Frame.GetController<DeleteObjectsViewController>();
            controller.DeleteAction.Execute += DeleteAction_Execute;     
            base.OnActivated();
        }

        private void DeleteAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            var header = e.CurrentObject as TranHeader;
            var criteria = CriteriaOperator.Parse("[TranHeader_Id] > ?",header.Id);
            var matchingImportLines = View.ObjectSpace.GetObjects<BankImportLine>(criteria);  // there should only be one
            foreach (var line in matchingImportLines)
            {
                line.MatchingHeader = null;
                View.ObjectSpace.ModifiedObjects.Add(line);
            }
        }

        protected override void OnDeactivated()
        {
            controller.DeleteAction.Execute -= DeleteAction_Execute;
            base.OnDeactivated();
        }
    }
}