using System;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Xpo;

namespace SBD.GL.Module.BusinessObjects.Accounts
{
    [Table("Accounts")]
    public class SimpleAccount : IAccount
    {
        public int Id { get; set; }
        public string Code { get; set; }
    }

}