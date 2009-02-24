<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/SellerMaster.master" AutoEventWireup="true" CodeBehind="Seller.aspx.cs" Inherits="SSManagment.Seller" %>

<asp:Content ID="cntCategories" ContentPlaceHolderID="cphStandartCategories" runat="server">
	<table cellpadding="0" cellspacing="0">
		<tr>
			<td>
				<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
					<h2>
						�������</h2>
				</div>
				<div class="Widget_Body_container">
					<div class="Widget_Body_top"><span></span></div>
					<div class="Widget_Body_content">
						<div class="clear"></div>
						<div style="overflow: auto; width: 200px; background-color: #F7F7DE;">
							<asp:UpdatePanel ID="uplTree" runat="server" UpdateMode="Conditional">
								<ContentTemplate>
									<asp:TreeView ID="treeCategories" runat="server" ExpandDepth="1" AutoGenerateDataBindings="true" ForeColor="#333333" ShowLines="True" Height="100%" Width="200px" BackColor="#F7F7DE" OnSelectedNodeChanged="treeCategories_SelectedNodeChanged" Font-Size="8pt">
										<ParentNodeStyle ForeColor="White" BackColor="#6B696B" BorderColor="#FF6600" BorderStyle="Dashed" BorderWidth="1px" Font-Bold="True" NodeSpacing="1px" />
										<RootNodeStyle ForeColor="White" BackColor="#6B696B" BorderColor="#FF6600" BorderWidth="1px" Font-Bold="True" NodeSpacing="1px" />
									</asp:TreeView>
								</ContentTemplate>
								<Triggers>
									<asp:AsyncPostBackTrigger ControlID="treeCategories" EventName="DataBinding" />
								</Triggers>
							</asp:UpdatePanel>
						</div>
						<div class="clear"></div>
					</div>
				</div>
				<div class="Widget_Body_bottom"><span></span></div>
			</td>
		</tr>
	</table>
