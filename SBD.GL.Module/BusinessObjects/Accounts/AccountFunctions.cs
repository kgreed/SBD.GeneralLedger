using System;
using System.Linq;
using SBD.GL.Module.BusinessObjects.Transactions;

namespace SBD.GL.Module.BusinessObjects.Accounts
{
    public static class AccountFunctions
    {
        public static decimal GetOpeningBalance(TranHeader header)
        {
            using (var db = new GLDbContext())
            {
                try
                {
                    if (header.LinkedAccount == null) return 0;
                    var credits = db.Transactions
                        .Where(x => x.CreditAccount.Id == header.LinkedAccount.Id &&
                                    x.TranHeader.StatementNumber < header.StatementNumber);

                    var creditTotal = credits.Any() ? credits.Sum(y => y.Amount) : 0;
                    var debits = db.Transactions
                        .Where(x => x.DebitAccount.Id == header.LinkedAccount.Id &&
                                    x.TranHeader.StatementNumber < header.StatementNumber);
                    var debitTotal = debits.Any() ? debits.Sum(y => y.Amount) : 0;
                    return header.LinkedAccount.OpeningBalance + creditTotal - debitTotal;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }


        public static decimal GetNextStatementNumber(Account linkedAccount)
        {
            using (var db = new GLDbContext())
            {
                try
                {
                    var lastStatement = db.TranHeaders.Where(x => x.LinkedAccount.Id == linkedAccount.Id)
                        .OrderByDescending(x => x.StatementNumber).FirstOrDefault();


                    if (lastStatement == null)
                    {
                        return 1;
                    }

                    return lastStatement.StatementNumber + 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

            }
        }
    }
}