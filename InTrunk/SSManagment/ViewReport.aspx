<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/AdminMaster.master" AutoEventWireup="true" CodeBehind="ViewReport.aspx.cs" Inherits="SSManagment.ViewReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdminMenu" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphAdminObject" runat="server">
  
  <div>
    <rsweb:ReportViewer ID="reportViewer1" runat="server" OnDrillthrough="drillThrough">
    </rsweb:ReportViewer>
  </div>

</asp:Content>
