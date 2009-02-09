<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/SellerMaster.master"
    AutoEventWireup="true" CodeBehind="Seller.aspx.cs" Inherits="SSManagment.Seller" %>

<asp:Content ID="cntCategories" ContentPlaceHolderID="cphStandartCategories" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center" width="80%">
                <asp:TextBox ID="txtFind" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td width="20%">
                <asp:Button ID="btnFind" runat="server" Text="Найти" Width="40px" />
            </td>
        </tr>
        <tr>
            <td colspan="2" height="1px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ForeColor="Gray" ImageSet="Arrows"
                    ShowLines="True" Height="100%" Width="100%">
                    <ParentNodeStyle ForeColor="Black" />
                    <RootNodeStyle ForeColor="Black" />
                </asp:TreeView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cntProducts" ContentPlaceHolderID="cphStandartProducts" runat="server">
</asp:Content>
<asp:Content ID="cntBuy" ContentPlaceHolderID="cphStandartBuy" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>
                <asp:Button ID="btnBackProduct" runat="server" Text="Возврат товара" Width="100%" />
            </td>
        </tr>
        <tr height="2px">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <span>Покупатель :</span>
                <asp:DropDownList ID="drpBuyer" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="center">
                <span>Список покупок</span>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="GridView1" runat="server" Width="100%">
                </asp:GridView>
            </td>
        </tr>
        <tr height="2px">
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnBuy" runat="server" Text="Оформить покупку" Width="100%" />
            </td>
        </tr>
    </table>
</asp:Content>
