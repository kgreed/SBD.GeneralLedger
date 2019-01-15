using System;
using System.ComponentModel.DataAnnotations;

namespace SBD.GL.Module.BusinessObjects
{
    public class BankImportLine
    {
        [Key]
        public int Id { get; set; }

        public virtual BankImport  BankImport { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Ref1 { get; set; }
        public string Ref2 { get; set; }
        public string Ref3 { get; set; }
        public string Ref4 { get; set; }
        public string Ref5 { get; set; }
        public virtual Account Account { get; set; }
        public string Note { get; set; }
    }
}