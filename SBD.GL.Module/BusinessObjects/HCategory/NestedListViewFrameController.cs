using DevExpress.ExpressApp;

namespace SBD.GL.Module.BusinessObjects.HCategory
{
    public class NestedListViewFrameController : ViewController
    {
        private Frame masterFrame;
        public NestedListViewFrameController()
        {
            TargetViewType = ViewType.ListView;
            TargetViewNesting = Nesting.Nested;
        }
        public void AssignMasterFrame(Frame parentFrame)
        {
            masterFrame = parentFrame;
            // Use this Frame to get Controllers and Actions.  
        }
    }
}