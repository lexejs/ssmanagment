﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/SellerMaster.master" AutoEventWireup="true" CodeBehind="Seller.aspx.cs" Inherits="SSManagment.Seller" %>

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
				<asp:TreeView ID="treeCategories" runat="server" ExpandDepth="1" AutoGenerateDataBindings="true"
                    ForeColor="Gray" ImageSet="Arrows" ShowLines="True" Height="100%" Width="100%">
					<ParentNodeStyle ForeColor="Black" />
					<RootNodeStyle ForeColor="Black" />
				</asp:TreeView>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ID="cntProducts" ContentPlaceHolderID="cphStandartProducts" runat="server">
    <asp:GridView ID="gvwProducts" runat="server" Width="100%" AutoGenerateColumns="true" CellPadding="4" ForeColor="#333333" GridLines="None">
		<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
		<Columns>
			<asp:BoundField HeaderText="Товары" ReadOnly="True" />
			<asp:ButtonField ButtonType="Image" CommandName="add" Text="В корзину" 
                ImageUrl="~/App_Themes/Main/Icons/162px-Ambox_emblem_plus.svg.png">
				<ControlStyle Width="24px" Height="24px" />
			</asp:ButtonField>
			<asp:ButtonField ButtonType="Image" CommandName="sale" Text="Продать" 
                ImageUrl="~/App_Themes/Main/Icons/120px-Emblem-advertisement-dollar.svg.png">
				<ControlStyle Width="24px" Height="24px" />
			</asp:ButtonField>
		</Columns>
		<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
		<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
		<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
		<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
		<EditRowStyle BackColor="#999999" />
		<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
	</asp:GridView>
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
				<span>Список покупок ( корзина)</span>
			</td>
		</tr>
		<tr>
			<td align="center">
				<asp:GridView ID="gvwShoppingCart" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
					<RowStyle BackColor="#F7F7DE" />
					<Columns>
						<asp:TemplateField HeaderText="Товар">
                            <ItemTemplate>
                                <asp:Label ID="lblShoppingCartProduct" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ControlStyle Width="100%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Цена">
                            <ItemTemplate>
                                <asp:Label ID="lblShoppingCartCost" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
						<asp:ButtonField ButtonType="Image" CommandName="delete" 
                            ImageUrl="~/App_Themes/Main/Icons/120px-Dialog-error.svg.png" Text="Удалить">
                        <ControlStyle Height="24px" Width="24px" />
                        </asp:ButtonField>
					</Columns>
					<FooterStyle BackColor="#CCCC99" />
					<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
					<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
					<HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
					<AlternatingRowStyle BackColor="White" />
				</asp:GridView>
			</td>
		</tr>
		<tr>
			<td>
				&nbsp;
				Итого :</td>
		</tr>
		<tr>
			<td>
				<asp:Button ID="btnBuy" runat="server" Text="Оформить покупку" Width="100%" />
			</td>
		</tr>
	</table>
</asp:Content>