</asp:Content>
<asp:Content ID="cntProducts" ContentPlaceHolderID="cphStandartProducts" runat="server">
	<asp:UpdatePanel ID="uplProductsGrid" runat="server" UpdateMode="Always">
		<ContentTemplate>
			<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
				<h2>
					������</h2>
			</div>
			<div class="Widget_Body_container">
				<div class="Widget_Body_top"><span></span></div>
				<div class="Widget_Body_content">
					<div class="clear"></div>
					<table cellpadding="0" cellspacing="0" width="500px">
						<tr>
							<td align="center">
								<table>
									<tr>
										<td align="right">
											<asp:TextBox ID="txtFind" runat="server" Width="200px"></asp:TextBox>
										</td>
										<td>
											<button id="btnFind" runat="server">
												<span><em>�����</em></span></button>
										</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td>
								<asp:GridView ID="gvwProducts" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" CaptionAlign="Top" CellPadding="0" ForeColor="Black" GridLines="Vertical" OnRowCommand="gvwProducts_RowCommand" Width="99%" Font-Size="8pt" AllowSorting="True" OnRowCreated="gvwProducts_RowCreated" OnSorting="gvwProducts_Sorting" PagerStyle-Wrap="False" DataKeyNames="id" OnRowDataBound="gvwProducts_RowDataBound">
									<RowStyle BackColor="#F7F7DE" />
									<Columns>
										<asp:TemplateField HeaderText="id" Visible="False">
											<ItemTemplate>
												<asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:BoundField DataField="name" SortExpression="name" HeaderText="��������" ReadOnly="True" HeaderStyle-Wrap="False">
											<HeaderStyle Wrap="False" />
										</asp:BoundField>
										<asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
											<ItemTemplate>
												<asp:LinkButton ID="lbtnOrder" runat="server" CommandName="order" CausesValidation="false" CommandArgument='<%# Eval("id") %>'>
													<asp:Image ID="img" runat="server" ImageUrl="~/App_Themes/Main/Icons/24px-Dialog-information_on.svg.png" Width="16px" Height="16px" /></asp:LinkButton>
											</ItemTemplate>
										</asp:TemplateField>
										<asp:BoundField DataField="measure" SortExpression="measure" HeaderText="���." NullDisplayText="��" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="False">
											<ControlStyle Width="10px" />
											<HeaderStyle Wrap="False" />
											<ItemStyle HorizontalAlign="Center" />
										</asp:BoundField>
										<asp:BoundField DataField="bprice" DataFormatString="{0:C}" SortExpression="bprice" HeaderText="����" NullDisplayText="��������" ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="False">
											<ControlStyle Width="50px" />
											<HeaderStyle Wrap="False" />
											<ItemStyle HorizontalAlign="Right" />
										</asp:BoundField>
										<asp:TemplateField HeaderText="� �������" SortExpression="count" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Middle">
											<ItemTemplate>
												<asp:Label ID="lblCount" runat="server" Text='<%# Bind("count") %>' ForeColor="Black"></asp:Label>
												<span id="spanCountCalc" runat="server" visible="false">-
													<asp:Button ID="btnUnReserv" runat="server" Text='<%# Bind("reserveCount") %>' CommandName="unreserv" CausesValidation="false" CommandArgument='<%# Eval("id") %>' Height="18px" Font-Size="8pt" />
													= <span id="spanSum" runat="server" style="color: #33CC33; font-size: 9pt; font-weight: bold; font-style: normal; font-variant: normal"></span></span></ItemTemplate>
											<HeaderStyle Wrap="False" />
											<ItemStyle Wrap="False" Width="65px" />
										</asp:TemplateField>
										<asp:TemplateField HeaderText="���-��" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
											<ItemTemplate>
												<asp:TextBox ID="txtBuyCount" runat="server" Width="35px"></asp:TextBox>
											</ItemTemplate>
											<ControlStyle Width="40px" />
										</asp:TemplateField>
										<asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
											<ItemTemplate>
												<asp:ImageButton ID="ibtnAdd" runat="server" CausesValidation="false" CommandName="add" ToolTip="��������� � �������" ImageUrl="~/App_Themes/Main/Icons/24px-Ambox_emblem_plus.svg.png" CommandArgument='<%# Eval("id") %>' />
											</ItemTemplate>
											<ControlStyle Height="24px" Width="24px" />
										</asp:TemplateField>
										<asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
											<ItemTemplate>
												<asp:ImageButton ID="ibtnSale" runat="server" CausesValidation="false" CommandName="sale" CommandArgument='<%# Eval("id") %>' ImageUrl="~/App_Themes/Main/Icons/24px-Emblem-advertisement-dollar.svg.png" ToolTip="�������" />
											</ItemTemplate>
											<ControlStyle Height="24px" Width="24px" />
										</asp:TemplateField>
										<asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
											<ItemTemplate>
												<asp:ImageButton ID="ibtnReserv" runat="server" CausesValidation="false" CommandName="reserved" CommandArgument='<%# Eval("id") %>' ImageUrl="~/App_Themes/Main/Icons/24px-Emblem-symbolic-link.svg.png" ToolTip="���������������" />
											</ItemTemplate>
											<ControlStyle Height="24px" Width="24px" />
										</asp:TemplateField>
									</Columns>
									<EmptyDataTemplate>
										<center>
											<span>� �������� ���� ������� ��������� ���������!</span></center>
									</EmptyDataTemplate>
									<FooterStyle BackColor="#CCCC99" />
									<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
									<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
									<HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" Wrap="False" />
									<AlternatingRowStyle BackColor="White" />
								</asp:GridView>
								&nbsp;
							</td>
						</tr>
					</table>
					<div class="clear"></div>
				</div>
			</div>
			<div class="Widget_Body_bottom"><span></span></div>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cntBuy" ContentPlaceHolderID="cphStandartBuy" runat="server">
	<asp:UpdatePanel ID="uplShoppingCart" runat="server">
		<ContentTemplate>
			<table cellpadding="0" cellspacing="0">
				<tr>
					<td>
						<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
							<h2>
								������ ������� ( �������)</h2>
						</div>
						<div class="Widget_Body_container">
							<div class="Widget_Body_top"><span></span></div>
							<div class="Widget_Body_content">
								<div class="clear"></div>
								<table cellpadding="0" cellspacing="0" width="100%" style="margin-left: -3px">
									<tr>
										<td align="center">
											<asp:GridView ID="gvwShoppingCart" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" CellPadding="2" ForeColor="Black" GridLines="Vertical" OnRowCommand="gvwShoppingCart_RowCommand" OnRowDeleting="gvwShoppingCart_RowDeleting" Font-Size="7pt">
												<RowStyle BackColor="#F7F7DE" />
												<Columns>
													<asp:TemplateField HeaderText="id" Visible="False">
														<ItemTemplate>
															<asp:Label ID="lblID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
													<asp:BoundField HeaderText="�����" DataField="name"></asp:BoundField>
													<asp:BoundField HeaderText="����" DataField="bprice" DataFormatString="{0:C}" NullDisplayText="��������" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
													<asp:TemplateField HeaderText="���-��">
														<ItemTemplate>
															<asp:TextBox ID="txtBuyCount" OnTextChanged="txtBuyCount_Click" runat="server" Text='<%# Eval("BuyCount") %>' Width="20px" AutoPostBack="True"></asp:TextBox>
														</ItemTemplate>
														<ControlStyle Width="27px" />
													</asp:TemplateField>
													<asp:BoundField HeaderText="�����" DataField="ResultPrice" DataFormatString="{0:C}" NullDisplayText="0" ItemStyle-HorizontalAlign="Right"></asp:BoundField>
													<asp:TemplateField ShowHeader="False">
														<ItemTemplate>
															<asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="delete" CommandArgument='<%# Eval("id") %>' ImageUrl="~/App_Themes/Main/Icons/24px-Dialog-error.svg.png" ToolTip="�������" />
														</ItemTemplate>
														<ControlStyle Height="24px" Width="24px" />
													</asp:TemplateField>
												</Columns>
												<EmptyDataTemplate>
													<center>
														<span>������ ������� ����</span></center>
												</EmptyDataTemplate>
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
													<td align="left" style="width: 90px">
														<span>���������� :</span>
													</td>
													<td align="left">
														<asp:DropDownList ID="drpBuyer" runat="server" Width="120px" AutoPostBack="True">
														</asp:DropDownList>
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td>
											&nbsp; <span>����� :&nbsp;</span><asp:Label ID="lblSum" runat="server"></asp:Label>
										</td>
									</tr>
									<tr>
										<td>
											<button id="btnBuy" runat="server" style="width: 100%" onserverclick="btnBuy_Click">
												<span><em>�������� �������</em></span></button>
										</td>
									</tr>
								</table>
								<div class="clear"></div>
							</div>
						</div>
						<div class="Widget_Body_bottom"><span></span></div>
					</td>
				</tr>
				<tr>
					<td>
						<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
							<h2>
								�������������</h2>
						</div>
						<div class="Widget_Body_container">
							<div class="Widget_Body_top"><span></span></div>
							<div class="Widget_Body_content">
								<div class="clear"></div>
								<center>
									<button id="btnAdmin" runat="server" style="width: 180px" onserverclick="btnAdminClick">
										<span><em>�����������������</em></span></button>
									<br />
									<button id="btnReturn" runat="server" style="width: 180px" onserverclick="btnReturn_Click">
										<span><em>������� ������</em></span></button>
									<br />
									<button id="btnDemand" runat="server" style="width: 180px" onserverclick="btnDemand_Click">
										<span><em>����� �� �����</em></span></button>
								</center>
								<div class="clear"></div>
							</div>
						</div>
						<div class="Widget_Body_bottom"><span></span></div>
					</td>
				</tr>
			</table>
			<div id="modalBuyConfirm" runat="server" visible="false">
				<div class="overlay"></div>
				<div class="content">
					<center>
						<div>
							<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
								<center>
									<h2>
										�������� �������? </h2>
								</center>
							</div>
							<div class="Widget_Body_container">
								<div class="Widget_Body_top"><span></span></div>
								<div class="Widget_Body_content">
									<div class="clear"></div>
									<center>
										<table cellpadding="0" cellspacing="0">
											<tr>
												<td align="center" colspan="2">
													<asp:GridView ID="gvwShpingCartConfirm" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" CellPadding="2" ForeColor="Black" GridLines="Vertical" Font-Size="9pt">
														<RowStyle BackColor="#F7F7DE" />
														<Columns>
															<asp:BoundField HeaderText="�����" DataField="name">
																<ControlStyle Width="100%" />
																<ItemStyle HorizontalAlign="Left" />
															</asp:BoundField>
															<asp:BoundField HeaderText="����" DataFormatString="{0:C}" DataField="bprice" NullDisplayText="��������" ItemStyle-HorizontalAlign="Right">
																<ControlStyle Width="50px" />
																<ItemStyle HorizontalAlign="Right" />
															</asp:BoundField>
															<asp:BoundField HeaderText="���-��" DataField="BuyCount" NullDisplayText="0" ItemStyle-HorizontalAlign="Right">
																<ControlStyle Width="50px" />
																<ItemStyle HorizontalAlign="Right" />
															</asp:BoundField>
															<asp:BoundField HeaderText="�����" DataFormatString="{0:C}" DataField="ResultPrice" NullDisplayText="0" ItemStyle-HorizontalAlign="Right">
																<ControlStyle Width="50px" />
																<ItemStyle HorizontalAlign="Right" />
															</asp:BoundField>
														</Columns>
														<EmptyDataTemplate>
															<center>
																<span>������ ������� ����</span></center>
														</EmptyDataTemplate>
														<FooterStyle BackColor="#CCCC99" />
														<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
														<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
														<HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
														<AlternatingRowStyle BackColor="White" />
													</asp:GridView>
													<br />
													<span>����� :&nbsp;</span><asp:Label ID="lblShopConfirmSum" runat="server" Font-Size="12pt"></asp:Label>
												</td>
											</tr>
											<tr>
												<td colspan="2">
													<br />
												</td>
											</tr>
											<tr>
												<td>
													<button id="btnYes" runat="server" style="width: 75px;" onserverclick="btnYes_Click">
														<span><em>��</em></span></button>
												</td>
												<td>
													<button id="btnCancel" runat="server" style="width: 75px;" onserverclick="btnCancel_Click">
														<span><em>���</em></span></button>
												</td>
											</tr>
										</table>
									</center>
									<div class="clear"></div>
								</div>
							</div>
							<div class="Widget_Body_bottom"><span></span></div>
						</div>
					</center>
				</div>
			</div>
			<div id="modalUnReservConfirm" runat="server" visible="false">
				<div class="overlay"></div>
				<div class="content">
					<center>
						<div>
							<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
								<center>
									<h2>
										������� � ������� ���������������� �����? </h2>
								</center>
							</div>
							<div class="Widget_Body_container">
								<div class="Widget_Body_top"><span></span></div>
								<div class="Widget_Body_content">
									<div class="clear"></div>
									<center>
										<table>
											<tr>
												<td>
													<asp:HiddenField ID="hdnUnReservId" runat="server" />
													<button id="btnUnReservYes" runat="server" style="width: 75px;" onserverclick="btnUnReservYes_Click">
														<span><em>��</em></span></button>
												</td>
												<td>
													<button id="btnUnReservNo" runat="server" style="width: 75px;" onserverclick="btnUnReservNo_Click">
														<span><em>���</em></span></button>
												</td>
											</tr>
										</table>
									</center>
									<div class="clear"></div>
								</div>
							</div>
							<div class="Widget_Body_bottom"><span></span></div>
						</div>
					</center>
				</div>
			</div>
			<div id="modalWarningConfirm" runat="server" visible="false">
				<div class="overlay"></div>
				<div class="content">
					<center>
						<div>
							<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
								<center>
									<h2 style="color: #FF6600; font-weight: bold">
										�������� !!! </h2>
								</center>
							</div>
							<div class="Widget_Body_container">
								<div class="Widget_Body_top"><span></span></div>
								<div class="Widget_Body_content">
									<div class="clear"></div>
									<center>
										<table width="100%">
											<tr>
												<td>
													<asp:Label ID="lblWarning" runat="server"></asp:Label>
													<br />
												</td>
											</tr>
											<tr>
												<td>
													���������� ����� � ��������� �������������� �� ���� ���������, ������ ������� �������� ��� ������� ��� ��������� ����������.
												</td>
											</tr>
											<tr>
												<td>
													<br />
													<button id="btnWarningOk" runat="server" style="width: 75px;" onserverclick="btnWarningOk_Click">
														<span><em>��</em></span></button>
												</td>
											</tr>
										</table>
									</center>
									<div class="clear"></div>
								</div>
							</div>
							<div class="Widget_Body_bottom"><span></span></div>
						</div>
					</center>
				</div>
			</div>
			<div id="modalDemand" runat="server" visible="false">
				<div class="overlay"></div>
				<div class="content">
					<center>
						<div>
							<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
								<center>
									<h2>
										�������� ����� �� ����� ������� �� ����������� � ������������.</h2>
								</center>
							</div>
							<div class="Widget_Body_container">
								<div class="Widget_Body_top"><span></span></div>
								<div class="Widget_Body_content">
									<div class="clear"></div>
									<center>
										<table width="100%">
											<tr>
												<td align="center" colspan="2">
													<span>�������� ������ :&nbsp;</span><asp:TextBox ID="txtDemandProduct" runat="server" Width="250px"></asp:TextBox>
													<br />
													<br />
												</td>
											</tr>
											<tr>
												<td>
													<button id="btnDemandOk" runat="server" style="width: 75px;" onserverclick="btnDemandOk_Click">
														<span><em>��</em></span></button>
												</td>
												<td>
													<button id="btnDemandNo" runat="server" style="width: 75px;" onserverclick="btnDemandNo_Click">
														<span><em>���</em></span></button>
												</td>
											</tr>
										</table>
									</center>
									<div class="clear"></div>
								</div>
							</div>
							<div class="Widget_Body_bottom"><span></span></div>
						</div>
					</center>
				</div>
			</div>
			<div id="modalReturn" runat="server" visible="false">
				<div class="overlay"></div>
				<div class="content">
					<center>
						<div>
							<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
								<center>
									<h2>
										������� ������ </h2>
								</center>
							</div>
							<div class="Widget_Body_container">
								<div class="Widget_Body_top"><span></span></div>
								<div class="Widget_Body_content">
									<div class="clear"></div>
									<center>
										<table width="100%">
											<tr>
												<td align="center">
													<table cellpadding="0" cellspacing="0" width="100%">
														<tr>
															<td align="right">
																������� ��� ����� �������:&nbsp;
															</td>
															<td align="left">
																<asp:TextBox ID="txtReturnProductCode" runat="server" Width="150px"></asp:TextBox>
															</td>
														</tr>
														<tr>
															<td align="right">
																������� ���� �������:&nbsp;
															</td>
															<td align="left">
																<asp:TextBox ID="txtReturnProductSoldDate" runat="server"></asp:TextBox>
															</td>
														</tr>
														<tr>
															<td colspan="2" align="center">
																<button id="btnReturnShowProducts" runat="server" style="width: 185px;" onserverclick="btnReturnShowProducts_Click">
																	<span><em>�������� ������ �������</em></span></button>
															</td>
														</tr>
														<tr>
															<td colspan="2" align="center">
																<asp:GridView ID="gvwReturn" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" CellPadding="2" ForeColor="Black" GridLines="Vertical" Font-Size="9pt" OnRowCommand="gvwReturn_RowCommand" OnRowDeleting="gvwReturn_RowDeleting">
																	<RowStyle BackColor="#F7F7DE" />
																	<Columns>
																		<asp:BoundField HeaderText="�����" DataField="name">
																			<ControlStyle Width="100%" />
																			<ItemStyle HorizontalAlign="Left" />
																		</asp:BoundField>
																		<asp:BoundField HeaderText="����" DataFormatString="{0:C}" DataField="bprice" NullDisplayText="��������" ItemStyle-HorizontalAlign="Right">
																			<ControlStyle Width="50px" />
																			<ItemStyle HorizontalAlign="Right" />
																		</asp:BoundField>
																		<asp:BoundField HeaderText="���-��" DataField="BuyCount" NullDisplayText="0" ItemStyle-HorizontalAlign="Right">
																			<ControlStyle Width="50px" />
																			<ItemStyle HorizontalAlign="Right" />
																		</asp:BoundField>
																		<asp:BoundField HeaderText="�����" DataFormatString="{0:C}" DataField="ResultPrice" NullDisplayText="0" ItemStyle-HorizontalAlign="Right">
																			<ControlStyle Width="50px" />
																			<ItemStyle HorizontalAlign="Right" />
																		</asp:BoundField>
																		<asp:TemplateField ShowHeader="False">
																			<ItemTemplate>
																				<asp:TextBox ID="txtReturnCount" runat="server" Width="20px" AutoPostBack="True"></asp:TextBox>
																			</ItemTemplate>
																			<ControlStyle Width="27px" />
																		</asp:TemplateField>
																		<asp:TemplateField ShowHeader="False">
																			<ItemTemplate>
																				<asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" CommandName="return" CommandArgument='<%# Eval("id") %>' ImageUrl="~/App_Themes/Main/Icons/48px-Ambox_emblem_arrow.svg.png" ToolTip="�������" />
																			</ItemTemplate>
																			<ControlStyle Height="24px" Width="24px" />
																		</asp:TemplateField>
																	</Columns>
																	<EmptyDataTemplate>
																		<center>
																			<span>������ ������� ����</span></center>
																	</EmptyDataTemplate>
																	<FooterStyle BackColor="#CCCC99" />
																	<PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
																	<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
																	<HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
																	<AlternatingRowStyle BackColor="White" />
																</asp:GridView>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td colspan="2">
													<br />
													<br />
													<button id="btnReturnOk" runat="server" style="width: 75px;" onserverclick="btnReturnOk_Click">
														<span><em>��</em></span></button>
												</td>
											</tr>
										</table>
									</center>
									<div class="clear"></div>
								</div>
							</div>
							<div class="Widget_Body_bottom"><span></span></div>
						</div>
					</center>
				</div>
			</div>
			<div id="modalReturnConfirm" runat="server" visible="false">
				<div class="overlay"></div>
				<div class="content">
					<center>
						<div>
							<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
								<center>
									<h2>
										������� ����� � �������? </h2>
								</center>
							</div>
							<div class="Widget_Body_container">
								<div class="Widget_Body_top"><span></span></div>
								<div class="Widget_Body_content">
									<div class="clear"></div>
									<center>
										<table width="100%">
											<tr>
												<td>
													<asp:HiddenField ID="HiddenField1" runat="server" />
													<button id="btnReturnConfirmOk" runat="server" style="width: 75px;" onserverclick="btnReturnConfirmOk_Click">
														<span><em>��</em></span></button>
												</td>
												<td>
													<button id="ReturnConfirmNo" runat="server" style="width: 75px;" onserverclick="ReturnConfirmNo_Click">
														<span><em>���</em></span></button>
												</td>
											</tr>
										</table>
									</center>
									<div class="clear"></div>
								</div>
							</div>
							<div class="Widget_Body_bottom"><span></span></div>
						</div>
					</center>
				</div>
			</div>
			<div id="modalOrderConfirm" runat="server" visible="false">
				<div class="overlay"></div>
				<div class="content">
					<center>
						<div>
							<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
								<center>
									<h2>
										������ ������ �� ������� ������? </h2>
								</center>
							</div>
							<div class="Widget_Body_container">
								<div class="Widget_Body_top"><span></span></div>
								<div class="Widget_Body_content">
									<div class="clear"></div>
									<center>
										<table width="100%">
											<tr>
												<td>
													<asp:HiddenField ID="hdnOrder" runat="server" />
													<button id="btnOrderYes" runat="server" style="width: 75px;" onserverclick="btnOrderYes_Click">
														<span><em>��</em></span></button>
												</td>
												<td>
													<button id="btnOrderNo" runat="server" style="width: 75px;" onserverclick="btnOrderNo_Click">
														<span><em>���</em></span></button>
												</td>
											</tr>
										</table>
									</center>
									<div class="clear"></div>
								</div>
							</div>
							<div class="Widget_Body_bottom"><span></span></div>
						</div>
					</center>
				</div>
			</div>
			<div id="modalReserv" runat="server" visible="false">
				<div class="overlay"></div>
				<div class="content">
					<center>
						<div>
							<div class="Widget_heading_container"><span class="Widget_heading_container_Span"></span>
								<center>
									<h2>
										��������������� ����� </h2>
								</center>
							</div>
							<div class="Widget_Body_container">
								<div class="Widget_Body_top"><span></span></div>
								<div class="Widget_Body_content">
									<div class="clear"></div>
									<center>
										<table width="100%">
											<tr>
												<td colspan="2">
													<table cellpadding="0" width="100%">
														<tr>
															<td align="right">
																<span>�������� ������ :</span>
															</td>
															<td align="left">
																<asp:Label ID="lblResrvName" runat="server"></asp:Label>
																<asp:HiddenField ID="hdnResrvID" runat="server" />
															</td>
														</tr>
														<tr>
															<td align="right">
																<span>� ������� :</span>
															</td>
															<td align="left">
																<asp:Label ID="lblResrvCount" runat="server"></asp:Label>
															</td>
														</tr>
														<tr>
															<td align="right">
																<span>��������������� :</span>
															</td>
															<td align="left">
																<asp:Label ID="lblResrvReserved" runat="server"></asp:Label>
															</td>
														</tr>
														<tr>
															<td align="right">
																<span>���� :</span>
															</td>
															<td align="left">
																<asp:Label ID="lblResrvBprice" runat="server"></asp:Label>
															</td>
														</tr>
														<tr>
															<td align="right">
																<span>��������������� :</span>
															</td>
															<td align="left">
																<asp:TextBox ID="txtResrvBuyCount" runat="server" OnTextChanged="txtResrvBuyCount_TextChanged"></asp:TextBox>
															</td>
														</tr>
														<tr>
															<td align="right">
																<span>����� :</span>
															</td>
															<td align="left">
																<asp:Label ID="lblResrvSum" runat="server"></asp:Label>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td colspan="2" align="center">
													<span>����������� ����� ��</span>
													<asp:Calendar ID="calResrvReservDateTo" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" OnSelectionChanged="calResrvReservDateTo_SelectionChanged">
														<SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
														<SelectorStyle BackColor="#CCCCCC" />
														<WeekendDayStyle BackColor="#FFFFCC" />
														<TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
														<OtherMonthDayStyle ForeColor="#808080" />
														<NextPrevStyle VerticalAlign="Bottom" />
														<DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
														<TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
													</asp:Calendar>
												</td>
											</tr>
											<tr>
												<td>
													<button id="btnReservYes" runat="server" style="width: 75px;" onserverclick="btnReservYes_Click">
														<span><em>��</em></span></button>
												</td>
												<td>
													<button id="btnReservNo" runat="server" style="width: 75px;" onserverclick="btnReservNo_Click">
														<span><em>���</em></span></button>
												</td>
											</tr>
										</table>
									</center>
									<div class="clear"></div>
								</div>
							</div>
							<div class="Widget_Body_bottom"><span></span></div>
						</div>
					</center>
				</div>
			</div>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
