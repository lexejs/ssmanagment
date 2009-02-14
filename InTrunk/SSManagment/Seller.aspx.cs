using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSManagment.Models;

namespace SSManagment
{
    public class ShopingCart
    {
        public IList<item> Product;
        public int BuyCount;

    }

    public partial class Seller : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (AppHelper.CurrentUser != null)
            {
                if (!Page.IsPostBack)
                {
                    LoadingTree();
                    LoadingBuyers();
                    btnAdmin.Visible = AppHelper.CurrentUser.isAdmin.Value;
                }
            }
        }

        #region Methods

        private ShopingCart ShopingCartSession
        {
            get { return ((ShopingCart)Session["ShopingCartItems"]); }
            set { Session["ShopingCartItems"] = value; }
        }

        private void LoadingBuyers()
        {
            var db = new ssmDataContext();
            drpBuyer.DataSource = db.buyers.Where(b => b.isActive.HasValue && b.isActive.Value).ToList();
            drpBuyer.DataTextField = "name";
            drpBuyer.DataValueField = "id";
            drpBuyer.DataBind();
        }

        private void LoadingTree()
        {
            ssmDataContext cont = new ssmDataContext();
            IList<group> rootCategories = cont.groups.Where(b => b.parent == null).OrderBy(b => b.name).ToList();
            IList<group> rootchild = cont.groups.Where(b => b.parent != null).OrderBy(b => b.name).ToList();

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

        #endregion

        #region Handlers



        protected void btnAdminClick(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {

        }

        protected void drpBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!((ListControl)(sender)).SelectedValue.Equals("0"))
            {
                btnBuy.Visible = true;
            }
            else
            {
                btnBuy.Visible = false;
            }

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

        protected void gvwProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var db = new ssmDataContext();
            TableCellCollection cells = ((GridView)sender).Rows[int.Parse(e.CommandArgument.ToString())].Cells;
            foreach (DataControlFieldCell cell in cells)
            {
                if (cell.ContainingField.HeaderText.ToLower() == "id")
                {

                }
            }



            switch (e.CommandName.ToLower())
            {
                case "add":
                    {

                        break;
                    }
                case "sale":
                    {
                        break;
                    }
                case "reserved":
                    {
                        break;
                    }
            }
        }

        #endregion

        protected void gvwProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    }
}
