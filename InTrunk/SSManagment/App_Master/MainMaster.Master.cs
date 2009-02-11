using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSManagment.App_Master
{
	public partial class Main : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Models.AppHelper.CurrentUser == null)
				Response.Redirect("Login.aspx");
		}

		protected void lnkLogOut_Click(object sender, EventArgs e)
		{
			Models.AppHelper.CurrentUser = null;
			Response.Redirect("Login.aspx");
		}
	}
}
