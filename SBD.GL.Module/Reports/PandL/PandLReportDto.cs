using System.ComponentModel;
using DevExpress.Internal;
using DevExpress.Persistent.Base;

namespace SBD.GL.Module.Reports.PandL
{
    [VisibleInReports]
    [DefaultProperty("Account")]
    public class PandLReportDto  
    {
       public decimal Amount { get; set; }
       public string Account { get; set; }

    }
}