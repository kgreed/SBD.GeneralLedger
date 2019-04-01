using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;

namespace SBD.GL.Module.BusinessObjects.Accounts
{
    [NavigationItem("01 Main")]
    [DefaultProperty("Code")]
    [VisibleInReports]
    [XafDisplayName("Accounts")]
    [ImageName("BO_Category")]
    [Appearance("NotIsRoot", Criteria = "Parent_Id != null", AppearanceItemType = "ViewItem", TargetItems = "GlCategory", Enabled = false, Context = "DetailView")]

    public class Account : BasicBo, IHCategory, IObjectSpaceLink, IAccount
    {
        public Account()
        {
            Children = new BindingList<Account>();
          
        }

        [Browsable(false)]
        public int GLCategory_Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [ForeignKey("GLCategory_Id")]
        public virtual GLCategory Category { get; set; }
        

        public override void OnCreated()
        {
            GstCategory = HandyDefaults.DefaultGstCategory(ObjectSpace,true);
            base.OnCreated();
        }

        public override void OnSaving()
        {
           // Header = Children.Count > 0;
            base.OnSaving();
        }

         

        [MaxLength(60)] // so we can index it
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        public string Code { get; set; }

        [Browsable(false)]
        [Key] public int Id { get; set; }

       

        [Browsable(true)]
        [VisibleInDetailView(true)]
        [ImmediatePostData]
        public virtual Account Parent { get; set; }

        [Browsable(false)]
        public int? Parent_Id { get; set; }

        [ForeignKey("Parent_Id")]
        [InverseProperty("Parent")]
      
        public virtual IList<Account> Children { get; set; }

      
        [ModelDefault("RowCount","5")]
        public string Notes { get; set; }

        [Browsable(false)]
        public decimal OpeningBalance { get; set; }


        [Browsable(false)]
        [RuleFromBoolProperty("GstOk", DefaultContexts.Save, "A Gst category is needed")]
        public bool GstOk => (GstCategory != null);


        [Browsable(false)]
        [RuleFromBoolProperty("ParentCategoryOk", DefaultContexts.Save, "Parent Category if present must match")]
        public bool ParentCategoryOk
        {
            get
            {
                if (Parent == null) return true;
                return Parent.Category == Category;
            }
        }

        ITreeNode ITreeNode.Parent => Parent;

        string ITreeNode.Name => Code;

        
        ITreeNode IHCategory.Parent
        {
            get => Parent;
            set => Parent = (Account) value;
        }

        string IHCategory.Name
        {
            get => Code;
            set => Code = value;
        }

        IBindingList ITreeNode.Children => (IBindingList) Children;

        [Browsable(false)]
        public IObjectSpace ObjectSpace { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public virtual GstCategory GstCategory { get; set; }
    }

    public static class ModelDefaultConstants
    {
        public const  string AllowEdit = "AllowEdit";
        public  const string IsFalse =  "false";
        public const string IsTrue = "true";

        public const string EditMask = "EditMask";
        public const string DisplayFormat = "DisplayFormat";
        public const string EditMaskGeneral = "g";
        public const string DisplayFormatGeneral = "{0:g}";
    }
}