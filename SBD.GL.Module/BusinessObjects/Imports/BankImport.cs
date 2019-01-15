using System;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace SBD.GL.Module.BusinessObjects
{
    [NavigationItem("01 Imports")]
    public class BankImport
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public BankImport() {
            Lines = new List<BankImportLine>();
        }

        public virtual Account Account { get; set; }
        [Aggregated]
        public virtual List<BankImportLine> Lines { get; set; }
        public string FileName { get; set; }
        public DateTime FileDate { get; set; }
        public DateTime ImportedAt { get; set; }
        
    }
}