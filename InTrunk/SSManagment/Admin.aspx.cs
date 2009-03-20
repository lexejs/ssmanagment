using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SSManagment.Models;

namespace SSManagment
{
	public partial class Admin : Page
	{
		private IList<group> groups;

		#region Constructor

		protected void Page_Load(object sender, EventArgs e)
		{
			var db = new ssmDataContext();

			// TODO move groups to Session
			groups = db.groups.ToList();

			if (!Page.IsPostBack)
			{
				lstGroupFill();
			}
		}
		#endregion

		#region Methods

		private void lstGroupFill()
		{
			var db = new ssmDataContext();
			int groupId = 0;
			if (lstGroup.SelectedIndex > -1)
				groupId = int.Parse(lstGroup.SelectedItem.Value);

			groups = db.groups.ToList();
			lstGroup.DataSource = groups.OrderBy(g => g.name).Where(g => g.parent == null).ToList();
			lstGroup.DataTextField = "name";
			lstGroup.DataValueField = "id";
			lstGroup.DataBind();
			txtGroupName.Text = "";

			ddlAttachTo.DataSource = groups.OrderBy(g => g.name).Where(g => g.parent == null).ToList();
			ddlAttachTo.DataTextField = "name";
			ddlAttachTo.DataValueField = "id";
			ddlAttachTo.DataBind();

			ddlItemToGroup.DataSource = groups.OrderBy(g => g.name).ToList();
			ddlItemToGroup.DataTextField = "name";
			ddlItemToGroup.DataValueField = "id";
			ddlItemToGroup.DataBind();

			ListItem listItem = lstGroup.Items.FindByValue(groupId.ToString());
			if (listItem != null)
			{
				listItem.Selected = true;
				//lstGroup_SelectedIndexChanged(new object(), new EventArgs());
			}
		}

		private void lstBuyersFill()
		{
			var db = new ssmDataContext();
			lstBuyers.DataSource = db.buyers.OrderBy(g => g.name).ToList();
			lstBuyers.DataTextField = "name";
			lstBuyers.DataValueField = "id";
			lstBuyers.DataBind();
		}

		private void lstsellersFill()
		{
			var db = new ssmDataContext();
			lstSellers.DataSource = db.sellers.OrderBy(g => g.fullName).ToList();
			lstSellers.DataTextField = "fullName";
			lstSellers.DataValueField = "id";
			lstSellers.DataBind();
		}

