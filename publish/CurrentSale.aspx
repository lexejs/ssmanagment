﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrentSale.aspx.cs" Inherits="SSManagment.CurrentSale" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <span>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Seller.aspx">К продажам</asp:HyperLink></span>

    <div>
    <rsweb:ReportViewer ID="reportViewer1" runat="server"
		Width="100%" ShowBackButton="True" AsyncRendering="false">
	</rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
