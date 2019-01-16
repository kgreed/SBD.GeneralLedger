using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using SBD.GL.Module.Annotations;


namespace SBD.GL.Module.BusinessObjects
{
  //  [VisibleInReports]
    [NavigationItem("01 Main")]
    [DefaultListViewOptions(true, NewItemRowPosition.Bottom)]
    [DefaultProperty("Summary")]
    [VisibleInReports]
    public class Transaction : BasicBo, IObjectSpaceLink, ICashbookLine , INotifyPropertyChanged
    {
        [Browsable(false)]
        [Key] public int Id { get; set; }

       // [Browsable(false)]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [Required]
        public Decimal Amount { get; set; }

        [Browsable(false)]
        [Required]
        public virtual Account DebitAccount { get; set; }

        [Browsable(false)]
        [Required]
        public virtual Account CreditAccount { get; set; }

        [Browsable(false)]
        public string Summary => $"{CreditAccount} Cr to {DebitAccount} Dr {Amount}";
        public string Memo { get; set; }

        [Browsable(false)]
        public  int TranHeader_Id { get; set; }


        [Browsable(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ForeignKey("TranHeader_Id")]
        [Required]
        public virtual TranHeader TranHeader { get; set; }

        public virtual Job job { get; set; }

        [Browsable(false)]
        public IObjectSpace ObjectSpace { get; set; }

        [NotMapped]
        public Account Account
        {
            get => Amount > 0 ? DebitAccount  : CreditAccount;
            set
            {
                if (Amount > 0)
                {
                    DebitAccount = value  ;
                }
                CreditAccount =   value;
              //  HiddenAccount = TranHeader.LinkedAccount;
            }
        }

        [NotMapped]
        public Account HiddenAccount  // other side of the transaction = header linked account
        {
            get
            {
                if (Amount > 0)
                {
                    return CreditAccount;
                }

                return DebitAccount;
            }
            set
            {
                if (Amount > 0)
                {
                    CreditAccount = value;
                }
                DebitAccount = value;
                //OnPropertyChanged();
            }
        }

        [NotMapped]
        public decimal CreditAmount {
            get => Amount > 0 ? 0 : 0- Amount;
            set => Amount = 0 - value;
        }

        [NotMapped]
        public decimal DebitAmount
        {
            get => Amount > 0 ? Amount : 0;
            set => Amount = value;
        }

        [Browsable(false)]
        [RuleFromBoolProperty("AccountOk", DefaultContexts.Save, "Account must not be a header account")]
        public bool AccountOk => Account.Header == false;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // in a cash book there is an credit, debit and account column as vs the actual data structure of amount, debit account, credit account
    public interface ICashbookLine
    {
        Account Account { get; set; }
        Decimal DebitAmount { get; set; }
        Decimal CreditAmount { get; set; }
    }
}