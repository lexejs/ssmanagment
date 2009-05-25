<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="SSManagment.ViewReport" %>
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
		<span>Продавец 
			<asp:DropDownList ID="lstSellers" runat="server">
			</asp:DropDownList>
		</span>
		<span>Покупатель 
			<asp:DropDownList ID="drpBuyers" runat="server">
			</asp:DropDownList>
		</span>
		<span>
            <asp:CheckBox ID="chkIsGiveBack" runat="server" Text="возвраты?" />
		</span>
		<span>
            за даты с 
            <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
            по <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
            </span>
		<span><asp:Button ID="btnShow" runat="server" Text="Button" OnClick="ShowReport"/></span>
		
	</div>
    <div>
    <rsweb:ReportViewer ID="reportViewer1" runat="server" OnDrillthrough="drillThrough"
		Width="100%" ShowBackButton="True" AsyncRendering="false">
	</rsweb:ReportViewer>
    </div>
    </form>
</body>
</html>
