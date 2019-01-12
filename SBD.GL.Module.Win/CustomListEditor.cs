using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBD.GL.Module.BusinessObjects.Accounts
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using DevExpress.ExpressApp;
    using DevExpress.ExpressApp.Editors;
    using DevExpress.ExpressApp.Model;
    using DevExpress.ExpressApp.SystemModule;
    using DevExpress.ExpressApp.Templates;
    using DevExpress.ExpressApp.Utils;
    using DevExpress.ExpressApp.Win.Controls;
    using DevExpress.ExpressApp.Win.SystemModule;
    using DevExpress.Utils.Menu;
    using DevExpress.XtraBars;
    // ... 
    [ListEditor(typeof(IAccount))]
    public class WinCustomListEditor : ListEditor
    {
        private System.Windows.Forms.ListView control;
        private System.Windows.Forms.ImageList images;
        private Object controlDataSource;
        private void dataSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            Refresh();
        }
        private void control_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OnProcessSelectedItem();
            }
        }
        private void control_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnProcessSelectedItem();
            }
        }
        private void control_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
        {
            OnSelectionChanged();
        }
        private void control_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectionChanged();
            OnFocusedObjectChanged();
        }
        private System.Windows.Forms.ListViewItem FindByTag(object tag)
        {
            IAccount itemToSearch = (IAccount)tag;
            if (control != null && itemToSearch != null)
            {
                foreach (System.Windows.Forms.ListViewItem item in control.Items)
                {
                    if (((IAccount)item.Tag).Id == itemToSearch.Id)
                        return item;
                }
            }
            return null;
        }
        protected override object CreateControlsCore()
        {
            control = new System.Windows.Forms.ListView();
            control.Sorting = SortOrder.Ascending;
            images = new System.Windows.Forms.ImageList();
            images.ImageSize = new System.Drawing.Size(104, 150);
            images.ColorDepth = ColorDepth.Depth32Bit;
            control.LargeImageList = images;
            control.HideSelection = false;
            control.SelectedIndexChanged += new EventHandler(control_SelectedIndexChanged);
            control.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(control_ItemSelectionChanged);
            control.MouseDoubleClick += new MouseEventHandler(control_MouseDoubleClick);
            control.KeyDown += new System.Windows.Forms.KeyEventHandler(control_KeyDown);
            Refresh();
            return control;
        }
        protected override void AssignDataSourceToControl(Object dataSource)
        {
            //if (dataSource is DevExpress.Xpo.XPServerCollectionSource)
            //{
            //    throw new Exception("The WinCustomListEditor doesn't support Server mode and so cannot use an XPServerCollectionSource object as the data source.");
            //}
            if (controlDataSource != dataSource)
            {
                IBindingList oldBindable = controlDataSource as IBindingList;
                if (oldBindable != null)
                {
                    oldBindable.ListChanged -= new ListChangedEventHandler(dataSource_ListChanged);
                }
                controlDataSource = dataSource;
                IBindingList bindable = controlDataSource as IBindingList;
                if (bindable != null)
                {
                    bindable.ListChanged += new ListChangedEventHandler(dataSource_ListChanged);
                }
                Refresh();
            }
        }
        public WinCustomListEditor(IModelListView info)
            : base(info)
        {
        }
        public override void Dispose()
        {
            controlDataSource = null;
            base.Dispose();
        }
        public override void Refresh()
        {
            if (control == null)
                return;
            object focused = FocusedObject;
            control.SelectedItems.Clear();
            try
            {
                control.BeginUpdate();
                //images.Images.Clear();
                control.Items.Clear();
                if (ListHelper.GetList(controlDataSource) != null)
                {
                    //images.Images.Add(ImageLoader.Instance.GetImageInfo("NoImage").Image);
                    foreach (IAccount item in ListHelper.GetList(controlDataSource))
                    {
                        //int imageIndex = 0;
                        //if (item.Image != null)
                        //{
                        //    images.Images.Add(item.Image);
                        //    imageIndex = images.Images.Count - 1;
                        //}
                        //System.Windows.Forms.ListViewItem lItem =
                        //    new System.Windows.Forms.ListViewItem(item.Text, imageIndex);
                        System.Windows.Forms.ListViewItem lItem =
                            new System.Windows.Forms.ListViewItem(item.Code);

                        lItem.Tag = item;
                        control.Items.Add(lItem);
                    }
                }
            }
            finally
            {
                control.EndUpdate();
            }

            FocusedObject = focused;
            if (FocusedObject == null && control.Items.Count > 1)
            {
                FocusedObject = control.Items[0].Tag;
            }
        }
        public override IList GetSelectedObjects()
        {
            if (control == null)
                return new object[0] { };

            object[] result = new object[control.SelectedItems.Count];
            for (int i = 0; i < control.SelectedItems.Count; i++)
            {
                result[i] = control.SelectedItems[i].Tag;
            }
            return result;
        }
        public override void SaveModel()
        {
        }
        public override SelectionType SelectionType => SelectionType.Full;

        public override object FocusedObject
        {
            get => (control != null) && (control.FocusedItem != null) ? control.FocusedItem.Tag : null;
            set
            {
                var item = FindByTag(value);
                if (item == null) return;
                control.SelectedItems.Clear();

                item.Focused = true;
                item.Selected = true;
            }
        }
    }
}