		private void LoadingTree()
		{
			treeCategories.Nodes.Clear();
			IList<group> rootCategories = groups.Where(b => b.parent == null).OrderBy(b => b.name).ToList();
			IList<group> rootchild = groups.Where(b => b.parent != null).OrderBy(b => b.name).ToList();

			foreach (group categ in rootCategories)
			{
				if (categ.parent != null) continue;
				var tree = new TreeNode(categ.name, categ.id.ToString());
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

		private void SaleFill()
		{
			gvwSales.DataSource = logSale.GetSalesList();
			gvwSales.DataBind();
		}

		private void BackListFill()
		{
			gvwBackList.DataSource = logSale.GetGiveBackListForApprove();
			gvwBackList.DataBind();
		}

		private void MessagesFill()
		{
			gvwMessagesList.DataSource = logActivity.GetMsgLogActivityList();
			gvwMessagesList.DataBind();

			gvwHotMessagesList.DataSource = logActivity.GetHotLogActivityList();
			gvwHotMessagesList.DataBind();
		}

		private void ForOrderFill()
		{
			gvwForOrder.DataSource = item.GetOrderList();
			gvwForOrder.DataBind();
		}

		private void EndItemsFill()
		{
			gvwEndItems.DataSource = item.GetEndItemList();
			gvwEndItems.DataBind();
		}

		private void LoadLstSubGroup()
		{
			int parentId = int.Parse(lstGroup.SelectedItem.Value);
			lstSubGroup.DataSource = groups.Where(g => g.parent != null && g.parent.Value == parentId).ToList();
			lstSubGroup.DataTextField = "name";
			lstSubGroup.DataValueField = "id";
			lstSubGroup.DataBind();
		}
		#endregion

		private void HideAll()
		{
			tblGroup.Visible = false;
			tblItems.Visible = false;
			tblBuyers.Visible = false;
			tblSellers.Visible = false;
			tblGiveBacks.Visible = false;
			tblSales.Visible = false;
			tblMessages.Visible = false;
			tblForOrder.Visible = false;
			tblEndItems.Visible = false;
		}

		#region Handlers

		protected void btnShowItems_Click(object sender, EventArgs e)
		{
			HideAll();
			var button = ((HtmlButton)sender);
			if (button != null)
			{
				if (button.ID.ToLower().Contains("items"))
				{
					tblItems.Visible = !tblGroup.Visible;
					LoadingTree();
					lstGroupFill();
				}
				else if (button.ID.ToLower().Contains("groups"))
				{
					tblItems.Visible = !tblGroup.Visible;
					lstGroupFill();
				}
				else if (button.ID.ToLower().Contains("buyer"))
				{
					tblBuyers.Visible = true;

					lstBuyersFill();
					lstGroupFill();
				}
				else if (button.ID.ToLower().Contains("seller"))
				{
					tblSellers.Visible = true;
					lstsellersFill();

				}
			}
			else
			{
				tblItems.Visible = !tblGroup.Visible;
			}
		}

		protected void btnShowSales_Click(object sender, EventArgs e)
		{
			tblGroup.Visible = false;
			tblItems.Visible = false;
			tblBuyers.Visible = false;
			tblSellers.Visible = false;
			tblGiveBacks.Visible = false;
			tblMessages.Visible = false;
			tblForOrder.Visible = false;
			tblSales.Visible = true;

			SaleFill();
		}

		protected void btnShowBackList_Click(object sender, EventArgs e)
		{
			tblGroup.Visible = false;
			tblItems.Visible = false;
			tblBuyers.Visible = false;
			tblSellers.Visible = false;
			tblSales.Visible = false;
			tblMessages.Visible = false;
			tblForOrder.Visible = false;
			tblGiveBacks.Visible = true;

			BackListFill();
		}

		protected void btnForOrder_Click(object sender, EventArgs e)
		{
			HideAll();
			tblForOrder.Visible = true;

			MessagesFill();
		}

		protected void btnShowMessage_Click(object sender, EventArgs e)
		{
			HideAll();
			tblMessages.Visible = true;

			ForOrderFill();
		}

		protected void btnShowEndItems_Click(object sender, EventArgs e)
		{
			HideAll();
			tblEndItems.Visible = true;

			EndItemsFill();
		}

		protected void btnGoBack_Click(object sender, EventArgs e)
		{
			Response.Redirect("Seller.aspx");
		}

		#region Groups

		protected void lstGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			int parentId = int.Parse(lstGroup.SelectedItem.Value);
			lstSubGroup.DataSource = groups.Where(g => g.parent != null && g.parent.Value == parentId).ToList();
			lstSubGroup.DataTextField = "name";
			lstSubGroup.DataValueField = "id";
			lstSubGroup.DataBind();
			txtGroupName.Text = lstGroup.SelectedItem.Text;
			txtSubGroupName.Text = "";
			var group = new ssmDataContext().groups.First(g => g.id == parentId);
			lblPGroup.Text = "Наименований товаров: " + group.GetItemsCountByGroupId(group.id);
		}

		protected void lstSubGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			int groupId = int.Parse(lstSubGroup.SelectedItem.Value);
			var db = new ssmDataContext();
			var group = db.groups.First(g => g.id == groupId);
			txtSubGroupName.Text = group.name;
			lblSubGroup.Text = "Наименований товаров: " + group.GetItemsCountByGroupId(group.id);
		}

		protected void btnAddGroup_Click(object sender, EventArgs e)
		{
			string groupName = "НОВАЯ ГРУППА";
			var db = new ssmDataContext();
			var nGroup = new group { name = groupName };
			db.groups.InsertOnSubmit(nGroup);
			db.SubmitChanges();
			lstGroupFill();
			if (lstGroup.Items.FindByText(groupName) != null)
			{
				lstGroup.SelectedIndex = lstGroup.Items.IndexOf(lstGroup.Items.FindByText(groupName));
				lstGroup_SelectedIndexChanged(new object(), new EventArgs());
			}
			else
			{
				txtGroupName.Text = lstGroup.SelectedItem.Text;
			}
		}

		protected void btnDelGroup_Click(object sender, EventArgs e)
		{
			if (lstGroup.SelectedItem != null)
			{
				var db = new ssmDataContext();
				int groupId = int.Parse(lstGroup.SelectedItem.Value);
				List<group> groupToDelete =
					db.groups.Where(g => g.id == groupId).ToList();
				if (groupToDelete.Count == 1 && group.GetItemsCountByGroupId(groupToDelete[0].id) == 0)
				{
					db.groups.DeleteOnSubmit(groupToDelete.First());
					db.SubmitChanges();
					lstGroupFill();
				}
			}
		}

