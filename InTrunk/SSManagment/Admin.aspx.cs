using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSManagment.Models;

namespace SSManagment
{
	public partial class Admin : System.Web.UI.Page
	{
		IList<group> groups;
		protected void Page_Load(object sender, EventArgs e)
		{
			var db = new ssmDataContext();

			// TODO move groups to Session
			groups = db.groups.ToList();

			if (!Page.IsPostBack)
			{
				lstGroupFill();
				LoadingTree();
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

			ListItem listItem = lstGroup.Items.FindByValue(groupId.ToString());
			if (listItem != null)
			{
				listItem.Selected = true;
				lstGroup_SelectedIndexChanged(new object(), new EventArgs());
			}
		}

        private void LoadingTree()
        {
            IList<group> rootCategories = groups.Where(b => b.parent == null).OrderBy(b => b.name).ToList();
            IList<group> rootchild = groups.Where(b => b.parent != null).OrderBy(b => b.name).ToList();

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

		
		protected void lstGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			var parentId = int.Parse(lstGroup.SelectedItem.Value);
			lstSubGroup.DataSource = groups.Where(g => g.parent != null && g.parent.Value == parentId).ToList();
			lstSubGroup.DataTextField = "name";
			lstSubGroup.DataValueField = "id";
			lstSubGroup.DataBind();
			txtGroupName.Text = lstGroup.SelectedItem.Text;
		}


		protected void btnAddGroup_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtGroupName.Text))
			{
				var db = new ssmDataContext();
				var nGroup = new group() { name = txtGroupName.Text };
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
				var groupId = int.Parse(lstGroup.SelectedItem.Value);
				var groupToDelete = db.groups.Where(g => g.id == groupId && g.groups.Count == 0 && g.items.Count == 0).ToList();
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
				var parentId = int.Parse(lstGroup.SelectedItem.Value);
				var nGroup = new group() { name = txtSubGroupName.Text, parent = parentId };
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
				var groupId = int.Parse(lstSubGroup.SelectedItem.Value);
				var groupToDelete = db.groups.Where(g => g.id == groupId && g.items.Count == 0).ToList();
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
					var groupId = int.Parse(lstGroup.SelectedItem.Value);
					var groupToUpdate = db.groups.Where(g => g.id == groupId).ToList();
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
				var groupId = int.Parse(lstSubGroup.SelectedItem.Value);
				var groupToUpdate = db.groups.Where(g => g.id == groupId && g.groups.Count == 0).ToList();
				if (groupToUpdate.Count == 1)
				{
					groupToUpdate.First().name = lstSubGroup.SelectedItem.Value;
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
				var groupId = int.Parse(lstSubGroup.SelectedItem.Value);
				var groupToMove = db.groups.Where(g => g.id == groupId && g.groups.Count == 0).ToList();
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
				var groupToAttachId = int.Parse(ddlAttachTo.SelectedItem.Value);
				var groupsToAttach = db.groups.Where(g => g.id == groupToAttachId).ToList();
				var groupForAttachId = int.Parse(lstGroup.SelectedItem.Value);
				var groupsForAttach = db.groups.Where(g => g.id == groupForAttachId).ToList();
				if (groupsToAttach.Count == 1 && groupsForAttach.Count == 1 && groupToAttachId != groupForAttachId)
				{
					groupsForAttach.First().parent = groupsToAttach.First().id;
					db.SubmitChanges();
					lstGroupFill();
				}
			}
		}
	
		protected void btnShowGroups_Click(object sender, EventArgs e)
		{
			tblGroup.Visible = true;
			tblItems.Visible = !tblGroup.Visible;
		}

		protected void btnShowItems_Click(object sender, EventArgs e)
		{
			tblGroup.Visible = false;
			tblItems.Visible = !tblGroup.Visible;
		}

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Seller.aspx");
            ;
        }

		protected void treeCategories_SelectedNodeChanged(object sender, EventArgs e)
		{
			int id;
			if (int.TryParse(((System.Web.UI.WebControls.TreeView)(sender)).SelectedNode.Value, out id))
			{
				lstItems.DataSource = item.GetAllByGroupId(id);
				lstItems.DataTextField = "name";
				lstItems.DataValueField = "id";
				lstItems.DataBind();
			
			}
		}

		protected void lstItems_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		#endregion

	}
}
