<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemReports.aspx.cs" Inherits="SSManagment.RemReports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Удаление отчётов</title>
	<script src="Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
	<link href="Content/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
	<script src="Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

	<script src="Scripts/jquery.ui.i18n.js" type="text/javascript"></script>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<span>
			<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="ViewReport.aspx">К отчётам</asp:HyperLink></span>
		<span>Удалить отчёты за даты с
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
			<asp:Button ID="btnGO" runat="server" Text="Удалить!" OnClick="RemoveReports" /></span>
	</div>
	<div>
		<span><b>
			<asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label></b> </span>
	</div>
	</form>
</body>
</html>
