using System;
using System.Collections.Generic;
using System.Linq;
using SBD.GL.Module.BusinessObjects;

namespace SBD.GL.Module.Reports.PandL
{
    public static class BalanceSheetData
    {
        public static List<BalanceSheetDto> BalanceSheet(DateTime AsAtDate)
        {
            var data = new List<BalanceSheetDto>();
            using (var db = new GLDbContext())
            {
                var debitResults = db.Transactions.
                    Where(x=>x.TranHeader.Date <= AsAtDate && x.DebitAccount.Category.IsBalanceSheet)
                    .GroupBy(x=>x.DebitAccount).Select(r => new BalanceSheetDto()
                    {
                        Account = r.FirstOrDefault().DebitAccount.Code,
                        Amount = r.Sum(x => 0 - x.Amount) + r.FirstOrDefault().DebitAccount.OpeningBalance
                    }).ToList();

                var creditResults = db.Transactions.
                    Where(x => x.TranHeader.Date <= AsAtDate && x.CreditAccount.Category.IsBalanceSheet)
                    .GroupBy(x => x.CreditAccount).Select(r => new BalanceSheetDto()
                    {
                        Account = r.FirstOrDefault().DebitAccount.Code,
                        Amount = r.Sum(x => 0 - x.Amount) + r.FirstOrDefault().CreditAccount.OpeningBalance
                    }).ToList();

                 
                var results = debitResults;
                results.AddRange(creditResults);

                var refined = results.GroupBy(x => x.Account).Select(r => new BalanceSheetDto()
                {
                    Account = r.First().Account,
                    Amount = r.Sum(x => x.Amount)
                }).ToList();

                return refined;
            }

         
        }
    }
}