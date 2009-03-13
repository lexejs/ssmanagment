using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SSManagment.Models;

namespace SSManagment
{
	public partial class Reports : System.Web.UI.Page
	{
		private ReportDataSource source;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (AppHelper.CurrentUser == null || (AppHelper.CurrentUser != null && !AppHelper.CurrentUser.isAdmin.GetValueOrDefault(false)))
			{
				Response.Redirect("Default.aspx", true);
			}
		}

		protected void ReportViewer_Drillthrough(object sender, DrillthroughEventArgs e)
		{
			var localReportViewer = (LocalReport)e.Report;
			localReportViewer.DataSources.Add(source);
		}

		protected void ShowButtonClick(object sender, EventArgs e)
		{
			
		}
	}
}
