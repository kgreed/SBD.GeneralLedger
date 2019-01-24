using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

namespace SBD.GL.Module.BusinessObjects
{
    [NavigationItem("01 Main")]
    [XafDisplayName("Jobs")]
    [ImageName("BO_KPI_Scorecard")]
    public class Job : BasicBo
    {
        [Browsable(false)]
        [Key] public int Id { get; set; }


        [MaxLength(60)] // so we can index it
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        public string Name { get; set; }

        [ModelDefault("RowCount", "5")]
        public string Notes { get; set; }
    }
}