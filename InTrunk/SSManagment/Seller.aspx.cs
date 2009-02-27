using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SSManagment.Models;
using Image = System.Web.UI.WebControls.Image;

namespace SSManagment
{
	public class ShopingCart : item
	{
		public ShopingCart(item product, int buyCount)
		{
			adminPrice = product.adminPrice;
			bprice = product.bprice;
			canGiveBack = product.canGiveBack;
			count = product.count;
			countToOrder = product.countToOrder;
			group = product.group;
			groupId = product.groupId;
			id = product.id;
			isActive = product.isActive;
			logSales = product.logSales;
			measure = product.measure;
			name = product.name;
			order = product.order;
			pct = product.pct;
			price = product.price;
			reserveCount = product.reserveCount;
			reserveEndDate = product.reserveEndDate;

			BuyCount = buyCount;
		}

		public int BuyCount { get; set; }
		public double? ResultPrice
		{
			get
			{
				return (BuyCount * bprice);
			}
		}
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

		private IList<item> ProductsSession
		{
			get
			{
				if (Session["Products"] == null)
				{
					Session["Products"] = new List<item>();
				}
				return ((IList<item>)Session["Products"]);

			}
			set { Session["Products"] = value; }
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

			drpSingleBuyBuyerList.DataSource = db.buyers.Where(b => b.isActive.HasValue && b.isActive.Value).ToList();
			drpSingleBuyBuyerList.DataTextField = "name";
			drpSingleBuyBuyerList.DataValueField = "id";
			drpSingleBuyBuyerList.DataBind();
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

#warning При изменении покупателя в дропе пересчитывать итого
			CalcShopingCartSum(lblSum, spanShopingCartSum);

			gvwShoppingCart.DataSource = ShopingCartSession;
			gvwShoppingCart.DataBind();
		}

		private void LoadProductsGridView()
		{
			ProductsSession = item.GetAllByGroupId(Convert.ToInt32(treeCategories.SelectedNode.Value));
			gvwProducts.DataSource = ProductsSession;
			gvwProducts.Sort("name", SortDirection.Ascending);
			gvwProducts.DataBind();
		}

		private void CalcShopingCartSum(Label lblTmp, HtmlGenericControl spanTmp)
		{
			if (ShopingCartSession != null || ShopingCartSession.Count > 0)
			{
				spanTmp.Visible = true;
#warning Округлять сумму итого десятков в большую сторану
#warning  * (1-Buyer.pct)
				lblTmp.Text = ShopingCartSession.Sum(b => b.ResultPrice).ToString();
			}
			else
			{
				spanTmp.Visible = false;
				lblTmp.Text = "0";
			}
		}
		#endregion


		#region Handlers

		protected void btnAdminClick(object sender, EventArgs e)
		{
			Response.Redirect("Admin.aspx");
		}

		protected void btnFind_Click(object sender, EventArgs e)
		{
			ProductsSession = item.FindByName(txtFind.Text);
			gvwProducts.DataSource = ProductsSession;
			gvwProducts.Sort("name", SortDirection.Ascending);
			gvwProducts.DataBind();
		}

		protected void btnBuy_Click(object sender, EventArgs e)
		{
			ShowModalBuyConfirm();
		}

		protected void btnDemand_Click(object sender, EventArgs e)
		{
			ShowDemand();
		}

		protected void btnReturn_Click(object sender, EventArgs e)
		{
			ShowReturn();
		}

