using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;

namespace SBD.GL.Module.BusinessObjects
{
    [NavigationItem("Main")]
    public class Job : BasicBo
    {
        [Browsable(false)]
        [Key] public int Id { get; set; }
        [MaxLength(450)]
        public string Name { get; set; }
    }
}