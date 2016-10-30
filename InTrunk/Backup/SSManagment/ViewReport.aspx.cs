using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using SSManagment.Models;

namespace SSManagment
{
	public partial class ViewReport : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (AppHelper.CurrentUser == null || !AppHelper.CurrentUser.isAdmin.GetValueOrDefault(false))
			{
				Response.Redirect("Login.aspx");
			}
			if (!Page.IsPostBack)
			{
				ComboBoxesFill();
			}
			if (string.IsNullOrEmpty(txtDateTo.Text))
				txtDateTo.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
			if (string.IsNullOrEmpty(txtDateFrom.Text))
				txtDateFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
		}

		protected void drillThrough(object sender, DrillthroughEventArgs e)
		{
			//var localReportViewer = (LocalReport) e.Report;
			//localReportViewer.DataSources.Add(CreateSource());
		}

		protected void ShowReport(object sender, EventArgs e)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			reportViewer1.LocalReport.DataSources.Clear();
			reportViewer1.LocalReport.DataSources.Add(CreateSource());
			reportViewer1.LocalReport.ReportEmbeddedResource = string.Format("{0}.Reports.SaleReport.rdlc", assembly.GetName().Name);
			//reportViewer1.LocalReport.Refresh();
		}

		private void ComboBoxesFill()
		{
			List<seller> list = seller.Cache.OrderBy(g => g.fullName).ToList();
			list.Insert(0, new seller { fullName = "Все" });
			lstSellers.DataSource = list;
			lstSellers.DataTextField = "fullName";
			lstSellers.DataValueField = "id";
			lstSellers.DataBind();

			List<buyer> blist = buyer.Cache.OrderBy(b => b.name).ToList();
			blist.Insert(0, new buyer { name = "Все" });
			drpBuyers.DataSource = blist;
			drpBuyers.DataValueField = "id";
			drpBuyers.DataTextField = "name";
			drpBuyers.DataBind();

		}

		private ReportDataSource CreateSource()
		{
			int sellerId = int.Parse(lstSellers.SelectedValue);
			int buyerId = int.Parse(drpBuyers.SelectedValue);
			bool isGiveBack = chkIsGiveBack.Checked;
			bool dateOk = false;
			var db = new ssmDataContext();
			var fromDate = DateTime.Now.AddDays(-1);
			var toDate = DateTime.Now;

			if (!string.IsNullOrEmpty(txtDateFrom.Text.Trim()))
			{
				if (!DateTime.TryParse(txtDateFrom.Text, out fromDate))
				{
					fromDate = DateTime.Now.AddDays(-1);
				}
			}

			if (!string.IsNullOrEmpty(txtDateTo.Text.Trim()))
			{
				if (!DateTime.TryParse(txtDateTo.Text, out toDate))
				{
					toDate = DateTime.Now;
				}

			}
			lblMessage.Text = "Выполнено за даты с " + fromDate.ToString("yyyy-MM-dd") + " по " + toDate.ToString("yyyy-MM-dd");

			IEnumerable<SalesDataSource> list = db.logSales.ToList()
					.Join(buyer.Cache, l => l.buyerId, b => b.id, (l, b) => new { logSales = l, Buyer = b })
					.Join(seller.Cache, l => l.logSales.sellerId, s => s.id, (l, s) => new { logSalesB = l, Seller = s })

	.Where(
					s =>
					(sellerId == 0 || s.logSalesB.logSales.sellerId == sellerId)
					&&
					(buyerId == 0 || s.logSalesB.logSales.buyerId == buyerId)
					&&
					(s.logSalesB.logSales.isGiveBack == isGiveBack)
					&&
					(s.logSalesB.logSales.date.GetValueOrDefault() <= toDate && s.logSalesB.logSales.date.GetValueOrDefault() >= fromDate)

					)

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