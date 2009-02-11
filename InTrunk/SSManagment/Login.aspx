<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SSManagment.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <table style="width: 100%;">
            <tr>
                <td>
                    Учётная запись:&nbsp;<asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Пароль:&nbsp;<asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                    
                </td>
                <td>
									<asp:Button ID="btnLogin" runat="server" Text="Вход" onclick="btnLogin_Click" /></td>
            </tr>
        </table>
    </center>
    </form>
</body>
</html>
