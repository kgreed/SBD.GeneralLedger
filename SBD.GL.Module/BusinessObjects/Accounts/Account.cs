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
using DevExpress.PivotGrid.SliceQueryEngine;

namespace SBD.GL.Module.BusinessObjects
{
    [NavigationItem("01 Main")]
    [DefaultProperty("Code")]
    [VisibleInReports]
  
    [Appearance("NotIsRoot", Criteria = "Parent_Id != null", AppearanceItemType = "ViewItem", TargetItems = "GlCategory", Enabled = false, Context = "DetailView")]

    public class Account : BasicBo, IHCategory, IObjectSpaceLink, IAccount
    {
        public Account()
        {
            Children = new BindingList<Account>();
          
        }

        public override void OnCreated()
        {
            bool isBalanceSheet = false;
            if (CategoryOk)
            {
                isBalanceSheet = GlCategory == GLCategoryEnum.Asset
                                 || GlCategory == GLCategoryEnum.Liability
                                 || GlCategory == GLCategoryEnum.Equity;
            }

            GstCategory = HandyFunctions.DefaultGstCategory(ObjectSpace,! isBalanceSheet);
            base.OnCreated();
        }

        public override void OnSaving()
        {
            Header = Children.Count > 0;
            base.OnSaving();
        }

        [ModelDefault("AllowEdit", "false")]
        public bool Header { get; set; }
        

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

        [NotMapped]
        public decimal Opening_Balance {
            get => Header ? 0 : OpeningBalance;
            set => OpeningBalance = value;
        }

     

        [NotMapped]
        public GLCategoryEnum GlCategory
        {
            get => (GLCategoryEnum) Category;
            set => Category = (int) value;
        }

        private int _category;

        [Browsable(false)]
        public int Category {
            get => Parent?.Category ?? _category;
            set => _category = value; 
        }

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

        [Browsable(false)]
        [RuleFromBoolProperty("CategoryOk", DefaultContexts.Save, "Category must be valid")]
        public bool CategoryOk => HandyFunctions.IsValidEnum<GLCategoryEnum>(Category);

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
}