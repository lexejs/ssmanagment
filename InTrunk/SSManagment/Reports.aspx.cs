using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using SSManagment.Models;

namespace SSManagment
{
	public partial class Reports1 : Page
	{
		//private ReportDataSource source;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				lstsellersFill();
			}
		
		}

		protected void drillThrough(object sender, DrillthroughEventArgs e)
		{
			//var localReportViewer = (LocalReport) e.Report;
			//localReportViewer.DataSources.Add(CreateSource());
		}

		protected void ShowReport(object sender, EventArgs e)
		{
      
			var assembly = Assembly.GetExecutingAssembly();
			reportViewer1.LocalReport.DataSources.Clear();
			reportViewer1.LocalReport.DataSources.Add(CreateSource());
			reportViewer1.LocalReport.ReportEmbeddedResource = string.Format("{0}.Reports.SaleReport.rdlc",
																																			 assembly.GetName().Name);
			reportViewer1.LocalReport.Refresh();
		}

		private void lstsellersFill()
		{
			var db = new ssmDataContext();
			var list = db.sellers.OrderBy(g => g.fullName).ToList();
			list.Insert(0, new seller {fullName = "Все"});
			lstSellers.DataSource = list;
			lstSellers.DataTextField = "fullName";
			lstSellers.DataValueField = "id";
			lstSellers.DataBind();
		}

		ReportDataSource CreateSource()
		{
			var db = new ssmDataContext();
			IEnumerable<SalesDataSource> list = db.logSales
				.Join(db.buyers, l => l.buyerId, b => b.id, (l, b) => new { logSales = l, Buyer = b })
				.Join(db.sellers, l => l.logSales.sellerId, s => s.id, (l, s) => new { logSalesB = l, Seller = s })
				.Where(s => lstSellers.SelectedValue == null || int.Parse(lstSellers.SelectedValue) ==0|| s.Seller.id == int.Parse(lstSellers.SelectedValue))
				.Select(s => new SalesDataSource
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