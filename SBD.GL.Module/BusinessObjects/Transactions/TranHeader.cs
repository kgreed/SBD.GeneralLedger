using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using SBD.GL.Module.Annotations;

namespace SBD.GL.Module.BusinessObjects
{

    [NavigationItem("01 Main")]
    [DefaultProperty("Id")]
    public class TranHeader : BasicBo, IObjectSpaceLink, INotifyPropertyChanged
    {
        public TranHeader()
        {
            Transactions = new List<Transaction>();
        }

        [ModelDefault(ModelDefaultConstants.EditMask, ModelDefaultConstants.EditMaskGeneral)]
        [ModelDefault(ModelDefaultConstants.DisplayFormat,ModelDefaultConstants.DisplayFormatGeneral)]
        public decimal StatementNumber { get; set; }  

        [System.ComponentModel.DataAnnotations.Required]
        public DateTime Date { get; set; }
        public virtual Card Card { get; set; }

        //[Browsable(false)]
        //public int? BankImportLine_Id { get; set; }
        //[ForeignKey("BankImportLine_Id")]
        //public virtual BankImportLine  BankImportLine { get; set; }

        public override void OnCreated()
        {
            if ( InstanceWideMemvars.Instance.EntryDate == Convert.ToDateTime(null) )
            {
                InstanceWideMemvars.Instance.EntryDate = DateTime.Now;
            }
            Date = InstanceWideMemvars.Instance.EntryDate;
     
            base.OnCreated();
        }
        
        public override void OnSaving()
        {
            if (Id == 0) //new
            {
                InstanceWideMemvars.Instance.EntryDate = Date;

            }
            base.OnSaving();
        }
      //  [Browsable(false)]
        [Key] public int Id { get; set; }

        [Browsable(false)]
        public IObjectSpace ObjectSpace { get; set; }
        public string Reference { get; set; }
    //  [ModelDefault("RowCount", "3")]
        public string Notes { get; set; }

        [Aggregated]
        public virtual  IList<Transaction> Transactions { get; set; }

       

    
        private Account _linkedAccount;
        [ImmediatePostData]
        [XafDisplayName("Account")]
        public virtual Account LinkedAccount {
            get => _linkedAccount;
            set
            {
                _linkedAccount = value;
                //foreach (var tran in Transactions)
                //{
                //  tran.HiddenAccount= _linkedAccount;
                //}
        
                OpeningBalance = LinkedAccount != null ? HandyFunctions.GetOpeningBalance(this) : 0;
            }
        }

        [NotMapped]
        [ModelDefault(ModelDefaultConstants.AllowEdit, ModelDefaultConstants.IsFalse)]
        public decimal OpeningBalance { get; set; }

        public decimal TotalCredits => Transactions.Sum(x => x.CreditAmount);
        public decimal TotalDebits => Transactions.Sum(x => x.DebitAmount);

        public decimal ClosingBalance => OpeningBalance + TotalCredits - TotalDebits;


        //private BindingList<Account> _accounts;
        //[NotMapped]
        //[Browsable(false)]
        //public BindingList<Account> Accounts => _accounts ?? (_accounts = HandyFunctions.GetValidTransactionAccounts(ObjectSpace));

        public override void OnLoaded()
        {
            // we need it loaded so it displays in the combo box
        //    var x = Accounts;
            base.OnLoaded();
        }

       
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

     


     
}