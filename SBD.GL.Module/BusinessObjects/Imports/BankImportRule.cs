using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;

namespace SBD.GL.Module.BusinessObjects
{
    [NavigationItem("01 Imports")]
    public class BankImportRule
    {
        [Key]
        public int Id { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Ref4 { get; set; }
        public string Ref5 { get; set; }
        public virtual Account Account { get; set; }

        public string RuleName { get; set; }
    }
}