<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SSManagment.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<link href="App_Themes/Main/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
	<form id="loginForm" runat="server" style="height: 600px; width: 100%;">
	<asp:ScriptManager ID="mainScriptManager" runat="server" />
	<table style="height: 100%; width: 100%">
		<tr>
			<td valign="middle" align="center">
				<table>
					<tr>
						<td>
							<div class="Widget_heading_container">
								<span class="Widget_heading_container_Span"></span>
								<h2>
									Вход в систему</h2>
							</div>
							<div class="Widget_Body_container">
								<div class="Widget_Body_top">
									<span></span>
								</div>
								<div class="Widget_Body_content">
									<div class="clear">
									</div>
									<asp:UpdatePanel ID="uplLogin" runat="server" UpdateMode="Always">
										<ContentTemplate>
											<table>
												<tr>
													<td align="right">
														<span>Учётная запись:</span>
													</td>
													<td align="left" width="130px">
														<asp:TextBox ID="txtLogin" runat="server" TabIndex="1"></asp:TextBox>
													</td>
													<td rowspan="2" align="left">
														<button id="btnLogin" runat="server" tabindex="3" onserverclick="btnLogin_Click1">
															<span><em>Войти</em></span></button>
													</td>
												</tr>
												<tr>
													<td align="right">
														<span>Пароль:
														</span>
													</td>
													<td align="left">
														<asp:TextBox ID="txtPassword" runat="server" TabIndex="2" TextMode="Password"></asp:TextBox>
													</td>
												</tr>
												<tr id="tdPassMsg" runat="server" visible="false">
													<td colspan="3">
														<asp:Label ID="lblIncorectPass" runat="server" Text="Неправильный логин или пароль!" ForeColor="#CC0000" Font-Bold="True"></asp:Label>
													</td>
												</tr>
											</table>
										</ContentTemplate>
									</asp:UpdatePanel>
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
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
