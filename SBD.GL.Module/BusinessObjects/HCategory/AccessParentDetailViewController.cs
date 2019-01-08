using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl.EF;

namespace SBD.GL.Module.BusinessObjects.HCategory
{

    public class AccessParentDetailViewController : ViewController
    {
        private void UpdateDetailViewCaption()
        {
            if (!(Frame is NestedFrame)) return;
            if (View.CurrentObject != null)
            {
                ((NestedFrame)Frame).ViewItem.View.Caption = ((H2Category)View.CurrentObject).ID.ToString();
            }
        }
        private void View_CurrentObjectChanged(object sender, EventArgs e)
        {
            UpdateDetailViewCaption();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            View.CurrentObjectChanged += View_CurrentObjectChanged;
            UpdateDetailViewCaption();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            View.CurrentObjectChanged -= View_CurrentObjectChanged;
        }
        public AccessParentDetailViewController()
        {
            TargetViewType = ViewType.ListView;
            TargetViewNesting = Nesting.Nested;
            TargetObjectType = typeof(H2Category);
        }
    }
}
