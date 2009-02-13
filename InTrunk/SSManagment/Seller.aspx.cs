using System;
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
                if (!Page.IsPostBack)
                {
                    LoadingTree();
                    btnAdmin.Visible = AppHelper.CurrentUser.isAdmin.Value;
                }
            }
        }

        protected void btnAdminClick(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void treeCategories_SelectedNodeChanged(object sender, EventArgs e)
        {
        	int id;
			if (int.TryParse(((System.Web.UI.WebControls.TreeView)(sender)).SelectedNode.Value, out id))
			{
				gvwProducts.DataSource = item.GetAllByGroupId(id);
				gvwProducts.DataBind();
			}
        }
        private void LoadingTree()
        {
            ssmDataContext cont = new ssmDataContext();
            IList<group> rootCategories = cont.groups.Where(b => b.parent == null).ToList();
            IList<group> rootchild = cont.groups.Where(b => b.parent != null).ToList();

            foreach (group categ in rootCategories)
            {
                TreeNode tree;

                if (categ.parent == null)
                {
                    tree = new TreeNode(categ.name, categ.id.ToString());

                    IList<group> cildNode = rootchild.Where(b => b.parent == categ.id).ToList();

                    if (cildNode.Count > 0)
                    {
                        foreach (group child in cildNode)
                        {
                            tree.ChildNodes.Add(new TreeNode(child.name, child.id.ToString()));
                        }
                        tree.Expanded = false;
                    }
                    treeCategories.Nodes.Add(tree);
                }
            }
        }


    }
}
