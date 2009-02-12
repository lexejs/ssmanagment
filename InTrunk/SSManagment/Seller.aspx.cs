﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSManagment.Models;

namespace SSManagment
{
	public partial class Seller : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (AppHelper.CurrentUser != null)
			{
				gvwProducts.DataSource = item.GetAll();
				gvwProducts.DataBind();

				
				btnAdmin.Visible = AppHelper.CurrentUser.isAdmin.Value;
				//treeCategories.DataSource = (IHierarchicalDataSource)db.groups.ToList();
				//treeCategories.DataBind();
			}
		}
		protected void btnAdminClick(object sender, EventArgs e)
		{
			Response.Redirect("Admin.aspx");
		}
	}
}
