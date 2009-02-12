<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/AdminMaster.master"
	AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="SSManagment.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminMenu" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphAdminObject" runat="server">
	<div>
		<table>
			<tr>
				<td>
					<asp:ListBox ID="lstGroup" runat="server" Height="300px" Width="300px" OnSelectedIndexChanged="lstGroup_SelectedIndexChanged"
						AutoPostBack="True"></asp:ListBox>
				</td>
			</tr>
			<tr>
				<td>
					<asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox>
					<asp:Button ID="btnAddGroup" runat="server" Text="Добавить" 
						onclick="btnAddGroup_Click" />
					<asp:Button ID="btnDelGroup" runat="server" Text="Удалить" 
						onclick="btnDelGroup_Click" />
				</td>
			</tr>
		</table>
		<table>
			<tr>
				<td>
					<asp:ListBox ID="lstSubGroup" runat="server" Height="300px" Width="300px" AutoPostBack="true">
					</asp:ListBox>
				</td>
			</tr>
			<tr>
				<td>
					<asp:TextBox ID="txtSubGroupName" runat="server"></asp:TextBox>
					<asp:Button ID="btnAddSubGroup" runat="server" Text="Добавить" 
						onclick="btnAddSubGroup_Click" />
					<asp:Button ID="btnDellSubGroup" runat="server" Text="Удалить" />
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
