<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/AdminMaster.master"
	AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="SSManagment.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminMenu" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAdminObject" runat="server">
	<div>
		<span>
			<asp:ListBox ID="lstGroup" runat="server"></asp:ListBox>
			<asp:TextBox ID="txtGroupName" runat="server"></asp:TextBox>
			<asp:Button ID="btnAddGroup" runat="server" Text="Добавить" />
			<asp:Button ID="btnDelGroup" runat="server" Text="Удалить" />
		</span><span>
			<asp:ListBox ID="lstSubGroup" runat="server"></asp:ListBox>
			<asp:TextBox ID="txtSubGroupName" runat="server"></asp:TextBox>
			<asp:Button ID="btnAddSubGroup" runat="server" Text="Добавить" />
			<asp:Button ID="btnDellSubGroup" runat="server" Text="Удалить" />
		</span>
	</div>
</asp:Content>
