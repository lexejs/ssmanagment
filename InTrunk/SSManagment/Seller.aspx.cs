using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSManagment.Models;

namespace SSManagment
{
    public class ShopingCart : item
    {
        public int BuyCount { get; set; }
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
                    gvwProducts.DataSource = null;
                    gvwProducts.DataBind();
                    gvwShoppingCart.DataSource = null;
                    gvwShoppingCart.DataBind();
                }
            }
        }

        #region Methods

        private IList<ShopingCart> ShopingCartSession
        {
            get
            {
                if (Session["ShopingCartItems"] == null)
                {
                    Session["ShopingCartItems"] = new List<ShopingCart>();
                }
                return ((IList<ShopingCart>)Session["ShopingCartItems"]);

            }
            set { Session["ShopingCartItems"] = value; }
        }

        private bool isBuyerSelected
        {
            get
            {
                if (drpBuyer.SelectedValue.Equals("1"))
                {
                    return false;
                }
                return true;
            }
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

        private void LoadingShopingCart()
        {
            gvwShoppingCart.DataSource = ShopingCartSession;
            gvwShoppingCart.DataBind();
        }

        private static ShopingCart SetShopingCart(item product, int buyCount)
        {
            ShopingCart shop = new ShopingCart();
            shop.adminPrice = product.adminPrice;
            shop.bprice = product.bprice;
            shop.canGiveBack = product.canGiveBack;
            shop.count = product.count;
            shop.countToOrder = product.countToOrder;
            shop.group = product.group;
            shop.groupId = product.groupId;
            shop.id = product.id;
            shop.isActive = product.isActive;
            shop.logSales = product.logSales;
            shop.measure = product.measure;
            shop.name = product.name;
            shop.order = product.order;
            shop.pct = product.pct;
            shop.price = product.price;
            shop.reserveCount = product.reserveCount;
            shop.reserveEndDate = product.reserveEndDate;

            shop.BuyCount = buyCount;

            return shop;
        }

        #endregion

        #region Handlers



        protected void btnAdminClick(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void btnBuy_Click(object sender, EventArgs e)
        {
            gvwShoppingCart.DataSource = null;
            gvwShoppingCart.DataBind();
            ShopingCartSession = new List<ShopingCart>();
        }

        protected void treeCategories_SelectedNodeChanged(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(((TreeView)(sender)).SelectedNode.Value, out id))
            {
                gvwProducts.DataSource = item.GetAllByGroupId(id);
                gvwProducts.DataBind();
            }
        }

        protected void gvwProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            if (int.TryParse(e.CommandArgument.ToString(), out id))
            {
                var db = new ssmDataContext();
                TableCellCollection cells = ((GridView)sender).Rows[int.Parse(e.CommandArgument.ToString())].Cells;
                foreach (DataControlFieldCell cell in cells)
                {
                    if (cell.ContainingField.HeaderText.ToLower() == "id")
                    {

                    }
                }
                item itm = item.GetById(id);
                switch (e.CommandName.ToLower())
                {
                    case "add":
                        {
                            ShopingCartSession.Add(SetShopingCart(itm, 0));
                            int sum;
                            if (int.TryParse(lblSum.Text, out sum))
                                lblSum.Text = (sum + itm.bprice).ToString();
                            LoadingShopingCart();

                            break;
                        }
                    case "sale":
                        {
                            ShopingCartSession.Add(SetShopingCart(itm, 0));
                            int sum;
                            if (int.TryParse(lblSum.Text, out sum))
                                lblSum.Text = (sum + itm.bprice).ToString();
                            LoadingShopingCart();
                            if (isBuyerSelected)
                            {
                                btnBuy_Click(new object(), new EventArgs());
                            }
                            break;
                        }
                    case "reserved":
                        {
                            break;
                        }
                }
            }
        }

        protected void gvwShoppingCart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id;
            if (int.TryParse(e.CommandArgument.ToString(), out id))
            {
                if (e.CommandName.ToLower() == "delete")
                {
                    ShopingCart shop = ShopingCartSession.FirstOrDefault(b => b.id == id);
                    if (shop != null)
                    {
                        int sum;
                        if (int.TryParse(lblSum.Text, out sum))
                            lblSum.Text = (sum - shop.bprice).ToString();
                        ShopingCartSession.Remove(shop);
                    }
                    LoadingShopingCart();
                }
            }
        }

        #endregion
    }
}
