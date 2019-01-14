using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.Internal;
using DevExpress.Persistent.Base;
using SBD.GL.Module.BusinessObjects;

namespace SBD.GL.Module.Reports.PandL
{
    [VisibleInReports]
    [DefaultProperty("Account")]
    public class PandLReportDto  
    {
       public decimal Amount { get; set; }
       public string Account { get; set; }
    }

    public static class ReportData
    {
        public static List<PandLReportDto> PandL(DateTime fromDate, DateTime toDate)
        {
            var data = new List<PandLReportDto>();
            using (var db = new GLDbContext())
            {
                var debitResults = db.Transactions
                    .Where(x => x.TranHeader.Date >= fromDate && x.TranHeader.Date <= toDate & ! x.DebitAccount.IsBalanceSheet)
                    .GroupBy(x => x.DebitAccount)
                    .Select(r => new PandLReportDto
                    {
                        Account = r.First().DebitAccount.Code,
                        Amount = r.Sum(x => 0- x.Amount)
                    }).ToList();

                var creditResults = db.Transactions
                    .Where(x => x.TranHeader.Date >= fromDate &&
                                x.TranHeader.Date <= toDate & !x.CreditAccount.IsBalanceSheet)
                    .GroupBy(x => x.CreditAccount)
                    .Select(r => new PandLReportDto
                    {
                        Account = r.First().CreditAccount.Code,
                        Amount = r.Sum(x => x.Amount)
                    }).ToList();

                var results = debitResults;
                results.AddRange(creditResults);
               
                var refined = results.GroupBy(x=>x.Account).Select(r => new PandLReportDto
                {
                    Account = r.First().Account,
                    Amount = r.Sum(x => x.Amount)
                }).ToList();

                return refined;
            } 
        }
    }

    
}