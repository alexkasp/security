<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="CreateNewVlir.aspx.cs" Inherits="SandBox.WebUi.Pages.Information.CreateNewVlir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div style="padding:10px;">
    <br />
    <table style="width: 100%;">
        <tr>
            <td style="width: 115px">
                    <div class="table_text" __designer:mapid="35a">
                        Имя:</div>
                </td>
            <td>
                       <dx:ASPxTextBox ID="tbLir" runat="server" Width="200px">
                         <ValidationSettings ValidationGroup="CreateEtalonMachineValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxTextBox>
                  </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 115px">
                    <div class="table_text" __designer:mapid="35a">
                        Тип ОС:</div>
                </td>
            <td>
                       <dx:ASPxComboBox ID="cbSystem" runat="server" ValueType="System.String" Width="200px">
                       <ValidationSettings ValidationGroup="CreateEtalonMachineValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxComboBox>
                  </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 115px">
                    <div class="table_text" __designer:mapid="35a">
                        MAC-адрес:</div>
                </td>
            <td>
                       <dx:ASPxTextBox ID="tbLirMac" runat="server" Width="200px">
                         <ValidationSettings ValidationGroup="CreateEtalonMachineValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxTextBox>
                  </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 115px">
                <dx:ASPxButton ID="BAdd" runat="server" onclick="BAdd_Click" 
                    Text="Добавить ВЛИР">
                </dx:ASPxButton>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    
</div>
</asp:Content>
