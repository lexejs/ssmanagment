﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/AdminMaster.master"
	AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="SSManagment.Reports1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminMenu" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAdminObject" runat="server">
	<div>
		<span>Продавец 
			<asp:DropDownList ID="lstSellers" runat="server">
			</asp:DropDownList>
		</span>
		<span><asp:Button ID="btnShow" runat="server" Text="Button" OnClick="ShowReport"/></span>
		
	</div>
	<rsweb:ReportViewer ID="reportViewer1" runat="server" OnDrillthrough="drillThrough"
		Width="100%" ShowBackButton="True" AsyncRendering="false">
	</rsweb:ReportViewer>
</asp:Content>
