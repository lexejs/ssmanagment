<%@ Page Title="" Language="C#" MasterPageFile="~/App_Master/AdminMaster.master"
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
                                <button id="btnShowGroups" runat="server" style="width: 180px" onserverclick="btnShowItems_Click">
                                    <span><em>Отобразить Группы</em></span></button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <button id="btnShowItems" runat="server" style="width: 180px" onserverclick="btnShowItems_Click">
                                    <span><em>Отобразить Товары</em></span></button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <button id="btnShowBuyers" runat="server" style="width: 180px" onserverclick="btnShowItems_Click">
                                    <span><em>Отобразить покупателей</em></span></button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <button id="btnShowSellers" runat="server" style="width: 180px" onserverclick="btnShowItems_Click">
                                    <span><em>Отобразить продавцов</em></span></button>
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
                    <table runat="server" id="tblGroup" visible="true" cellpadding="0" cellspacing="0">
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
                                                                AutoPostBack="True"></asp:ListBox><br />
                                                            <asp:Label ID="lblPGroup" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:ListBox ID="lstSubGroup" runat="server" Height="300px" Width="300px" 
                                                                TabIndex="40" AutoPostBack='true' 
                                                                onselectedindexchanged="lstSubGroup_SelectedIndexChanged">
                                                            </asp:ListBox><br />
                                                            <asp:Label ID="lblSubGroup" runat="server" Text=""></asp:Label>
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
                    <table runat="server" id="tblItems" cellpadding="0" cellspacing="0" visible="false">
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
                                                        <td rowspan="3" style="text-decoration: none;vertical-align: top;text-align: left;">
                                                            <div style="overflow: auto; width: 200px; background-color: #F7F7DE;">
                                                                <asp:TreeView ID="treeCategories" runat="server" ExpandDepth="1" AutoGenerateDataBindings="true"
                                                                    ForeColor="#333333" ShowLines="True" Width="200px" BackColor="#F7F7DE" OnSelectedNodeChanged="treeCategories_SelectedNodeChanged">
                                                                    <ParentNodeStyle ForeColor="White" BackColor="#6B696B" BorderColor="#FF6600" BorderStyle="Dashed"
                                                                        BorderWidth="1px" Font-Bold="True" NodeSpacing="1px" />
                                                                    <RootNodeStyle ForeColor="White" BackColor="#6B696B" BorderColor="#FF6600" BorderWidth="1px"
                                                                        Font-Bold="True" NodeSpacing="1px" />
                                                                </asp:TreeView>
                                                            </div>
                                                        </td>
                                                        <td valign="bottom">
                                                            <asp:Label ID="lblGroupName" runat="server" Text=""></asp:Label><br />
                                                            <asp:ListBox ID="lstItems" runat="server" Height="300px" Width="410px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="lstItems_SelectedIndexChanged"></asp:ListBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top">
                                                            <table width="100%">
                                                                <tr>
                                                                    <td align="right">
                                                                        Название:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemName" runat="server"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemName"
                                                                            ErrorMessage="RequiredFieldValidator" ValidationGroup="itemChanged">*</asp:RequiredFieldValidator>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        Колличество:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemCount" runat="server"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="revItemCount" runat="server" ControlToValidate="txtItemCount"
                                                                            Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="\d{1,10}"
                                                                            ValidationGroup="itemChanged">*</asp:RegularExpressionValidator>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        Измеряется в:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemMeasure" runat="server"></asp:TextBox>
                                                                        <br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        Цена поставки:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemAdminPrice" runat="server"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtItemAdminPrice"
                                                                            Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="\d{1,9}"
                                                                            ValidationGroup="itemChanged">*</asp:RegularExpressionValidator><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        Процент:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemPct" runat="server"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtItemPct"
                                                                            Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="\d{1,3}(.)?(,)?\d{0,5}"
                                                                            ValidationGroup="itemChanged">*</asp:RegularExpressionValidator><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        Мин. колличество для заказа:
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="txtItemOrderCount" runat="server"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtItemOrderCount"
                                                                            Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="\d{1,4}"
                                                                            ValidationGroup="itemChanged">*</asp:RegularExpressionValidator><br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:CheckBox ID="chbItemCanGiveBack" runat="server" Text="Разрешать возврат" /><br />
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:CheckBox ID="chbItemIsActive" runat="server" Text="Доступен" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="justify">
                                                            <button id="btnItemUpdate" runat="server" tabindex="31" style="width: 130px" validationgroup="itemChanged"
                                                                onserverclick="btnItemUpdate_Click">
                                                                <span><em>Изменить</em></span></button>&nbsp;
                                                            <button id="Button2" runat="server" tabindex="33" style="width: 130px" onserverclick="btnItemAdd_Click">
                                                                <span><em>Добавить</em></span></button>&nbsp;<br />
                                                            <button id="Button3" runat="server" tabindex="35" style="width: 150px" onserverclick="btnItemMove_Click">
                                                                <span><em>Привязать к группе:</em></span></button>
                                                            <asp:DropDownList ID="ddlItemToGroup" runat="server">
                                                            </asp:DropDownList>
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
                    <table runat="server" id="tblBuyers" visible="false" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <center>
                                    <div class="Widget_heading_container">
                                        <span class="Widget_heading_container_Span"></span>
                                        <h2>
                                            ПОКУПАТЕЛИ</h2>
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
                                                            <asp:ListBox ID="lstBuyers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstBuyers_SelectedIndexChanged"
                                                                Width="200px" Height="100px"></asp:ListBox>
                                                        </td>
                                                        <td>
                                                            Имя<br />
                                                            <asp:TextBox ID="txtBuyerName" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Скидка (%)<br />
                                                            <asp:TextBox ID="txtBuyerPct" runat="server" Width="70"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkBuyerCanBuyOnTick" runat="server" Text="Покупка в кредит" /><br />
                                                            <asp:CheckBox ID="chkBuyerIsActive" runat="server" Text="Активен" />
                                                        </td>
                                                        <td>
                                                            <button id="btnBuyerUpd" runat="server" tabindex="60" style="width: 130px" onserverclick="btnBuyerUpdate_Click">
                                                                <span><em>Изменить</em></span></button>
                                                            <button id="btnBuyerAdd" runat="server" tabindex="65" style="width: 130px" validationgroup="itemChanged"
                                                                onserverclick="btnBuyerAdd_Click">
                                                                <span><em>Добавить</em></span></button>
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
                    <td>
            </tr>
        </table>
    </div>
</asp:Content>
