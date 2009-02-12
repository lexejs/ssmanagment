using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSManagment
{
	public partial class Seller : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			gvwProducts.DataSource = Models.item.GetAll();
			gvwProducts.DataBind();

			//treeCategories.DataSource = (IHierarchicalDataSource)db.groups.ToList();
			//treeCategories.DataBind();
			
		}
	}
}
