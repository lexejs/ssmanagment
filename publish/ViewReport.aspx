<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="SSManagment.ViewReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Отчёты</title>
	
	<script src="Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
	<link href="Content/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
	<script src="Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

	<script src="Scripts/jquery.ui.i18n.js" type="text/javascript"></script>
</head>
<body>
	<form id="form1" runat="server">
	<span>
		<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Seller.aspx">К продажам</asp:HyperLink></span>
	<div>
		<span>Продавец
			<asp:DropDownList ID="lstSellers" runat="server">
			</asp:DropDownList>
		</span><span>Покупатель
			<asp:DropDownList ID="drpBuyers" runat="server">
			</asp:DropDownList>
		</span><span>
			<asp:CheckBox ID="chkIsGiveBack" runat="server" Text="возвраты?" />
		</span><span>за даты с
			<asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
			по
			<asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>

			<script>
				$(document).ready(function() {

				$(function() {
					 $.datepicker.setDefaults($.extend($.datepicker.regional["ru"]));
						$("#txtDateFrom").datepicker({ dateFormat: 'yy-mm-dd' });
						$("#txtDateTo").datepicker({ dateFormat: 'yy-mm-dd' });
					});

				}); 									
			</script>

		</span><span>
			<asp:Button ID="btnShow" runat="server" Text="Button" OnClick="ShowReport" /></span>
	</div>
	<div><span>
		<b><asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label></b>
		</span></div>
	<div>
		<rsweb:ReportViewer ID="reportViewer1" runat="server" OnDrillthrough="drillThrough"
			Width="100%" ShowBackButton="True" AsyncRendering="false">
		</rsweb:ReportViewer>
	</div>
	</form>
</body>
</html>
