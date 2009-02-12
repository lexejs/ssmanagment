﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/SellerMaster.master"
	AutoEventWireup="true" CodeBehind="Seller.aspx.cs" Inherits="SSManagment.Seller" %>

<asp:Content ID="cntCategories" ContentPlaceHolderID="cphStandartCategories" runat="server">
	<table cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td>
				<div class="Widget_heading_container">
					<span class="Widget_heading_container_Span"></span>
					<h2>
						Разделы</h2>
				</div>
				<div class="Widget_Body_container">
					<div class="Widget_Body_top">
						<span></span>
					</div>
					<div class="Widget_Body_content">
						<div class="clear">
						</div>
						<asp:TreeView ID="treeCategories" runat="server" ExpandDepth="1" AutoGenerateDataBindings="true"
							ForeColor="#333333" ShowLines="True" Height="100%" Width="100%" BackColor="#F7F7DE"
							BorderColor="#DEDFDE" BorderWidth="1px">
							<ParentNodeStyle ForeColor="White" BackColor="#6B696B" BorderColor="#FF6600" BorderStyle="Dashed"
								BorderWidth="1px" Font-Bold="True" NodeSpacing="1px" />
							<RootNodeStyle ForeColor="White" BackColor="#6B696B" BorderColor="#FF6600" BorderWidth="1px"
								Font-Bold="True" NodeSpacing="1px" />
						</asp:TreeView>
						<div class="clear">
						</div>
					</div>
				</div>
				<div class="Widget_Body_bottom">
					<span></span>
				</div>
			</td>
		</tr>
		<tr>
			<td>
				<div class="Widget_heading_container">
					<span class="Widget_heading_container_Span"></span>
					<h2>
						Дополнительно</h2>
				</div>
				<div class="Widget_Body_container">
					<div class="Widget_Body_top">
						<span></span>
					</div>
					<div class="Widget_Body_content">
						<div class="clear">
						</div>
						<button id="btnAdmin" runat="server" style="width: 120px" onserverclick="btnAdminClick">
							<span><em>Администрирование</em></span></button>
						<button id="btnReturn" runat="server" style="width: 120px">
							<span><em>Возврат товара</em></span></button>
						<div class="clear">
						</div>
					</div>
				</div>
				<div class="Widget_Body_bottom">
					<span></span>
				</div>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ID="cntProducts" ContentPlaceHolderID="cphStandartProducts" runat="server">
	<div class="Widget_heading_container">
		<span class="Widget_heading_container_Span"></span>
		<h2>
			Товары</h2>
	</div>
	<div class="Widget_Body_container">
		<div class="Widget_Body_top">
			<span></span>
		</div>
		<div class="Widget_Body_content">
			<div class="clear">
			</div>
			<table width="100%">
				<tr>
					<td align="center">
						<table>
							<td align="right">
								<asp:TextBox ID="txtFind" runat="server" Width="200px"></asp:TextBox>
							</td>
							<td>
								<button id="btnFind" runat="server">
									<span><em>Найти</em></span></button>
							</td>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<asp:GridView ID="gvwProducts" runat="server" Width="100%" AutoGenerateColumns="False"
							CellPadding="4" ForeColor="Black" GridLines="Vertical" CaptionAlign="Top" BackColor="White"
							BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
							<RowStyle BackColor="#F7F7DE" />
							<Columns>
								<asp:BoundField HeaderText="Наименование товара" ReadOnly="True" DataField="name" />
								<asp:BoundField HeaderText="На складе" DataField="count" NullDisplayText="отсутствует">
									<ControlStyle Width="50px" />
								</asp:BoundField>
								<asp:BoundField HeaderText="В упаковке" DataField="measure" NullDisplayText="неизветно">
									<ControlStyle Width="50px" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Цена" DataField="price" NullDisplayText="уточнить">
									<ControlStyle Width="50px" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Зарезервировано" DataField="countToOrder">
									<ControlStyle Width="70px" />
								</asp:BoundField>
								<asp:TemplateField HeaderText="Кол-во">
									<ItemTemplate>
										<asp:TextBox ID="txtBuyCount" runat="server" Text="1"></asp:TextBox>
									</ItemTemplate>
									<ControlStyle Width="50px" />
								</asp:TemplateField>
								<asp:ButtonField ButtonType="Image" CommandName="add" Text="В корзину" ImageUrl="~/App_Themes/Main/Icons/162px-Ambox_emblem_plus.svg.png">
									<ControlStyle Width="24px" Height="24px" />
								</asp:ButtonField>
								<asp:ButtonField ButtonType="Image" CommandName="sale" Text="Продать" ImageUrl="~/App_Themes/Main/Icons/120px-Emblem-advertisement-dollar.svg.png">
									<ControlStyle Width="24px" Height="24px" />
								</asp:ButtonField>
								<asp:ButtonField ButtonType="Image" CommandName="reserved" Text="Зарезервировать"
									ImageUrl="~/App_Themes/Main/Icons/48px-Emblem-symbolic-link.svg.png">
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
			</table>
			<div class="clear">
			</div>
		</div>
	</div>
	<div class="Widget_Body_bottom">
		<span></span>
	</div>
</asp:Content>
<asp:Content ID="cntBuy" ContentPlaceHolderID="cphStandartBuy" runat="server">
	<div class="Widget_heading_container">
		<span class="Widget_heading_container_Span"></span>
		<h2>
			Список покупок ( корзина)</h2>
	</div>
	<div class="Widget_Body_container">
		<div class="Widget_Body_top">
			<span></span>
		</div>
		<div class="Widget_Body_content">
			<div class="clear">
			</div>
			<table cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td align="center">
						<asp:GridView ID="gvwShoppingCart" runat="server" Width="100%" AutoGenerateColumns="False"
							BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
							ForeColor="Black" GridLines="Vertical">
							<RowStyle BackColor="#F7F7DE" />
							<Columns>
								<asp:BoundField HeaderText="Товар" DataField="name">
									<ControlStyle Width="150px" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Цена" DataField="price" NullDisplayText="уточнить">
									<ControlStyle Width="50px" />
								</asp:BoundField>
								<asp:TemplateField HeaderText="Кол-во">
									<ItemTemplate>
										<asp:TextBox ID="txtBuyCount" runat="server"></asp:TextBox>
									</ItemTemplate>
									<ControlStyle Width="50px" />
								</asp:TemplateField>
								<asp:ButtonField ButtonType="Image" CommandName="delete" ImageUrl="~/App_Themes/Main/Icons/120px-Dialog-error.svg.png"
									Text="Удалить">
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
						<table>
							<tr>
								<td width="90px" align="left">
									<span>Покупатель :</span>
								</td>
								<td align="left">
									<asp:DropDownList ID="drpBuyer" runat="server" Width="120px">
									</asp:DropDownList>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						&nbsp; <span>Итого :</span>
					</td>
				</tr>
				<tr>
					<td>
						<button id="btnBuy" runat="server" style="width: 100%">
							<span><em>Оформить покупку</em></span></button>
					</td>
				</tr>
			</table>
			<div class="clear">
			</div>
		</div>
	</div>
	<div class="Widget_Body_bottom">
		<span></span>
	</div>
</asp:Content>
