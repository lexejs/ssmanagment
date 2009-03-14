using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace SSManagment
{
	public partial class Reports1 : System.Web.UI.Page
	{
		private ReportDataSource source;

		protected void Page_Load(object sender, EventArgs e)
		{
			var db = new Models.ssmDataContext();
			var assembly = Assembly.GetExecutingAssembly();
			IEnumerable<SalesDataSource> list = db.logSales.Select(s => new SalesDataSource() { Buyer = s.buyerId.ToString(), Cash = s.cash.GetValueOrDefault(), Date = s.date.GetValueOrDefault(), IsGiveBack = s.isGiveBack.GetValueOrDefault(), Item = s.logName, ItemsCount = s.itemsCount.GetValueOrDefault(0), Seller = s.sellerId.ToString(), SID = s.sid.GetValueOrDefault() }).ToList();
			source = new ReportDataSource("SalesDataSource", list);
			reportViewer1.LocalReport.DataSources.Add(source);
			reportViewer1.LocalReport.ReportEmbeddedResource = string.Format("{0}.Reports.SaleReport.rdlc", assembly.GetName().Name);
									
		}

		protected void drillThrough(object sender, DrillthroughEventArgs e)
		{
			var localReportViewer = (LocalReport)e.Report;
			localReportViewer.DataSources.Add(source);

		}
	}
}
