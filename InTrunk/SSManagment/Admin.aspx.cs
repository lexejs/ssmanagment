﻿using System;
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
                lstGroup_SelectedIndexChanged(new object(), new EventArgs());
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

        private void LoadingTree()
        {
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

        #endregion

        #region Handlers

        protected void lstGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int parentId = int.Parse(lstGroup.SelectedItem.Value);
            lstSubGroup.DataSource = groups.Where(g => g.parent != null && g.parent.Value == parentId).ToList();
            lstSubGroup.DataTextField = "name";
            lstSubGroup.DataValueField = "id";
            lstSubGroup.DataBind();
            txtGroupName.Text = lstGroup.SelectedItem.Text;
            var group = new ssmDataContext().groups.First(g => g.id == parentId);
            lblPGroup.Text = "Наименований товаров: " + group.items.Count.ToString();
        }

        protected void lstSubGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int groupId = int.Parse(lstSubGroup.SelectedItem.Value);
            var db = new ssmDataContext();
            var group = db.groups.First(g => g.id == groupId);
            txtSubGroupName.Text = group.name;
            lblSubGroup.Text = "Наименований товаров: " + group.items.Count.ToString();
        }

        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtGroupName.Text))
            {
                var db = new ssmDataContext();
                var nGroup = new group { name = string.IsNullOrEmpty(txtGroupName.Text) ? "НОВАЯ ГРУППА" : txtGroupName.Text };
                db.groups.InsertOnSubmit(nGroup);
                db.SubmitChanges();
                lstGroupFill();
            }
        }

        protected void btnDelGroup_Click(object sender, EventArgs e)
        {
            if (lstGroup.SelectedItem != null)
            {
                var db = new ssmDataContext();
                int groupId = int.Parse(lstGroup.SelectedItem.Value);
                List<group> groupToDelete =
                    db.groups.Where(g => g.id == groupId && g.groups.Count == 0 && g.items.Count == 0).ToList();
                if (groupToDelete.Count == 1)
                {
                    db.groups.DeleteOnSubmit(groupToDelete.First());
                    db.SubmitChanges();
                    lstGroupFill();
                }
            }
        }

        protected void btnAddSubGroup_Click(object sender, EventArgs e)
        {
            if (lstGroup.SelectedIndex > -1 && !string.IsNullOrEmpty(txtSubGroupName.Text))
            {
                var db = new ssmDataContext();
                int parentId = int.Parse(lstGroup.SelectedItem.Value);
                var nGroup = new group { name = string.IsNullOrEmpty(txtSubGroupName.Text) ? "Новая Группа" : txtSubGroupName.Text, parent = parentId };
                db.groups.InsertOnSubmit(nGroup);
                db.SubmitChanges();
                lstGroupFill();
                txtSubGroupName.Text = "";
            }
        }

        protected void btnDellSubGroup_Click(object sender, EventArgs e)
        {
            if (lstSubGroup.SelectedItem != null)
            {
                var db = new ssmDataContext();
                int groupId = int.Parse(lstSubGroup.SelectedItem.Value);
                List<group> groupToDelete = db.groups.Where(g => g.id == groupId && g.items.Count == 0).ToList();
                if (groupToDelete.Count == 1)
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
                    }
                }
            }
        }

        protected void btnUpdateSubGroup_Click(object sender, EventArgs e)
        {
            if (lstSubGroup.SelectedItem != null && !string.IsNullOrEmpty(lstSubGroup.SelectedItem.Value))
            {
                var db = new ssmDataContext();
                int groupId = int.Parse(lstSubGroup.SelectedItem.Value);
                List<group> groupToUpdate = db.groups.Where(g => g.id == groupId && g.groups.Count == 0).ToList();
                if (groupToUpdate.Count == 1)
                {
                    groupToUpdate.First().name = txtSubGroupName.Text;
                    db.SubmitChanges();
                    lstGroupFill();
                }
            }
        }

        protected void btnMoveToGroup_Click(object sender, EventArgs e)
        {
            if (lstSubGroup.SelectedItem != null)
            {
                var db = new ssmDataContext();
                int groupId = int.Parse(lstSubGroup.SelectedItem.Value);
                List<group> groupToMove = db.groups.Where(g => g.id == groupId && g.groups.Count == 0).ToList();
                if (groupToMove.Count == 1)
                {
                    groupToMove.First().parent = null;
                    db.SubmitChanges();
                    lstGroupFill();
                }
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



        protected void btnShowItems_Click(object sender, EventArgs e)
        {
            var button = ((HtmlButton)sender);
            if (button != null)
            {
                if (button.ID.ToLower().Contains("items"))
                {
                    tblGroup.Visible = false;
                    tblItems.Visible = !tblGroup.Visible;
                    tblBuyers.Visible = false;
                    LoadingTree();
                    lstGroupFill();
                }
                else if (button.ID.ToLower().Contains("groups"))
                {
                    tblGroup.Visible = true;
                    tblItems.Visible = !tblGroup.Visible;
                    tblBuyers.Visible = false;
                    lstGroupFill();
                }
                else if (button.ID.ToLower().Contains("buyer"))
                {
                    tblGroup.Visible = false;
                    tblItems.Visible = false;
                    tblBuyers.Visible = true;
                    lstBuyersFill();
                    lstGroupFill();
                }
            }
            else
            {
                tblGroup.Visible = false;
                tblItems.Visible = !tblGroup.Visible;
                tblBuyers.Visible = false;
            }
        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Seller.aspx");
        }

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
                var item = new item { name = "новый товар", groupId = groupId };
                db.items.InsertOnSubmit(item);
                db.SubmitChanges();
                treeCategories_SelectedNodeChanged(new object(), new EventArgs());
            }
        }

        #endregion

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
            var db = new ssmDataContext();
            buyer buyer = new buyer { isActive = false, name = "новый покупатель", canBuyOnTick = false, pct = 0 };
            db.buyers.InsertOnSubmit(buyer);
            db.SubmitChanges();
            lstBuyersFill();
        }

        protected void btnBuyerUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lstBuyers.SelectedValue))
            {
                int buyerId = int.Parse(lstBuyers.SelectedValue);
                var db = new ssmDataContext();
                buyer buyer = db.buyers.First(b => b.id == buyerId);
                buyer.name = txtBuyerName.Text;
                buyer.canBuyOnTick = chkBuyerCanBuyOnTick.Checked;
                float pct = 0;
                buyer.pct = float.TryParse(txtBuyerPct.Text, out pct) ? pct : 0;
                buyer.isActive = chkBuyerIsActive.Checked;
                db.SubmitChanges();
            }
            lstBuyersFill();
        }


    }
}