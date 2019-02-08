using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using SBD.GL.Module.BusinessObjects.Accounts;

namespace SBD.GL.Module.BusinessObjects.Imports
{
    [NavigationItem("02 Imports")]
    [XafDisplayName("Bank Imports")]
    [ImageName("BO_Transition")]
    public class BankImport : IObjectSpaceLink
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public BankImport() {
            Lines = new List<BankImportLine>();
        }

        public virtual Account Account { get; set; }
        [DevExpress.Xpo.Aggregated]
        public virtual List<BankImportLine> Lines { get; set; }
        public string FileName { get; set; }
        public DateTime FileDate { get; set; }
        public DateTime ImportedAt { get; set; }

        [Browsable(false)]
        public IObjectSpace ObjectSpace { get; set; }
    }
}