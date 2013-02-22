<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="CreateMachine.aspx.cs" Inherits="SandBox.WebUi.Pages.Information.CreateMachine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />

<div id='page_header'>
    Создание ВЛИР на основе ЛИР
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                      <ContentTemplate>  
<table class='form'>
			<tbody>
                <tr>
                  <td><div class="simple_text">Имя новой ВЛИР:</div></td>
                  <td>
                                      
                       <dx:ASPxTextBox ID="tbLir" runat="server" Width="200px" 
                              ontextchanged="tbLir_TextChanged" AutoPostBack="True">
                         <ValidationSettings ValidationGroup="CreateEtalonMachineValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxTextBox>
                          <dx:ASPxLabel ID="LValidation" runat="server" Visible="False">
                          </dx:ASPxLabel>
                  </td>
                </tr>
                <tr>
                  <td><div class="simple_text">Эталонная ЛИР:</div></td>
                  <td>
                       <dx:ASPxComboBox ID="cbEtalon" runat="server" ValueType="System.String" Width="200px">
                       </dx:ASPxComboBox>
                  </td>

                </tr>
                <tr>
                  <td></td>
                  <td>
                      <dx:ASPxButton ID="btnCreate" runat="server" Text="Создать" ValidationGroup="CreateEtalonMachineValidationGroup" onclick="BtnCreateClick" AutoPostBack = "false">
                      </dx:ASPxButton>
                  </td>
                </tr>
			</tbody>
</table> 
 </ContentTemplate>
                      </asp:UpdatePanel>
</asp:Content>

