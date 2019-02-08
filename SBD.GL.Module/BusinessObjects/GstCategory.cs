using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using SBD.GL.Module.BusinessObjects.Accounts;

namespace SBD.GL.Module.BusinessObjects
{
    [NavigationItem("03 Config")]
    [DefaultProperty("Code")]
    [XafDisplayName("Tax Categories")]
    [ImageName("BO_Category")]
    public class GstCategory 
    {
        [Browsable(false)]
        [Key] public int Id { get; set; }

        [MaxLength(60)] // so we can index it
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        public string Code { get; set; }

        public decimal Percent { get; set; }
        [ModelDefault("RowCount", "5")]
        public string Notes { get; set; }

       
    }
}