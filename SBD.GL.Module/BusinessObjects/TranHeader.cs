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
        [System.ComponentModel.DataAnnotations.Required]
        public DateTime Date { get; set; }
        public virtual Card Card { get; set; }

        public override void OnCreated()
        {
            Date = DateTime.Today;
            base.OnCreated();
        }

       
        [Key] public int Id { get; set; }

        [Browsable(false)]
        public IObjectSpace ObjectSpace { get; set; }
        public string Reference { get; set; }
    //  [ModelDefault("RowCount", "3")]
        public string Notes { get; set; }

        [Aggregated]
        public virtual  IList<Transaction> Transactions { get; set; }

       
        public decimal TotalCredits => Transactions.Sum(x => x.CreditAmount);
        public decimal TotalDebits => Transactions.Sum(x => x.DebitAmount);

        private Account _linkedAccount;
        //[ImmediatePostData]
        public virtual Account LinkedAccount {
            get => _linkedAccount;
            set
            {
                _linkedAccount = value;
                foreach (var tran in Transactions)
                {
                  tran.HiddenAccount= _linkedAccount;
                }
                //OnPropertyChanged();
               // Transactions = Transactions;
            }
        }


        private BindingList<Account> _accounts;
        [NotMapped]
        [Browsable(false)]
        public BindingList<Account> Accounts => _accounts ?? (_accounts = HandyFunctions.GetValidTransactionAccounts(ObjectSpace));

        public override void OnLoaded()
        {
            // we need it loaded so it displays in the combo box
            var x = Accounts;
            base.OnLoaded();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

     


    // [VisibleInReports]
}