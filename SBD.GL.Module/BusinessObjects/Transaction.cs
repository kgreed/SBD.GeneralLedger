using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;

namespace SBD.GL.Module.BusinessObjects
{
    [VisibleInReports]
    [NavigationItem("Main")]
    public class Transaction : BasicBo
    {
        [Browsable(false)]
        [Key] public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Decimal Amount { get; set; }



        [Required]
        public virtual Account DebitAccount { get; set; }


        [Required]
        public virtual Account CreditAccount { get; set; }

        [MaxLength(20)]
        [System.ComponentModel.DataAnnotations.Schema.Index(IsUnique = true)]
        public string BankId { get; set; }
        public string Memo { get; set; }


        public virtual Job job { get; set; }
        public virtual Card Card { get; set; }
    }
}