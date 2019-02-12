using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Utils.Behaviors;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraGrid.Views.Grid;
using SBD.GL.Module.BusinessObjects.Accounts;
using ListView = DevExpress.ExpressApp.ListView;

namespace SBD.GL.Module.Win.Controllers
{
    public class AccountDragDropController : ViewController<ListView>
    {
        private BehaviorManager behaviorManager1;

        public AccountDragDropController()
        {
        

            TargetObjectType = typeof(Account);
            TargetViewType = ViewType.ListView;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            View.EditorChanged += View_EditorChanged;
            SetupEditor();
        }

        protected override void OnDeactivated()
        {
            View.EditorChanged -= View_EditorChanged;
            base.OnDeactivated();
        }

        private void View_EditorChanged(object sender, EventArgs e)
        {
            SetupEditor();
        }

        private void SetupEditor()
        {
            if (View.Editor == null) return;
            View.Editor.ControlsCreated += Editor_ControlsCreated;
            SetupDragDrop();
        }
        private void Editor_ControlsCreated(object sender, EventArgs e)
        {
            SetupDragDrop();
        }
        private void SetupDragDrop()
        {
            if (!(View.Editor is TreeListEditor editor) || editor.TreeList == null) return;

            editor.TreeList.AllowDrop = false;
            behaviorManager1 = new BehaviorManager();
            behaviorManager1.Attach<DragDropBehavior>(editor.TreeList, behavior =>
            {
                behavior.Properties.AllowDrop = true;
                behavior.Properties.InsertIndicatorVisible = true;
                behavior.Properties.PreviewVisible = true;
                behavior.DragOver += Behavior_DragOver;
                behavior.DragDrop += Behavior_DragDrop;
                behavior.BeginDragDrop += Behavior_BeginDragDrop;
            });

        }


        private void Behavior_DragOver(object sender, DragOverEventArgs e)
        {
            var args = DragOverGridEventArgs.GetDragOverGridEventArgs(e);
            e.InsertType = args.InsertType;
            e.InsertIndicatorLocation = args.InsertIndicatorLocation;
            e.Action = args.Action;
            Cursor.Current = args.Cursor;
            args.Handled = true;
        }

        private void Behavior_BeginDragDrop(object sender, BeginDragDropEventArgs e)
        {
            if (!(View.Editor is TreeListEditor editor) || editor.TreeList == null) return;
             
            // not sure what to do here
        }

        private void Behavior_DragDrop(object sender, DragDropEventArgs e)
        {
           
            var target = e.Target as ObjectTreeList;  
            if (e.Action == DragDropActions.None) return;  // If I disable this line then I can move the node
            if (target == null) return;

            var hitPoint = target.PointToClient(Cursor.Position);
        
            var hitInfo = target.CalcHitInfo(hitPoint);
          
            var droppedOnNode = hitInfo.Node;
            var droppedNode = target.FocusedNode;

            var account = droppedNode.Object as Account;
            var newParent = droppedOnNode.Data as Account;

            account.Parent = newParent;

            View.ObjectSpace.SetModified(account);
             

        }
    }
}