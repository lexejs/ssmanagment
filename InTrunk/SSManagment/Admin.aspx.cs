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
			}
		}

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

			ListItem listItem = lstGroup.Items.FindByValue(groupId.ToString());
			if (listItem != null)
			{
				listItem.Selected = true;
				lstGroup_SelectedIndexChanged(new object(),new EventArgs());
			}
		}

		protected void lstGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			var parentId = int.Parse(lstGroup.SelectedItem.Value);
			lstSubGroup.DataSource = groups.Where(g => g.parent != null && g.parent.Value == parentId).ToList();
			lstSubGroup.DataTextField = "name";
			lstSubGroup.DataValueField = "id";
			lstSubGroup.DataBind();
			txtGroupName.Text = lstGroup.SelectedItem.Text;
		}

		//protected void lstSubGroup_SelectedIndexChanged(object sender, EventArgs e)
		//{

		//}

		protected void btnAddGroup_Click(object sender, EventArgs e)
		{
			var db = new ssmDataContext();
			var nGroup = new group() { name = txtGroupName.Text };
			db.groups.InsertOnSubmit(nGroup);
			db.SubmitChanges();
			lstGroupFill();
		}

		protected void btnDelGroup_Click(object sender, EventArgs e)
		{
			var db = new ssmDataContext();
			if (lstGroup.SelectedItem != null)
			{
				var groupId = int.Parse(lstGroup.SelectedItem.Value);
				var groupToDelete = db.groups.Where(g => g.id == groupId && g.groups.Count == 0).ToList();
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
			var db = new ssmDataContext();
			if (lstGroup.SelectedIndex > -1)
			{
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
			var db = new ssmDataContext();

			if (lstSubGroup.SelectedItem != null)
			{
				var groupId = int.Parse(lstSubGroup.SelectedItem.Value);
				var groupToDelete = db.groups.Where(g => g.id == groupId && g.groups.Count == 0).ToList();
				if (groupToDelete.Count == 1)
				{
					db.groups.DeleteOnSubmit(groupToDelete.First());
					db.SubmitChanges();
					lstGroupFill();
				}
			}
		}

	}
}
