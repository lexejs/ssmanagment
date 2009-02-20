using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSManagment.Models;
using Image = System.Web.UI.WebControls.Image;

namespace SSManagment
{
	public class ShopingCart : item
	{
		public int BuyCount { get; set; }
		public double? ResultPrice { get; set; }
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
					LoadingShopingCart();
					btnAdmin.Visible = AppHelper.CurrentUser.isAdmin.Value;
					gvwProducts.DataSource = null;
					gvwProducts.DataBind();
				}
			}
		}

		#region Properties

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

		#endregion

		#region Methods

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
			shop.ResultPrice = shop.BuyCount * shop.bprice;

			return shop;
		}

		private void LoadProductsGridView()
		{
			Session["Products"] = item.GetAllByGroupId(Convert.ToInt32(treeCategories.SelectedNode.Value));
			gvwProducts.DataSource = Session["Products"];
			gvwProducts.DataBind();
		}

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

		protected void treeCategories_SelectedNodeChanged(object sender, EventArgs e)
		{
			int id;
			if (int.TryParse(((TreeView)(sender)).SelectedNode.Value, out id))
			{
				((TreeView)(sender)).SelectedNode.Expand();
				IList<item> itm = item.GetAllByGroupId(id);
				Session["Products"] = itm;
				gvwProducts.DataSource = itm;
				gvwProducts.DataBind();
				gvwProducts.Sort("name", SortDirection.Ascending);
			}
		}

		protected void gvwProducts_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int id;
			if (int.TryParse(e.CommandArgument.ToString(), out id))
			{
				item itm = item.GetById(id);

				switch (e.CommandName.ToLower())
				{
					case "unreserv":
						{
							ShowModalUnReservConfirm(itm.id);

							break;
						}
					case "order":
						{
							ShowModalOrderConfirm(itm.id);

							break;
						}
					default:
						{
							int counItemsToBuy = 0;
							int.TryParse(((TextBox)(((Control)(e.CommandSource)).FindControl("txtBuyCount"))).Text, out counItemsToBuy);
							((TextBox)(((Control)(e.CommandSource)).FindControl("txtBuyCount"))).Text = "";

							if (counItemsToBuy > 0 && itm.count > 0)
							{
								if (counItemsToBuy <= (itm.count - (itm.reserveCount ?? 0)))
								{
									switch (e.CommandName.ToLower())
									{
										case "add":
											{
												ShopingCartSession.Add(SetShopingCart(itm, counItemsToBuy));
												lblSum.Text = ShopingCartSession.Sum(b => b.ResultPrice).ToString();
												LoadingShopingCart();
												((Control)(e.CommandSource)).FindControl("ibtnAdd").Visible = false;
												((Control)(e.CommandSource)).FindControl("ibtnSale").Visible = false;

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
												((Control)(e.CommandSource)).FindControl("ibtnAdd").Visible = false;
												((Control)(e.CommandSource)).FindControl("ibtnSale").Visible = false;

												break;
											}
										case "reserved":
											{
												ShowModalReservation(itm, counItemsToBuy);
												break;
											}
									}
								}
								else
								{
									((TextBox)(((Control)(e.CommandSource)).FindControl("txtBuyCount"))).Text = "";
								}

							}
						}
						break;
				}

			}
			else
			{
				((TextBox)(((Control)(e.CommandSource)).FindControl("txtBuyCount"))).Text = "";
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
						if (ShopingCartSession.Count <= 0)
						{
							btnBuy.Visible = false;
						}
						gvwProducts.DataSource = Session["Products"];
						gvwProducts.DataBind();
					}
					LoadingShopingCart();
				}
			}
		}
