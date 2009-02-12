using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSManagment
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click1(object sender, EventArgs e)
        {
            var db = new Models.ssmDataContext();
            var sellers = db.sellers.Where(
                s =>
                s.login.ToLower().CompareTo(txtLogin.Text.ToLower()) == 0 &&
                s.password.ToLower().CompareTo(txtPassword.Text.ToLower()) == 0).ToList();
            if (sellers.Count == 1)
            {
                Models.AppHelper.CurrentUser = sellers.First();
                Response.Redirect("Seller.aspx");
            }
        }
    }
}