		protected void treeCategories_SelectedNodeChanged(object sender, EventArgs e)
		{
			int id;
			if (int.TryParse(((TreeView)(sender)).SelectedNode.Value, out id))
			{
				((TreeView)(sender)).SelectedNode.Expand();
				IList<item> itm = item.GetAllByGroupId(id);
				ProductsSession = itm;
				gvwProducts.DataSource = itm;
				gvwProducts.Sort("name", SortDirection.Ascending);
				gvwProducts.DataBind();
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
							int counItemsToBuy = 1;

							if (counItemsToBuy > 0 && itm.count > 0)
							{
								if (counItemsToBuy <= (itm.count - (itm.reserveCount ?? 0)))
								{
									switch (e.CommandName.ToLower())
									{
										case "add":
											{
												ShopingCartSession.Add(new ShopingCart(itm, counItemsToBuy));
												LoadingShopingCart();
												((Control)(e.CommandSource)).FindControl("ibtnAdd").Visible = false;
												((Control)(e.CommandSource)).FindControl("ibtnSale").Visible = false;
												((Control)(e.CommandSource)).FindControl("ibtnReserv").Visible = false;

												break;
											}
										case "sale":
											{
												ShopingCartSession.Add(new ShopingCart(itm, counItemsToBuy));
												LoadingShopingCart();
												if (ShopingCartSession.Count == 1)
												{
													ShowModalSingleBuyConfirm();
												}
												((Control)(e.CommandSource)).FindControl("ibtnAdd").Visible = false;
												((Control)(e.CommandSource)).FindControl("ibtnSale").Visible = false;
												((Control)(e.CommandSource)).FindControl("ibtnReserv").Visible = false;

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
						}
						break;
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
						ShopingCartSession.Remove(shop);
						if (ShopingCartSession.Count <= 0)
						{
							
							btnBuy.Visible = false;
						}
						gvwProducts.DataSource = ProductsSession;
						gvwProducts.DataBind();

					}
					LoadingShopingCart();
				}
			}
		}

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
					e.Row.FindControl("ibtnReserv").Visible = false;
				}

				if (((item)(e.Row.DataItem)).count <= 0 || (((item)(e.Row.DataItem)).count <= ((item)(e.Row.DataItem)).reserveCount))
				{
					e.Row.FindControl("ibtnAdd").Visible = false;
					e.Row.FindControl("ibtnSale").Visible = false;
					e.Row.FindControl("ibtnReserv").Visible = false;
				}

