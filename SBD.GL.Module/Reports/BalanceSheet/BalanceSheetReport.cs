using System;
using DevExpress.XtraReports.Parameters;
using SBD.GL.Module.Reports.PandL;

namespace SBD.GL.Module.Reports.BalanceSheet
{
    public partial class BalanceSheetReport : DevExpress.XtraReports.UI.XtraReport
    {
        public BalanceSheetReport()
        {
            InitializeComponent();
            InitializeParameters();
        }

        private void InitializeParameters()
        {
            if ((DateTime) Parameters[0].Value == DateTime.MinValue)
            {
                var date = DateTime.Today;
              //  var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                Parameters[0].Value = date;
            }

            
            
           
        }

        protected override void OnParametersRequestSubmit(ParametersRequestEventArgs e)
        {
            BindToData();
            base.OnParametersRequestSubmit(e);
        }

        private void BindToData()
        {
            var asAtDate = (DateTime) this.Parameters[0].Value;
            this.xrLabelHeading.Text = $"Balance Sheet Report {asAtDate.ToShortDateString()}";
            var results = BalanceSheetData.BalanceSheet(asAtDate);
            DataSource = results;
        }
    }
}
