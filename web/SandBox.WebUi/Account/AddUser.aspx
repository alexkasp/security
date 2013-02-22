<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="SandBox.WebUi.Account.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../Content/PageView.css"  />

<div id='page_header'>
    Добавление нового пользователя
</div>

<table class='panel'>
			<tbody>
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text'>
                        <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Назад к списку пользователей" NavigateUrl="~/Pages/Settings/Users.aspx">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
			</tbody>
</table>

<table class='form'>
			<tbody>
                <tr>
                  <td><div class="simple_text">Имя пользователя:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbUserName" runat="server" Width="200px">
                         <ValidationSettings ValidationGroup="AddResearchValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxTextBox>
                  </td>
                </tr>
                <tr>
                  <td><div class="simple_text">Пароль:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbPassword" runat="server" Width="200px" Password="True">
                         <ValidationSettings ValidationGroup="AddResearchValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxTextBox>
                  </td>
                </tr>
                <tr>
                  <td><div class="simple_text">Подтверждение пароля:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbPasswordConfirm" runat="server" Width="200px" 
                           Password="True">
                         <ValidationSettings ValidationGroup="AddResearchValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxTextBox>
                  </td>
                </tr>
                <tr>
                  <td><div class="simple_text">Группа:</div></td>
                  <td>
                       <dx:ASPxComboBox ID="cbRole" runat="server" ValueType="System.String" Width="200px">
                        <ValidationSettings ValidationGroup="AddResearchValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                    </ValidationSettings>
                       </dx:ASPxComboBox>
                  </td>
                </tr>
                <tr>
                  <td>
                  </td>
                  <td>
                      <dx:ASPxButton ID="btnCreate" runat="server" Text="Добавить" ValidationGroup="AddResearchValidationGroup" onclick="BtnCreateClick" AutoPostBack = "false">
                      </dx:ASPxButton>
                  </td>
                </tr>
			</tbody>
</table> 



</asp:Content>
