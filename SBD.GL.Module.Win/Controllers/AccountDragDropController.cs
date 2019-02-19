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
using DevExpress.XtraTreeList;
using SBD.GL.Module.BusinessObjects.Accounts;
using ListView = DevExpress.ExpressApp.ListView;

namespace SBD.GL.Module.Win.Controllers
{
    public class AccountDragDropController : ViewController<ListView>
    {
        private BehaviorManager behaviorManager1;
        DragDropBehavior behaviorField = null;
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
            if (behaviorField != null)
            {
                //...
                behaviorField = null;
            }
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
            var treeList = editor.TreeList;
            treeList.AllowDrop = false;  // setting this to true prevents any drag drop icon
            behaviorManager1 = new BehaviorManager();
            behaviorManager1.Attach(editor.TreeList, BehaviorSettings());
          
            treeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Single;
            treeList.OptionsDragAndDrop.DropNodesMode = DropNodesMode.Standard;

        }

        private Action<DragDropBehavior> BehaviorSettings()
        {
            return behavior =>
            {
                behavior.Properties.AllowDrop = true;
                behavior.Properties.InsertIndicatorVisible = true;
                behavior.Properties.PreviewVisible = true;
                behavior.DragOver += Behavior_DragOver;
                behavior.DragDrop += Behavior_DragDrop;
                behavior.BeginDragDrop += Behavior_BeginDragDrop;
                behaviorField = behavior;
            };
        }


        private void Behavior_DragOver(object sender, DragOverEventArgs e)
        {
            if (!(View.Editor is TreeListEditor editor) || editor.TreeList == null) return;
            var treeList = editor.TreeList;
            var args = DragOverGridEventArgs.GetDragOverGridEventArgs(e);
            e.InsertType = args.InsertType;
            e.InsertIndicatorLocation = args.InsertIndicatorLocation;
            e.Action = args.Action;
            //

            var target = e.Target as ObjectTreeList;
            var hitPoint = target.PointToClient(Cursor.Position);
            var hitInfo = target.CalcHitInfo(hitPoint);

            var candidateNode = hitInfo.Node;
            var candidateAccount = candidateNode.Tag as Account;
            //https://www.devexpress.com/Support/Center/Question/Details/Q356150/how-to-enable-drag-and-drop-feature-for-xaf-treelisteditor
            if (candidateAccount.OpeningBalance != 0)
            {
                // allow drop , but how?
                //TreeList treeControl = (TreeList)editor.Control;
                //e.Action = DragDropActions.Move;
            
                treeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.None;
            }
            else
            {
                treeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Single;
            }
            //

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
            var newParent = droppedOnNode.Tag as Account;

            account.Parent = newParent;

            View.ObjectSpace.SetModified(account);
            View.ObjectSpace.CommitChanges();
             

        }
    }
}