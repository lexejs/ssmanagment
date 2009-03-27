using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSManagment.Models;

namespace SSManagment
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Models.item.CheckForExpiredItems();
        }

        protected void btnLogin_Click1(object sender, EventArgs e)
        {
			AppHelper.TreeSelectedNodePathSession = null;
        	AppHelper.ShopingCartSession = null;
        	AppHelper.ProductsSession = null;

			var sellers = seller.Cache.Where(
        		s =>
        		s.login.ToLower().CompareTo(txtLogin.Text.ToLower()) == 0 &&
        		s.password.ToLower().CompareTo(txtPassword.Text.ToLower()) == 0 && s.isActive.Value).ToList();
        	if (sellers.Count == 1)
        	{
        		AppHelper.CurrentUser = sellers.First();
        		Response.Redirect("Seller.aspx");
				tdPassMsg.Visible = false;
        	}
        	else
        	{
        		tdPassMsg.Visible = true;
        	}
    }
    }
}
