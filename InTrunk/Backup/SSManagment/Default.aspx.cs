using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSManagment
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
					//if (Models.AppHelper.CurrentUser != null)
						Response.Redirect("Seller.aspx");
        }
    }
}
