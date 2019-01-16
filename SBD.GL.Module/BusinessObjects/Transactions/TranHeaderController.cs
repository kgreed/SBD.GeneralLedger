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
            controller.Deleting += Controller_Deleting;
            base.OnActivated();
        }

        private void Controller_Deleting(object sender, DeletingEventArgs e)
        {
            foreach (var obj in e.Objects)
            {
                var header = obj as TranHeader;
                var criteria = CriteriaOperator.Parse("[TranHeader_Id] == ?", header.Id);
                var matchingImportLines =
                    View.ObjectSpace.GetObjects<BankImportLine>(criteria); // there should only be one
                foreach (var line in matchingImportLines)
                {
                    line.MatchingHeader = null;
                    View.ObjectSpace.ModifiedObjects.Add(line);
                }

                foreach (var tran in header.Transactions)
                {
                    View.ObjectSpace.Delete(tran);
                }
            }
        }

        protected override void OnDeactivated()
        {
            controller.Deleting -= Controller_Deleting;
            base.OnDeactivated();
        }
    }
}