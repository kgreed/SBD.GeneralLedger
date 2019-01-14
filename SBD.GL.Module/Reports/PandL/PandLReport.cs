using System;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;

using DevExpress.DataAccess;
 
using DevExpress.XtraReports.Configuration;
 

namespace SBD.GL.Module.Reports.PandL
{
    public partial class PandLReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PandLReport()
        {
            InitializeComponent();
            BindToData();
        }
        private void BindToData()
        {
            var fromDate = (DateTime) this.Parameters[0].Value;
            var toDate = (DateTime) this.Parameters[1].Value  ;
            var results = ReportData.PandL(fromDate, toDate);
            DataSource = results;
            //// Create a data source with the required connection parameters.    
            //Access97ConnectionParameters connectionParameters =
            //    new Access97ConnectionParameters("../../Data/nwind.mdb", "", "");
            //SqlDataSource ds = new SqlDataSource(connectionParameters);

            //// Create a query to access fields of the Products data table.  
            //SelectQuery query = SelectQueryFluentBuilder
            //    .AddTable("Products")
            //    .SelectColumns("CategoryID", "ProductName")
            //    .Build("Products");

            //// Add a query parameter to be used as a criterion for data source level data filtering. 
            //// In this example the query parameter has the Expression type and contains 
            //// a simple expression that references a value of a report parameter named "catID". 
            //QueryParameter parameter = new QueryParameter()
            //{
            //    Name = "catID",
            //    Type = typeof(Expression),
            //    Value = new Expression("?catID", typeof(System.Int32))
            //};
            //query.Parameters.Add(parameter);
            //query.FilterString = "CategoryID = ?catID";

            //ds.Queries.Add(query);

            //// Assign the data source to the report. 
            //this.DataSource = ds;
            //this.DataMember = "Products";

            //// Bind report controls to appropriate data fields depending on the report's data binding mode. 
            //if (Settings.Default.UserDesignerOptions.DataBindingMode == DataBindingMode.Bindings)
            //{
            //    xrLabel1.DataBindings.Add("Text", ds, "Products.CategoryID");
            //    xrLabel2.DataBindings.Add("Text", ds, "Products.ProductName");
            //}
            //else
            //{
            //    xrLabel1.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[CategoryID]"));
            //    xrLabel2.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", "[ProductName]"));
            //}
        }
    }
}