		protected void btnUpdateGroup_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtGroupName.Text))
			{
				if (lstGroup.SelectedItem != null)
				{
					var db = new ssmDataContext();
					int groupId = int.Parse(lstGroup.SelectedItem.Value);
					List<group> groupToUpdate = db.groups.Where(g => g.id == groupId).ToList();
					if (groupToUpdate.Count == 1)
					{
						groupToUpdate.First().name = txtGroupName.Text;
						db.SubmitChanges();
						lstGroupFill();
						txtGroupName.Text = lstGroup.SelectedItem.Text;
					}
				}
			}
		}

		protected void btnAddSubGroup_Click(object sender, EventArgs e)
		{
			if (lstGroup.SelectedIndex > -1)
			{
				string subGroupName = "Новая Подгруппа";
				var db = new ssmDataContext();
				int parentId = int.Parse(lstGroup.SelectedItem.Value);
				var nGroup = new group { name = subGroupName, parent = parentId };
				db.groups.InsertOnSubmit(nGroup);
				db.SubmitChanges();
				lstGroupFill();

				txtGroupName.Text = lstGroup.SelectedItem.Text;
				LoadLstSubGroup();
				if (lstSubGroup.Items.FindByText(subGroupName) != null)
				{
					lstSubGroup.SelectedIndex = lstSubGroup.Items.IndexOf(lstSubGroup.Items.FindByText(subGroupName));
					lstSubGroup_SelectedIndexChanged(new object(), new EventArgs());
				}
			}
		}

		protected void btnDellSubGroup_Click(object sender, EventArgs e)
		{
			if (lstSubGroup.SelectedItem != null)
			{
				var db = new ssmDataContext();
				int groupId = int.Parse(lstSubGroup.SelectedItem.Value);
				List<group> groupToDelete = db.groups.Where(g => g.id == groupId).ToList();
				if (groupToDelete.Count == 1 && group.GetItemsCountByGroupId(groupToDelete[0].id) == 0)
				{
					db.groups.DeleteOnSubmit(groupToDelete.First());
					db.SubmitChanges();
					lstGroupFill();
				}
				txtGroupName.Text = lstGroup.SelectedItem.Text;
				txtSubGroupName.Text = "";
				lstSubGroup.SelectedIndex = -1;

				LoadLstSubGroup();
			}
		}

		protected void btnUpdateSubGroup_Click(object sender, EventArgs e)
		{
			if (lstSubGroup.SelectedItem != null && !string.IsNullOrEmpty(lstSubGroup.SelectedItem.Value))
			{
				var db = new ssmDataContext();
				int groupId = int.Parse(lstSubGroup.SelectedItem.Value);
				List<group> groupToUpdate = db.groups.Where(g => g.id == groupId).ToList();
				if (groupToUpdate.Count == 1)
				{
					groupToUpdate.First().name = txtSubGroupName.Text;
					db.SubmitChanges();
					lstGroupFill();
				}

				txtGroupName.Text = lstGroup.SelectedItem.Text;
				LoadLstSubGroup();
				lstSubGroup.Items.FindByText(txtSubGroupName.Text).Selected = true;
			}
		}

		protected void btnMoveToGroup_Click(object sender, EventArgs e)
		{
			if (lstSubGroup.SelectedItem != null)
			{
				var db = new ssmDataContext();
				int groupId = int.Parse(lstSubGroup.SelectedItem.Value);
				List<group> groupToMove = db.groups.Where(g => g.id == groupId).ToList();
				if (groupToMove.Count == 1)
				{
					group group = groupToMove.First();
					item item = new item() { name = group.name, groupId = group.parent };
					db.items.InsertOnSubmit(item);
					db.groups.DeleteOnSubmit(group);
					db.SubmitChanges();
					lstGroupFill();
					LoadLstSubGroup();
				}
				txtGroupName.Text = lstGroup.SelectedItem.Text;
				txtSubGroupName.Text = "";
			}
		}

		protected void btnAttachTo_Click(object sender, EventArgs e)
		{
			if (lstGroup.SelectedItem != null && ddlAttachTo.SelectedItem != null)
			{
				var db = new ssmDataContext();
				int groupToAttachId = int.Parse(ddlAttachTo.SelectedItem.Value);
				List<group> groupsToAttach = db.groups.Where(g => g.id == groupToAttachId).ToList();
				int groupForAttachId = int.Parse(lstGroup.SelectedItem.Value);
				List<group> groupsForAttach = db.groups.Where(g => g.id == groupForAttachId).ToList();
				if (groupsToAttach.Count == 1 && groupsForAttach.Count == 1 && groupToAttachId != groupForAttachId)
				{
					groupsForAttach.First().parent = groupsToAttach.First().id;
					db.SubmitChanges();
					lstGroupFill();
				}
			}
		}

		#endregion

		#region Items

		protected void treeCategories_SelectedNodeChanged(object sender, EventArgs e)
		{
			int id;
			if (int.TryParse(treeCategories.SelectedNode.Value, out id))
			{
				lstItems.DataSource = item.GetAllByGroupId(id);
				lstItems.DataTextField = "name";
				lstItems.DataValueField = "id";
				lstItems.DataBind();
				lblGroupName.Text = new ssmDataContext().groups.First(g => g.id == id).name;
			}

		}

		protected void lstItems_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstItems.SelectedItem != null)
			{
				int itemId = int.Parse(lstItems.SelectedValue);
				var db = new ssmDataContext();
				item item = db.items.Where(i => i.id == itemId).FirstOrDefault();

				if (item != null)
				{
					txtItemName.Text = item.name;
					txtItemMeasure.Text = item.measure;
					txtItemCount.Text = item.count != null ? item.count.Value.ToString() : "0";
					txtItemAdminPrice.Text = item.adminPrice != null ? item.adminPrice.Value.ToString() : "1";
					txtItemPct.Text = item.pct != null ? item.pct.Value.ToString() : "1";
					txtItemOrderCount.Text = item.countToOrder != null ? item.countToOrder.Value.ToString() : "0";
					chbItemCanGiveBack.Checked = item.canGiveBack != null ? item.canGiveBack.Value : false;
					chbItemIsActive.Checked = item.isActive != null ? item.isActive.Value : false;
				}
			}
		}


		protected void btnItemUpdate_Click(object sender, EventArgs e)
		{
			if (lstItems.SelectedItem != null)
			{
				int itemId = int.Parse(lstItems.SelectedValue);
				var db = new ssmDataContext();
				item item = db.items.Where(i => i.id == itemId).FirstOrDefault();

				item.name = txtItemName.Text;
				item.measure = txtItemMeasure.Text;
				item.count = int.Parse(txtItemCount.Text);
				item.adminPrice = float.Parse(txtItemAdminPrice.Text);
				item.pct = float.Parse(txtItemPct.Text);
				item.countToOrder = int.Parse(txtItemOrderCount.Text);
				item.canGiveBack = chbItemCanGiveBack.Checked;
				item.isActive = chbItemIsActive.Checked;

				db.SubmitChanges();

				int id;
				if (int.TryParse(treeCategories.SelectedNode.Value, out id))
				{
					lstItems.DataSource = item.GetAllByGroupId(id);
					lstItems.DataTextField = "name";
					lstItems.DataValueField = "id";
					lstItems.DataBind();
					lblGroupName.Text = new ssmDataContext().groups.First(g => g.id == id).name;
				}
			}
		}

		protected void btnItemMove_Click(object sender, EventArgs e)
		{
			if (lstItems.SelectedItem != null && ddlItemToGroup.SelectedItem != null)
			{
				int itemId = int.Parse(lstItems.SelectedValue);
				var db = new ssmDataContext();
				item item = db.items.Where(i => i.id == itemId).FirstOrDefault();

				int groupToAttachId = int.Parse(ddlItemToGroup.SelectedItem.Value);
				List<group> groupsToAttach = db.groups.Where(g => g.id == groupToAttachId).ToList();
				if (groupsToAttach.Count == 1)
				{
					item.groupId = groupsToAttach.First().id;
					db.SubmitChanges();
					treeCategories_SelectedNodeChanged(new object(), new EventArgs());
				}
			}
		}

		protected void btnItemAdd_Click(object sender, EventArgs e)
		{
			if (treeCategories.SelectedNode != null)
			{
				int groupId = int.Parse(treeCategories.SelectedNode.Value);
				var db = new ssmDataContext();
				string newItem = "новый товар";
				var item = new item { name = newItem, groupId = groupId };
				db.items.InsertOnSubmit(item);
				db.SubmitChanges();
				treeCategories_SelectedNodeChanged(new object(), new EventArgs());
				if (lstItems.Items.FindByText(newItem) != null)
				{
					lstItems.SelectedIndex = lstItems.Items.IndexOf(lstItems.Items.FindByText(newItem));
				}
				lstItems_SelectedIndexChanged(new object(), new EventArgs());
			}
		}


		#endregion

		#region Buyers

		protected void lstBuyers_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(lstBuyers.SelectedValue))
			{
				int buyerId = int.Parse(lstBuyers.SelectedValue);
				var db = new ssmDataContext();
				buyer buyer = db.buyers.First(b => b.id == buyerId);
				txtBuyerName.Text = buyer.name;
				chkBuyerCanBuyOnTick.Checked = buyer.canBuyOnTick.HasValue && buyer.canBuyOnTick.Value;
				chkBuyerIsActive.Checked = buyer.isActive.HasValue && buyer.isActive.Value;
				txtBuyerPct.Text = buyer.pct.HasValue ? buyer.pct.Value.ToString() : "0";
			}
		}

		protected void btnBuyerAdd_Click(object sender, EventArgs e)
		{
			string strName = "новый покупатель";
			var db = new ssmDataContext();
			buyer buyer = new buyer { isActive = false, name = strName, canBuyOnTick = false, pct = 0 };
			db.buyers.InsertOnSubmit(buyer);
			db.SubmitChanges();
			lstBuyersFill();
			if (lstBuyers.Items.FindByText(strName) != null)
			{
				lstBuyers.SelectedIndex = lstBuyers.Items.IndexOf(lstBuyers.Items.FindByText(strName));
				lstBuyers_SelectedIndexChanged(new object(), new EventArgs());
			}
		}

		protected void btnBuyerUpdate_Click(object sender, EventArgs e)
		{

			if (!string.IsNullOrEmpty(lstBuyers.SelectedValue))
			{
				string strName = txtBuyerName.Text;
				int buyerId = int.Parse(lstBuyers.SelectedValue);
				var db = new ssmDataContext();
				buyer buyer = db.buyers.First(b => b.id == buyerId);
				buyer.name = txtBuyerName.Text;
				buyer.canBuyOnTick = chkBuyerCanBuyOnTick.Checked;
				float pct = 0;
				buyer.pct = float.TryParse(txtBuyerPct.Text, out pct) ? pct : 0;
				buyer.isActive = chkBuyerIsActive.Checked;
				db.SubmitChanges();

				lstBuyersFill();

				if (lstBuyers.Items.FindByText(strName) != null)
				{
					lstBuyers.SelectedIndex = lstBuyers.Items.IndexOf(lstBuyers.Items.FindByText(strName));
					lstBuyers_SelectedIndexChanged(new object(), new EventArgs());
				}
			}
		}

		#endregion

		#region Sellers

		protected void lstSeller_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(lstSellers.SelectedValue))
			{
				int sellerID = int.Parse(lstSellers.SelectedValue);
				var db = new ssmDataContext();
				seller seller = db.sellers.First(b => b.id == sellerID);
				txtSellerName.Text = seller.fullName;
				txtSellerLogin.Text = seller.login;
				txtSellerPass.Text = seller.password;
				chkIsActive.Checked = seller.isActive.GetValueOrDefault();
				chkIsAdmin.Checked = seller.isAdmin.GetValueOrDefault();
			}
		}

		protected void btnSellerAdd_Click(object sender, EventArgs e)
		{
			string sellerName = "Продавец";
			var db = new ssmDataContext();
			seller seller = new seller { isActive = false, fullName = sellerName, isAdmin = false, password = "pass", login = "login" };
			db.sellers.InsertOnSubmit(seller);
			db.SubmitChanges();
			lstsellersFill();
			if (lstSellers.Items.FindByText(sellerName) != null)
			{
				lstSellers.SelectedIndex = lstSellers.Items.IndexOf(lstSellers.Items.FindByText(sellerName));
				lstSeller_SelectedIndexChanged(new object(), new EventArgs());
			}
		}

		protected void btnSellerUpdate_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(lstSellers.SelectedValue))
			{
				string sellerName = txtSellerName.Text;

				int sellerID = int.Parse(lstSellers.SelectedValue);
				var db = new ssmDataContext();
				seller seller = db.sellers.First(b => b.id == sellerID);
				seller.fullName = txtSellerName.Text;
				seller.login = txtSellerLogin.Text;
				seller.password = txtSellerPass.Text;
				seller.isActive = chkIsActive.Checked;
				seller.isAdmin = chkIsAdmin.Checked;
				db.SubmitChanges();
				lstsellersFill();
				if (lstSellers.Items.FindByText(sellerName) != null)
				{
					lstSellers.SelectedIndex = lstSellers.Items.IndexOf(lstSellers.Items.FindByText(sellerName));
					lstSeller_SelectedIndexChanged(new object(), new EventArgs());
				}
			}

		}

		#endregion

		#region Back

		protected void gvwBackList_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			int id;
			if (int.TryParse(e.CommandArgument.ToString(), out id))
			{
				if (e.CommandName.ToLower() == "approve")
				{
					logSale.ApproveGiveBack(id);
					BackListFill();
				}
			}
		}

		#endregion

		#endregion

	}
}