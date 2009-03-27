using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using SSManagment.Models;

namespace SSManagment
{
	public partial class CurrentSale : Page
	{
		private int sid;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (AppHelper.CurrentUser == null)
			{
				Response.Redirect("Login.aspx", true);
			}
			else
			{
				ShowReport();
			}

		}


		protected void ShowReport()
		{
			if (!int.TryParse(Request.QueryString["SID"], out sid))
			{
				sid = new ssmDataContext().logSales.Where(ls => ls.isGiveBack == false).OrderByDescending(ls=>ls.date).First().sid.Value;
			}

			Assembly assembly = Assembly.GetExecutingAssembly();
			reportViewer1.LocalReport.DataSources.Clear();
			reportViewer1.LocalReport.DataSources.Add(CreateSource(sid));
			reportViewer1.LocalReport.ReportEmbeddedResource = string.Format("{0}.Reports.CurrentSale.rdlc", assembly.GetName().Name);
			//reportViewer1.LocalReport.Refresh();
		}


		private ReportDataSource CreateSource(int sID)
		{

			var db = new ssmDataContext();


			IEnumerable<SalesDataSource> list = db.logSales
				.Join(buyer.Cache, l => l.buyerId, b => b.id, (l, b) => new { logSales = l, Buyer = b })
				.Join(seller.Cache, l => l.logSales.sellerId, s => s.id, (l, s) => new { logSalesB = l, Seller = s })
				.Where(
				s =>
				(s.logSalesB.logSales.sid.Value == sID && s.logSalesB.logSales.isGiveBack == false)

				).Select(s => new SalesDataSource
								 {
									 Buyer = s.logSalesB.Buyer.name,
									 Cash = s.logSalesB.logSales.cash.GetValueOrDefault(),
									 Date = s.logSalesB.logSales.date.GetValueOrDefault(),
									 IsGiveBack = s.logSalesB.logSales.isGiveBack.GetValueOrDefault(),
									 Item = s.logSalesB.logSales.logName,
									 ItemsCount = s.logSalesB.logSales.itemsCount.GetValueOrDefault(0),
									 Seller = s.Seller.fullName,
									 SID = s.logSalesB.logSales.sid.GetValueOrDefault()
								 }).ToList();

			return new ReportDataSource("SalesDataSource", list);
		}
	}
}