				if (((item)(e.Row.DataItem)).order == true)
				{
					e.Row.FindControl("lbtnOrder").Visible = false;
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

		protected void txtBuyCount_TextChanged(object sender, EventArgs e)
		{
			ShopingCart shop =
					ShopingCartSession.FirstOrDefault(
						b => b.id == Convert.ToInt32(((Label)((Control)(sender)).FindControl("lblID")).Text));
			int count;
			if (int.TryParse(((TextBox)sender).Text, out count))
			{
				if (shop != null)
				{
					if (count <= (shop.count - (shop.reserveCount ?? 0)))
					{
						shop.BuyCount = count;
						LoadingShopingCart();
					}
					else
					{
						((TextBox)sender).Text = shop.BuyCount.ToString();
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
			item.Order(Convert.ToInt32(hdnOrder.Value));
			modalOrderConfirm.Visible = false;
			ProductsSession.First(b => b.id == Convert.ToInt32(hdnOrder.Value)).order = true;
			gvwProducts.DataSource = ProductsSession;
			gvwProducts.DataBind();
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
			CalcShopingCartSum(lblShopConfirmSum, spanShopConfirm);
			gvwShpingCartConfirm.DataSource = ShopingCartSession;
			gvwShpingCartConfirm.DataBind();
			modalBuyConfirm.Visible = true;
		}

		#endregion

		#region Hendlers

		protected void btnYes_Click(object sender, EventArgs e)
		{
			item.BuyShopingCart(ShopingCartSession, ProductsSession, AppHelper.CurrentUser.id, Convert.ToInt32(drpBuyer.SelectedValue));

			ShopingCartSession = new List<ShopingCart>();
			LoadingShopingCart();
			gvwProducts.DataSource = ProductsSession;
			gvwProducts.DataBind();

			modalBuyConfirm.Visible = false;
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
			modalBuyConfirm.Visible = false;
		}

		#endregion

		#endregion

		#region Modal window single buy confirm

		#region Methods

		private void ShowModalSingleBuyConfirm()
		{
#warning При изменении покупателя в дропе пересчитывать итого
			CalcShopingCartSum(lblSingleBuySum, spanSingleBuySum);
			gvwSingleBuy.DataSource = ShopingCartSession;
			gvwSingleBuy.DataBind();
			modalSingleBuy.Visible = true;
		}

		#endregion

		#region Hendlers

		protected void btnSingleBuyYes_Click(object sender, EventArgs e)
		{
			item.BuyShopingCart(ShopingCartSession, ProductsSession, AppHelper.CurrentUser.id, Convert.ToInt32(drpBuyer.SelectedValue));

			ShopingCartSession = new List<ShopingCart>();
			LoadingShopingCart();
			gvwProducts.DataSource = ProductsSession;
			gvwProducts.DataBind();

			modalSingleBuy.Visible = false;
		}

		protected void btnSingleBuyNo_Click(object sender, EventArgs e)
		{
			modalSingleBuy.Visible = false;
		}

		protected void txtSingleBuyCount_TextChanged(object sender, EventArgs e)
		{
			ShopingCart shop =
					ShopingCartSession.FirstOrDefault(
						b => b.id == Convert.ToInt32(((Label)((Control)(sender)).FindControl("lblID")).Text));
			int count;
			if (int.TryParse(((TextBox)sender).Text, out count))
			{
				if (shop != null)
				{
					if (count <= (shop.count - (shop.reserveCount ?? 0)))
					{
						shop.BuyCount = count;
						CalcShopingCartSum(lblSingleBuySum, spanSingleBuySum);
					}
					else
					{
						((TextBox)sender).Text = shop.BuyCount.ToString();
					}
				}
			}
			else
			{
				((TextBox)sender).Text = shop.BuyCount.ToString();
			}
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

		#region Modal window Demand

		#region Methods

		private void ShowDemand()
		{
			modalDemand.Visible = true;
		}

		#endregion

		#region Hendlers

		protected void btnDemandOk_Click(object sender, EventArgs e)
		{
			logActivity.Warning(string.Format("Спрос на товар: {0}", txtDemandProduct.Text), AppHelper.CurrentUser.id);
			modalDemand.Visible = false;
		}

		protected void btnDemandNo_Click(object sender, EventArgs e)
		{
			modalDemand.Visible = false;
		}

		#endregion

		#endregion

		#region Modal window Return

		#region Methods

		private void ShowReturn()
		{
			modalReturn.Visible = true;
		}

		#endregion

		#region Hendlers

		protected void btnReturnOk_Click(object sender, EventArgs e)
		{
			modalReturn.Visible = false;
		}

		protected void btnReturnShowProducts_Click(object sender, EventArgs e)
		{
			gvwReturn.DataSource = logSale.GetGiveBackList(txtReturnProductCode.Text, txtReturnProductSoldDate.Text);
			gvwReturn.DataBind();
		}

		protected void gvwReturn_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int id;
			if (int.TryParse(e.CommandArgument.ToString(), out id))
			{
				if (e.CommandName.ToLower() == "return")
				{
					ShowReturnConfirm();

				}
			}
		}

		protected void gvwReturn_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			// Необходимо присутствие етого метода для правильного удаление строк из GridView gvwShoppingCart
		}

		#endregion

		#endregion

		#region Modal window Return Confirm

		#region Methods

		private void ShowReturnConfirm()
		{
			modalWarningConfirm.Visible = true;
		}

		#endregion

		#region Hendlers

		protected void btnReturnConfirmOk_Click(object sender, EventArgs e)
		{
#warning Функционал возврата
			//			logSale.GiveBack();
			modalWarningConfirm.Visible = false;
		}

		protected void ReturnConfirmNo_Click(object sender, EventArgs e)
		{
			modalWarningConfirm.Visible = false;
		}

		#endregion

		#endregion

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
							ProductsSession = ProductsSession.OrderBy(b => b.name).ToList();
							break;
						}
					case "count":
						{
							ProductsSession = ProductsSession.OrderBy(b => b.count).ToList();
							break;
						}
					case "measure":
						{
							ProductsSession = ProductsSession.OrderBy(b => b.measure).ToList();
							break;
						}
					case "bprice":
						{
							ProductsSession = ProductsSession.OrderBy(b => b.bprice).ToList();
							break;
						}
					case "reserveCount":
						{
							ProductsSession = ProductsSession.OrderBy(b => b.reserveCount).ToList();
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
							ProductsSession = ProductsSession.OrderByDescending(b => b.name).ToList();
							break;
						}
					case "count":
						{
							ProductsSession = ProductsSession.OrderByDescending(b => b.count).ToList();
							break;
						}
					case "measure":
						{
							ProductsSession = ProductsSession.OrderByDescending(b => b.measure).ToList();
							break;
						}
					case "bprice":
						{
							ProductsSession = ProductsSession.OrderByDescending(b => b.bprice).ToList();
							break;
						}
					case "reserveCount":
						{
							ProductsSession = ProductsSession.OrderByDescending(b => b.reserveCount).ToList();
							break;
						}
				}
			}

			gvwProducts.DataSource = ProductsSession;
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
