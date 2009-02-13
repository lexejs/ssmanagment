﻿<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/AdminMaster.master"
    AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="SSManagment.Admin" %>

<asp:Content ID="cntMenu" ContentPlaceHolderID="cphAdminMenu" runat="server">
    <center>
        <div class="Widget_heading_container">
            <span class="Widget_heading_container_Span"></span>
            <h2>
                Меню</h2>
        </div>
        <div class="Widget_Body_container">
            <div class="Widget_Body_top">
                <span></span>
            </div>
            <div class="Widget_Body_content">
                <div class="clear">
                </div>
                <center>
                    <table>
                        <tr>
                            <td>
                                <button id="btnGoBack" runat="server" style="width: 180px" onserverclick="btnGoBack_Click">
                                <span><em>Вернуться к продажам</em></span></button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <button id="btnShowGroups" runat="server" style="width: 180px" onserverclick="btnShowGroups_Click">
                                    <span><em>Отобразить Группы</em></span></button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <button id="btnShowItems" runat="server" style="width: 180px" onserverclick="btnShowItems_Click">
                                    <span><em>Отобразить Товары</em></span></button>
                            </td>
                        </tr>
                    </table>
                </center>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="Widget_Body_bottom">
            <span></span>
        </div>
    </center>
</asp:Content>
<asp:Content ID="cntObject" ContentPlaceHolderID="cphAdminObject" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table runat="server" id="tblGroup" visible="false" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <center>
                                    <div class="Widget_heading_container">
                                        <span class="Widget_heading_container_Span"></span>
                                        <h2>
                                            Группы</h2>
                                    </div>
                                    <div class="Widget_Body_container">
                                        <div class="Widget_Body_top">
                                            <span></span>
                                        </div>
                                        <div class="Widget_Body_content">
                                            <div class="clear">
                                            </div>
                                            <center>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:ListBox ID="lstGroup" runat="server" Height="300px" Width="300px" OnSelectedIndexChanged="lstGroup_SelectedIndexChanged"
                                                                AutoPostBack="True"></asp:ListBox>
                                                        </td>
                                                        <td>
                                                            <asp:ListBox ID="lstSubGroup" runat="server" Height="300px" Width="300px" TabIndex="40">
                                                            </asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <table>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="txtGroupName" runat="server" Width="190px" TabIndex="1"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <button id="btnUpdateGroup" runat="server" style="width: 95px" tabindex="2" onserverclick="btnUpdateGroup_Click">
                                                                            <span><em>Изменить</em></span></button>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <button id="btnAddGroup" runat="server" style="width: 95px" tabindex="2" onserverclick="btnAddGroup_Click">
                                                                            <span><em>Добавить</em></span></button>
                                                                    </td>
                                                                    <td>
                                                                        <button id="btnDelGroup" runat="server" style="width: 95px" tabindex="3" onserverclick="btnDelGroup_Click">
                                                                            <span><em>Удалить</em></span></button>
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <button id="btnAttachTo" runat="server" style="width: 95px" tabindex="3" onserverclick="btnAttachTo_Click">
                                                                            <span><em>Привязать к</em></span></button>
                                                                    </td>
                                                                    <td colspan="2">
                                                                        <asp:DropDownList ID="ddlAttachTo" runat="server" Width="190px">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td valign="top">
                                                            <table>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="txtSubGroupName" runat="server" Width="190px" TabIndex="44"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        <button id="btnUpdateSubGroup" runat="server" style="width: 95px" tabindex="45" onserverclick="btnUpdateSubGroup_Click">
                                                                            <span><em>Изменить</em></span></button>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <button id="btnAddSubGroup" runat="server" style="width: 95px" tabindex="46" onserverclick="btnAddSubGroup_Click">
                                                                            <span><em>Добавить</em></span></button>
                                                                    </td>
                                                                    <td>
                                                                        <button id="btnDellSubGroup" runat="server" style="width: 95px" tabindex="47" onserverclick="btnDellSubGroup_Click">
                                                                            <span><em>Удалить</em></span></button>
                                                                    </td>
                                                                    <td>
                                                                        <button id="btnMoveToGroup" runat="server" style="width: 95px" tabindex="48" onserverclick="btnMoveToGroup_Click">
                                                                            <span><em>Отвязать</em></span></button>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </center>
                                            <div class="clear">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="Widget_Body_bottom">
                                        <span></span>
                                    </div>
                                </center>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" id="tblItems" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <center>
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
                                            <center>
                                                <table>
                                                    <tr>
                                                        <td rowspan="3">
                                                            <div style="overflow: auto;  width: 200px; background-color: #F7F7DE;">
                                                                <asp:TreeView ID="treeCategories" runat="server" ExpandDepth="1" AutoGenerateDataBindings="true"
                                                                    ForeColor="#333333" ShowLines="True" Width="200px" BackColor="#F7F7DE"
                                                                    OnSelectedNodeChanged="treeCategories_SelectedNodeChanged">
                                                                    <ParentNodeStyle ForeColor="White" BackColor="#6B696B" BorderColor="#FF6600" BorderStyle="Dashed"
                                                                        BorderWidth="1px" Font-Bold="True" NodeSpacing="1px" />
                                                                    <RootNodeStyle ForeColor="White" BackColor="#6B696B" BorderColor="#FF6600" BorderWidth="1px"
                                                                        Font-Bold="True" NodeSpacing="1px" />
                                                                </asp:TreeView>
                                                            </div>
                                                        </td>
                                                        <td valign="top">
                                                            <asp:ListBox ID="lstItems" runat="server" Height="300px" Width="410px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="lstItems_SelectedIndexChanged"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <button id="Button1" runat="server" tabindex="31" style="width: 130px">
                                                                <span><em>Изменить</em></span></button>&nbsp;
                                                            <button id="Button2" runat="server" tabindex="33" style="width: 130px">
                                                                <span><em>Снять с продажи</em></span></button>&nbsp;
                                                            <button id="Button3" runat="server" tabindex="35" style="width: 130px">
                                                                <span><em>Снять с продажи</em></span></button>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td align="right">
                                                                        Название:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemName" runat="server"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        Колличество:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemCount" runat="server"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        Измеряется в:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemMeasure" runat="server"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        Цена поставки:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemAdminPrice" runat="server"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        Процент:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemPct" runat="server"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        Мин. колличество для заказа:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemOrderCount" runat="server"></asp:TextBox><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:CheckBox ID="chbItemCanGiveBack" runat="server" Text="Разрешать возврат" /><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:CheckBox ID="chbItemIsActive" runat="server" Text="Доступен" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </center>
                                            <div class="clear">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="Widget_Body_bottom">
                                        <span></span>
                                    </div>
                                </center>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