#warning Что то придумать с отображение конечной стоимости указанного колличества товаров в gvwShopingCart
		protected void gvwShoppingCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			// Необходимо присутствие етого метода для правильного удаление строк из GridView gvwShoppingCart
		}

		protected void gvwProducts_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (ShopingCartSession.Any(b => b.id == ((item)(e.Row.DataItem)).id))
				{
					e.Row.FindControl("ibtnAdd").Visible = false;
					e.Row.FindControl("ibtnSale").Visible = false;
				}

				if (((item)(e.Row.DataItem)).count <= 0 || (((item)(e.Row.DataItem)).count <= ((item)(e.Row.DataItem)).reserveCount))
				{
					e.Row.FindControl("ibtnAdd").Visible = false;
					e.Row.FindControl("ibtnSale").Visible = false;
					e.Row.FindControl("ibtnReserv").Visible = false;
					e.Row.FindControl("txtBuyCount").Visible = false;
				}

				if (((item)(e.Row.DataItem)).reserveCount == null || ((item)(e.Row.DataItem)).reserveCount <= 0)
				{
					e.Row.FindControl("spanCountCalc").Visible = false;
				}
				else
				{
					e.Row.FindControl("spanCountCalc").Visible = true;
					((System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("spanSum")).InnerText = (((item)(e.Row.DataItem)).count - ((item)(e.Row.DataItem)).reserveCount).ToString();
				}
			}
		}

		protected void txtBuyCount_Click(object sender, EventArgs e)
		{
			ShopingCart shop =
					ShopingCartSession.FirstOrDefault(
						b => b.id == Convert.ToInt32(((Label)((Control)(sender)).FindControl("lblID")).Text));
			int count;
			if (int.TryParse(((TextBox)sender).Text,out count))
			{
				if (shop != null)
				{
					if (count <= (shop.count - (shop.reserveCount ?? 0)))
					{
						shop.BuyCount = count;
						shop.ResultPrice = shop.BuyCount*shop.bprice;
						LoadingShopingCart();
					}
					else
					{
						((TextBox) sender).Text = shop.BuyCount.ToString();
					}
				}
			}
			else
			{
				((TextBox)sender).Text = shop.BuyCount.ToString();
			}


		}

		#endregion

		#region Modal window UnReserv Product

		#region Methods

		private void ShowModalUnReservConfirm(int id)
		{
			hdnUnReservId.Value = id.ToString();
			modalUnReservConfirm.Visible = true;
		}

		#endregion

		#region Hendlers

		protected void btnUnReservYes_Click(object sender, EventArgs e)
		{
			item.UnReservForItemId(Convert.ToInt32(hdnUnReservId.Value));
			LoadProductsGridView();

			modalUnReservConfirm.Visible = false;
		}

		protected void btnUnReservNo_Click(object sender, EventArgs e)
		{
			modalUnReservConfirm.Visible = false;
		}

		#endregion

		#endregion

		#region Modal window Order Product

		#region Methods

		private void ShowModalOrderConfirm(int id)
		{
			hdnOrder.Value = id.ToString();
			modalOrderConfirm.Visible = true;
		}

		#endregion

		#region Hendlers

		protected void btnOrderYes_Click(object sender, EventArgs e)
		{
#warning Сделать функционал заказа
			//item.OrderItemId(Convert.ToInt32(hdnOrder.Value));

			modalOrderConfirm.Visible = false;
		}

		protected void btnOrderNo_Click(object sender, EventArgs e)
		{
			modalOrderConfirm.Visible = false;
		}

		#endregion

		#endregion

		#region Modal window buy confirm

		#region Methods

		private void ShowModalBuyConfirm()
		{
			lblShopConfirmSum.Text = ShopingCartSession.Sum(b => b.ResultPrice).ToString();
			gvwShpingCartConfirm.DataSource = ShopingCartSession;
			gvwShpingCartConfirm.DataBind();
			modalBuyConfirm.Visible = true;
		}

		#endregion

		#region Hendlers

		protected void btnYes_Click(object sender, EventArgs e)
		{
#warning Действия по покупке

			ShopingCartSession = new List<ShopingCart>();
			LoadingShopingCart();
			gvwProducts.DataSource = Session["Products"];
			gvwProducts.DataBind();

			modalBuyConfirm.Visible = false;
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			modalBuyConfirm.Visible = false;
		}

		#endregion

		#endregion

		#region Modal window reservation product

		#region Methods

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
			calResrvReservDateTo.SelectedDate = DateTime.Now;
		}

		#endregion

		#region Hendlers

		protected void btnReservYes_Click(object sender, EventArgs e)
		{
			int count = 0;
			if (int.TryParse(txtResrvBuyCount.Text, out count) && count > 0)
			{
				if (item.ReservItem(Convert.ToInt32(hdnResrvID.Value), count, calResrvReservDateTo.SelectedDate))
				{
					LoadProductsGridView();
				}
				else
				{
					ShowWarningConfirm("Неудалось выполнить операцию резервирования продукта, попробуйте еще раз. ");
				}
			}

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

		#endregion

		#region Modal window Warning

		#region Methods

		private void ShowWarningConfirm(string msg)
		{
			lblWarning.Text = msg;
			modalWarningConfirm.Visible = true;
		}

		#endregion

		#region Hendlers

		protected void btnWarningOk_Click(object sender, EventArgs e)
		{
			modalWarningConfirm.Visible = false;
		}

		#endregion

		#endregion

#warning Обработать сортировку по умолчанию при первой загрузке продуктов, каждый раз сортирует в разную сторану
		#region Product grid sotring

		#region Methods

		private SortDirection GridViewSortDirection
		{
			get
			{
				if (Session["sortDirection"] == null)
					Session["sortDirection"] = SortDirection.Ascending;
				return (SortDirection)Session["sortDirection"];
			}
			set { Session["sortDirection"] = value; }
		}

		private void SortGridView(string sortExpression, SortDirection direction)
		{
			if (direction.Equals(SortDirection.Ascending))
			{
				switch (sortExpression)
				{
					case "name":
						{
							Session["Products"] = ((IList<item>)Session["Products"]).OrderBy(b => b.name).ToList();
							break;
						}
					case "count":
						{
							Session["Products"] = ((IList<item>)Session["Products"]).OrderBy(b => b.count).ToList();
							break;
						}
					case "measure":
						{
							Session["Products"] = ((IList<item>)Session["Products"]).OrderBy(b => b.measure).ToList();
							break;
						}
					case "bprice":
						{
							Session["Products"] = ((IList<item>)Session["Products"]).OrderBy(b => b.bprice).ToList();
							break;
						}
					case "reserveCount":
						{
							Session["Products"] = ((IList<item>)Session["Products"]).OrderBy(b => b.reserveCount).ToList();
							break;
						}
				}
			}
			else
			{
				switch (sortExpression)
				{
					case "name":
						{
							Session["Products"] = ((IList<item>)Session["Products"]).OrderByDescending(b => b.name).ToList();
							break;
						}
					case "count":
						{
							Session["Products"] = ((IList<item>)Session["Products"]).OrderByDescending(b => b.count).ToList();
							break;
						}
					case "measure":
						{
							Session["Products"] = ((IList<item>)Session["Products"]).OrderByDescending(b => b.measure).ToList();
							break;
						}
					case "bprice":
						{
							Session["Products"] = ((IList<item>)Session["Products"]).OrderByDescending(b => b.bprice).ToList();
							break;
						}
					case "reserveCount":
						{
							Session["Products"] = ((IList<item>)Session["Products"]).OrderByDescending(b => b.reserveCount).ToList();
							break;
						}
				}
			}

			gvwProducts.DataSource = Session["Products"];
			gvwProducts.DataBind();
		}

		private int GetSortColumnIndex()
		{

			foreach (DataControlField field in gvwProducts.Columns)
			{
				if (field.SortExpression ==
							 (string)Session["SortExpression"])
				{
					return gvwProducts.Columns.IndexOf(field);
				}
			}
			return -1;
		}

		private void AddSortImage(int columnIndex, GridViewRow headerRow)
		{
			Image sortImage = new Image();
			sortImage.Width = 16;
			sortImage.Height = 16;
			sortImage.Visible = true;

			if (GridViewSortDirection == SortDirection.Ascending)
			{
				sortImage.ImageUrl = "~/App_Themes/Main/SortOrder/16px-Up.png";
				sortImage.AlternateText = "По возрастанию";
			}
			else
			{
				sortImage.ImageUrl = "~/App_Themes/Main/SortOrder/16px-Down.png";
				sortImage.AlternateText = "По убыванию";
			}
			headerRow.Cells[columnIndex].Controls.Add(sortImage);
		}

		#endregion

		#region Handlers

		protected void gvwProducts_Sorting(object sender, GridViewSortEventArgs e)
		{
			if (e.SortExpression.Equals(Session["SortExpression"]))
			{
				Session["SortExpression"] = e.SortExpression;
				if (GridViewSortDirection == SortDirection.Ascending)
				{
					GridViewSortDirection = SortDirection.Descending;
					SortGridView(e.SortExpression, SortDirection.Descending);
				}
				else
				{
					GridViewSortDirection = SortDirection.Ascending;
					SortGridView(e.SortExpression, SortDirection.Ascending);
				}
			}
			else
			{
				Session["SortExpression"] = e.SortExpression;
				GridViewSortDirection = SortDirection.Ascending;
				SortGridView(e.SortExpression, SortDirection.Ascending);
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
