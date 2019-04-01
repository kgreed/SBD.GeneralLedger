using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.TreeListEditors.Win;
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

           treeList.DragObjectDrop += TreeList_DragObjectDrop;
           treeList.AfterDropNode += TreeList_AfterDropNode;
        }

        private void TreeList_DragObjectDrop(object sender, DragObjectDropEventArgs e)
        {
           throw  new Exception("This never gets called");
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
            treeList.OptionsDragAndDrop.BeginUpdate();

            var sourceAccount = sourceNode.Tag as Account;
            
            sourceAccount.Parent = droppedOnAccount;
            droppedOnAccount.Children.Add(sourceAccount);

            var prevNode = sourceNode.PrevVisibleNode;
            
            //sourceAccount.
            //var nodeName =

            //var myNode = treeList.FindNode(

            //    (node) => {

            //        return node["City"].ToString() == "London")

            View.ObjectSpace.CommitChanges();
            treeList.RefreshNode(sourceNode);
            treeList.RefreshNode(droppedOnNode);
            treeList.OptionsDragAndDrop.EndUpdate();
        }
    }
}