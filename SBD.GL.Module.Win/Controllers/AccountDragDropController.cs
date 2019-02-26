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
using DevExpress.XtraTreeList.Nodes;
using SBD.GL.Module.BusinessObjects.Accounts;
using ListView = DevExpress.ExpressApp.ListView;

namespace SBD.GL.Module.Win.Controllers
{
    public class AccountDragDropController : ViewController<ListView>
    {
        
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
            var treeList = editor.TreeList;
            treeList.AllowDrop = false;  // setting this to true prevents any drag drop icon


            treeList.OptionsDragAndDrop.DragNodesMode = DragNodesMode.Multiple; // this is required to turn on drag drop
         //  treeList.OptionsDragAndDrop.DropNodesMode = DropNodesMode.
           treeList.DragObjectDrop += TreeList_DragObjectDrop;
           treeList.AfterDropNode += TreeList_AfterDropNode;

        }

        private void TreeList_DragObjectDrop(object sender, DragObjectDropEventArgs e)
        {
            // does not get called
           var droppedon = e.DropInfo.RowIndex;
         //   DropNodes(e.DropInfo.);
        }

        private void TreeList_AfterDropNode(object sender, AfterDropNodeEventArgs e)
        {
            
             DropNodes( e.Node, e.DestinationNode); // works but temporarily displays double nodes

        }

        private void DropNodes(TreeListNode sourceNode, TreeListNode droppedOnNode )
        {


            if (!(View.Editor is TreeListEditor editor) || editor.TreeList == null) return;
            var treeList = editor.TreeList;

            var droppedOnAccount = droppedOnNode.Tag as Account;
            
            Trace.WriteLine($"count1:{droppedOnAccount.Children.Count} ");
            var sourceAccount = sourceNode.Tag as Account;
            Trace.WriteLine($"count2:{droppedOnAccount.Children.Count} ");
            sourceAccount.Parent = droppedOnAccount;
            Trace.WriteLine($"count3:{droppedOnNode.Nodes.Count} ");
            droppedOnAccount.Children.Add(sourceAccount);
            Trace.WriteLine($"count4:{droppedOnNode.Nodes.Count} ");


            View.ObjectSpace.CommitChanges();
            treeList.RefreshNode(sourceNode);
            treeList.RefreshNode(droppedOnNode);
        }


      

        private void Behavior_BeginDragDrop(object sender, BeginDragDropEventArgs e)
        {
            if (!(View.Editor is TreeListEditor editor) || editor.TreeList == null) return;
             
            // not sure what to do here
        }

        
    }
}