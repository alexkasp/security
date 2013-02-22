<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="NewResearch.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.NewResearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />

<div id='page_header'>
    Добавление нового исследования
</div>

<table class='panel'>
			<tbody>
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text'>
                        <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Назад к списку исследований" NavigateUrl="~/Pages/Research/Current.aspx">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
			</tbody>
</table>

<table class='form'>
			<tbody>
                <tr>
                  <td><div class='table_header'>Основные параметры</div></td>
                  <td><hr /></td>
                </tr>

                <tr>
                  <td><div class="table_text">Наименование исследования:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbLir" runat="server" Width="200px">
                         <ValidationSettings ValidationGroup="AddResearchValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxTextBox>
                  </td>
                </tr>
                <tr>
                  <td><div class="table_text">ЛИР:</div></td>
                  <td>
                       <dx:ASPxComboBox ID="cbMachine" runat="server" ValueType="System.String" Width="200px">
                        <ValidationSettings ValidationGroup="AddResearchValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                    </ValidationSettings>
                       </dx:ASPxComboBox>
                  </td>
                </tr>
                <tr>
                  <td><div class="table_text">Наименование ВПО:</div></td>
                  <td>
                       <dx:ASPxComboBox ID="cbMalware" runat="server" ValueType="System.String" Width="200px">
                       <ValidationSettings ValidationGroup="AddResearchValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                    </ValidationSettings>
                       </dx:ASPxComboBox>
                  </td>
                </tr>
                <tr>
                  <td><div class="table_text">Время проведения:</div></td>
                  <td>
                        <dx:ASPxSpinEdit ID="spinTime" runat="server" Height="21px" Number="0" Width="100px" MinValue = "1" MaxValue = "120">
                        </dx:ASPxSpinEdit>
                  </td>  
                </tr>
                
                <tr>
                  <td><div class='table_header'>Дополнительные параметры</div></td>
                  <td><hr /></td>
                </tr>

                <tr>
                  <td><div class="table_text">Скрыть файл:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbHideFile" runat="server" Width="200px">
                       </dx:ASPxTextBox>
                  </td>
                </tr>

                <tr>
                  <td><div class="table_text">Запретить удаление файла:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbLockDelete" runat="server" Width="200px">
                       </dx:ASPxTextBox>
                  </td>
                </tr>

                <tr>
                  <td><div class="table_text">Скрыть ветку реестра:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbHideRegistry" runat="server" Width="200px">
                       </dx:ASPxTextBox>
                  </td>
                </tr>

                <tr>
                  <td><div class="table_text">Скрыть процесс:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbHideProcess" runat="server" Width="200px">
                       </dx:ASPxTextBox>
                  </td>
                </tr>

                <tr>
                  <td><div class="table_text">Установить сигнатуру для сканирования:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbSetSignature" runat="server" Width="200px">
                       </dx:ASPxTextBox>
                  </td>
                </tr>

                <tr>
                  <td><div class="table_text">Установить расширение для отслеживания:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbSetExtension" runat="server" Width="200px">
                       </dx:ASPxTextBox>
                  </td>
                </tr>

                <tr>
                  <td><div class="table_text">Установить полосу пропускания:</div></td>
                  <td>
                       <dx:ASPxTextBox ID="tbSetBandwidth" runat="server" Width="200px" Text="4096">
                           <MaskSettings IncludeLiterals="DecimalSymbol" />
                       </dx:ASPxTextBox>
                  </td>
                </tr>

                  <td>
                  </td>
                  <td>
                      <dx:ASPxButton ID="btnCreate" runat="server" Text="Создать" ValidationGroup="AddResearchValidationGroup" onclick="BtnCreateClick" AutoPostBack = "false">
                      </dx:ASPxButton>
                  </td>
                </tr>
			</tbody>
</table>
             



</asp:Content>
