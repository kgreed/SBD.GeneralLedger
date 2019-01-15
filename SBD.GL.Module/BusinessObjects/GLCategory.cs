using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;

namespace SBD.GL.Module.BusinessObjects
{
    // CSV structure

    [NavigationItem("01 Main")]
    [DefaultProperty("CategoryName")]
    public class GLCategory{

        [Key]
        public int Id { get; set; }

        public int Category { get; set; }
        public string CategoryName => GlCategory.ToString();

        [NotMapped]
        public GLCategoryEnum GlCategory
        {
            get => (GLCategoryEnum)Category;
            set => Category = (int)value;
        }
        public bool IsBalanceSheet { get; set; }
        //private int _category;

        //[Browsable(false)]
        //public int Category
        //{
        //    get => Parent?.Category ?? _category;
        //    set => _category = value;
        //}
    }
}