using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Validation;

namespace SBD.GL.Module.BusinessObjects
{
    [NavigationItem("Main")]
    [DefaultProperty("Code")]
    [VisibleInReports]

    public class Account : BasicBo, IHCategory, IObjectSpaceLink
    {
        public Account()
        {
            Children = new BindingList<Account>();
        }

        public override void OnCreated()
        {
            base.OnCreated();
        }

        [Browsable(false)]
        [Key] public int Id { get; set; }

        [Browsable(false)] public int Category { get; set; }

        [Browsable(true)]
        [VisibleInDetailView(true)]
        public virtual Account Parent { get; set; }

        [Browsable(false)]
        public int? Parent_Id { get; set; }

        [ForeignKey("Parent_Id")]
       // [InverseProperty("Parent")]
      //  [Aggregated]
        public virtual IList<Account> Children { get; set; }


        [MaxLength(60)] // so we can index it
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        public string Code { get; set; }

        public string Notes { get; set; }

        public decimal OpeningBalance { get; set; }

        [NotMapped]
        public GLCategory GlCategory
        {
            get => (GLCategory) Category;
            set => Category = (int) value;
        }

        [Browsable(false)]
        [NotMapped]
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
        public IObjectSpace ObjectSpace { get; set; }
    }
}