<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="CreateEtalonMachine.aspx.cs" Inherits="SandBox.WebUi.Pages.Information.CreateEtalonMachine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />

<div id='page_header'>
    Добавление новой ЛИР
</div>

<table class='panel'>
			<tbody>
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text'>
                        <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Назад к списку ресурсов" NavigateUrl="~/Pages/Information/Resources.aspx">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
			</tbody>
</table>

<table class='form'>
			<tbody>
                <tr>
                  <td><div class="simple_text">Имя новой ЛИР:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbLir" runat="server" Width="200px">
                         <ValidationSettings ValidationGroup="CreateEtalonMachineValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxTextBox>
                  </td>
                </tr>
                <tr>
                  <td><div class="simple_text">Система:</div></td>
                  <td>
                       <dx:ASPxComboBox ID="cbSystem" runat="server" ValueType="System.String" Width="200px">
                       <ValidationSettings ValidationGroup="CreateEtalonMachineValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxComboBox>
                  </td>

                </tr>
                <tr>
                  <td><div class="simple_text">Тип среды:</div></td>
                  <td>
                       <dx:ASPxComboBox ID="cbEnvType" runat="server" ValueType="System.String" Width="100px">
                       <ValidationSettings ValidationGroup="CreateEtalonMachineValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxComboBox>
                  </td>
                </tr>
                <tr>
                  <td></td>
                  <td>
                      <dx:ASPxButton ID="btnCreate" runat="server" Text="Добавить" ValidationGroup="CreateEtalonMachineValidationGroup" onclick="BtnCreateClick" AutoPostBack="false">
                      </dx:ASPxButton>
                  </td>
                </tr>
			</tbody>
</table>
</asp:Content>

