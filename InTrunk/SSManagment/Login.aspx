<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SSManagment.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="App_Themes/Main/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="height: 600px; width: 100%;">
    <table style="height: 100%; width: 100%">
        <tr>
            <td valign="middle" align="center">
                <table>
                    <tr>
                        <td align="right">
                            <span>Учётная запись:</span>
                        </td>
                        <td align="left" width="130px">
                            <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                        </td>
                        <td rowspan="2" align="left">
                            <button id="btnLogin" runat="server" tabindex="2" onserverclick="btnLogin_Click1">
                                <span><em>Войти</em></span></button>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <span>Пароль:</span>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPassword" runat="server" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
