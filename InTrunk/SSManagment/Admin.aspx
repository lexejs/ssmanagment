<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/AdminMaster.master"
	AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="SSManagment.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminMenu" runat="server">
	<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Seller.aspx">Товар</asp:HyperLink>
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
					<asp:TextBox ID="txtGroupName" runat="server" TabIndex="1"></asp:TextBox>
					<asp:Button ID="btnAddGroup" runat="server" Text="Добавить" 
						onclick="btnAddGroup_Click" TabIndex="2" UseSubmitBehavior="False" />
					<asp:Button ID="btnDelGroup" runat="server" Text="Удалить" 
						onclick="btnDelGroup_Click" TabIndex="3" UseSubmitBehavior="False" />
				</td>
			</tr>
		</table>
		<table>
			<tr>
				<td>
					<asp:ListBox ID="lstSubGroup" runat="server" Height="300px" Width="300px" 
						TabIndex="4">
					</asp:ListBox>
				</td>
			</tr>
			<tr>
				<td>
					<asp:TextBox ID="txtSubGroupName" runat="server" TabIndex="5"></asp:TextBox>
					<asp:Button ID="btnAddSubGroup" runat="server" Text="Добавить" 
						onclick="btnAddSubGroup_Click" TabIndex="6" UseSubmitBehavior="False" />
					<asp:Button ID="btnDellSubGroup" runat="server" Text="Удалить" 
						onclick="btnDellSubGroup_Click" TabIndex="7" UseSubmitBehavior="False" />
				</td>
			</tr>
		</table>
	</div>
</asp:Content>
