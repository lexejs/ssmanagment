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
		private const string ASCENDING = " ASC";
		private const string DESCENDING = " DESC";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (AppHelper.CurrentUser != null)
			{
				if (!Page.IsPostBack)
				{
					LoadingTree();
					LoadingBuyers();
					LoadingShopingCart();
					btnAdmin.Visible = AppHelper.CurrentUser.isAdmin.Value;
					gvwProducts.DataSource = null;
					gvwProducts.DataBind();
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
			if (ShopingCartSession.Count > 0)
			{
				btnBuy.Visible = true;
			}
			else
			{
				btnBuy.Visible = false;
			}

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

		#region Modal window buy confirm

		private void ShowModalReservation(item product, int buyCount)
		{
			modalReserv.Visible = true;
			hdnResrvID.Value = product.id.ToString();
			lblResrvName.Text = product.name;
			lblResrvReserved.Text = product.reserveCount.ToString();
			lblResrvBprice.Text = product.bprice.ToString();
			lblResrvCount.Text = product.count.ToString();
			lblResrvSum.Text = (product.bprice * buyCount).ToString();
			txtResrvBuyCount.Text = buyCount.ToString();
		}

		#endregion

		#region Modal window reservation product

		private void ShowModalBuyConfirm()
		{
			modalBuyConfirm.Visible = true;
		}

		#endregion

		#region Product grid sotring

		private SortDirection GridViewSortDirection
		{
			get
			{
				if (Session["sortDirection"] == null)
					Session["sortDirection"] = SortDirection.Ascending;
				return (SortDirection)ViewState["sortDirection"];
			}
			set { Session["sortDirection"] = value; }
		}

		private void SortGridView(string sortExpression, string direction)
		{
			Session["Products"]
			gvwProducts.DataSource = dv;
			gvwProducts.DataBind();
		}

		private int GetSortColumnIndex()
		{
			foreach (DataControlField field in gvwProducts.Columns)
			{
				if (field.SortExpression ==
							 (string)ViewState["SortExpression"])
				{
					return gvwProducts.Columns.IndexOf(field);
				}
			}
			return -1;
		}

		private void AddSortImage(int columnIndex, GridViewRow headerRow)
		{
			Image sortImage = new Image();
			if (GridViewSortDirection == SortDirection.Ascending)
			{
				sortImage.ImageUrl = "~/images/uparrow.gif";
				sortImage.AlternateText = "Ascending Order";
			}
			else
			{
				sortImage.ImageUrl = "~/images/downarrow.gif";
				sortImage.AlternateText = "Descending Order";
			}
			headerRow.Cells[columnIndex].Controls.Add(sortImage);
		}

		#endregion

		#endregion


		#region Handlers

		protected void btnAdminClick(object sender, EventArgs e)
		{
			Response.Redirect("Admin.aspx");
		}

		protected void btnBuy_Click(object sender, EventArgs e)
		{
			ShowModalBuyConfirm();
		}

		#region Modal window buy confirm

		protected void btnYes_Click(object sender, EventArgs e)
		{
#warning Действия по покупке

			ShopingCartSession = new List<ShopingCart>();
			LoadingShopingCart();

			modalBuyConfirm.Visible = false;
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			modalBuyConfirm.Visible = false;
		}

		#endregion

		#region Modal window reservation product

		protected void btnReservYes_Click(object sender, EventArgs e)
		{
#warning Действия по Резервации продуктов

			modalReserv.Visible = false;
		}

		protected void btnReservNo_Click(object sender, EventArgs e)
		{
			modalReserv.Visible = false;
		}

		protected void txtResrvBuyCount_TextChanged(object sender, EventArgs e)
		{
			int val;
			if (int.TryParse(txtResrvBuyCount.Text, out val))
			{
				lblResrvSum.Text = (Convert.ToUInt32(lblResrvBprice.Text) * val).ToString();
			}
			else
			{
				lblResrvSum.Text = "";
			}
		}

		protected void calResrvReservDateTo_SelectionChanged(object sender, EventArgs e)
		{
			if (calResrvReservDateTo.SelectedDate <= DateTime.Now)
			{
				calResrvReservDateTo.SelectedDate = DateTime.Now;
			}
		}

		#endregion

		protected void treeCategories_SelectedNodeChanged(object sender, EventArgs e)
		{
			int id;
			if (int.TryParse(((TreeView)(sender)).SelectedNode.Value, out id))
			{
				IList<item> itm = item.GetAllByGroupId(id);
				Session["Products"] = itm;
				gvwProducts.DataSource = itm;
				gvwProducts.DataBind();
				
			}
		}

		protected void gvwProducts_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int id;
			if (int.TryParse(e.CommandArgument.ToString(), out id))
			{
				//var db = new ssmDataContext();
				//TableCellCollection cells = ((GridView)sender).Rows[int.Parse(e.CommandArgument.ToString())].Cells;
				//foreach (DataControlFieldCell cell in cells)
				//{
				//    if (cell.ContainingField.HeaderText.ToLower() == "id")
				//    {

				//    }
				//}
#warning Сделать получения колличества покупаемых штук
				int counItemsToBuy = 0;

				item itm = item.GetById(id);
				switch (e.CommandName.ToLower())
				{
					case "add":
						{
							ShopingCartSession.Add(SetShopingCart(itm, counItemsToBuy));
							int sum;
							if (int.TryParse(lblSum.Text, out sum))
								lblSum.Text = (sum + itm.bprice).ToString();
							LoadingShopingCart();

							break;
						}
					case "sale":
						{

							ShopingCartSession.Add(SetShopingCart(itm, counItemsToBuy));
							int sum;
							if (int.TryParse(lblSum.Text, out sum))
								lblSum.Text = (sum + itm.bprice).ToString();
							LoadingShopingCart();
							if (ShopingCartSession.Count == 1)
							{
								btnBuy_Click(new object(), new EventArgs());
							}

							break;
						}
					case "reserved":
						{
							ShowModalReservation(itm, counItemsToBuy);
							break;
						}
				}
			}
		}

		#region Shoping Catr

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
						if (ShopingCartSession.Count <= 0)
						{
							btnBuy.Visible = false;
						}
					}
					LoadingShopingCart();
				}
			}
		}

		protected void gvwShoppingCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			// Необходимо присутствие етого метода для правильного удаление строк из GridView gvwShoppingCart
		}

		#endregion

		#region Product grid sotring

		protected void gvwProducts_Sorting(object sender, GridViewSortEventArgs e)
		{
			string sortExpression = e.SortExpression;
			Session["SortExpression"] = sortExpression;

			if (GridViewSortDirection == SortDirection.Ascending)
			{
				GridViewSortDirection = SortDirection.Descending;
				SortGridView(sortExpression, DESCENDING);
			}
			else
			{
				GridViewSortDirection = SortDirection.Ascending;
				SortGridView(sortExpression, ASCENDING);
			}
		}

		protected void gvwProducts_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.Header)
			{
				int sortColumnIndex = GetSortColumnIndex();
				if (sortColumnIndex != -1)
				{
					AddSortImage(sortColumnIndex, e.Row);
				}
			}
		}

		#endregion

		#endregion










	}
}
