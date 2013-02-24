<%@ Page Title="" Language="C#" MasterPageFile="~/Light.master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SandBox.WebUi.Pages.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../Content/PageView.css"  />

       <div id='page_header'>
          Информация по ресурсам
       </div>

       <br />

		<table class='panel'>
			<tbody>		
                <tr>
					<td class='panel-left'>
						<div class='panel-label'>ЛИР:</div>
                               <div class='panel-text'>
                                   <dx:ASPxLabel ID="labelVmInfo" runat="server" Text="Обновление информации..." Font-Size="10" Font-Names = "Arial">
                                   </dx:ASPxLabel>
                               </div>
					</td>

					<td class='panel-center'>
					</td>
					<td class='panel-right'>
						<div class='panel-label'>ВЛИР:</div>
						<div class='panel-text'>Загрузка ЦП - 75%. Свободно оперативной памяти - 543 Мб.</div>
					</td>
				</tr>

                <tr>
                 <td class='panel-left'>
                   <br />
                 </td>
                </tr>
                
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text'>
                        <dx:ASPxHyperLink ID="linkRegisterNewVm" runat="server" Text="История проведенных исследований" NavigateUrl="~/Pages/Information/CreateEtalonMachine.aspx">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
                
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text'>
                        <dx:ASPxHyperLink ID="linkCreateNewVm" runat="server" Text="Постановка задачи на проведение исследования" NavigateUrl="~/Pages/Information/CreateMachine.aspx" Visible="true">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>

			</tbody>
		</table>

        <br />

        <div class='tbl_header'>
          Текущие сессии
       </div>


       <div class="Header">
        <div>
            <div>
               <dx:ASPxGridView ID="gridViewMachines" runat="server" 
        AutoGenerateColumns="False" EnableTheming="True" Theme="DevEx" Width="100%">
        <Columns>
            <dx:GridViewDataTextColumn Caption="Id" FieldName="Id" VisibleIndex="0" Visible=false>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Имя" FieldName="Name" VisibleIndex="1" Width="100px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Тип" FieldName="Type" VisibleIndex="2" Width="50px">
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataTextColumn Caption="Система" FieldName="System" VisibleIndex="3" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Состояние" FieldName="State" VisibleIndex="4" Width="100px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Тип среды" FieldName="EnvType" VisibleIndex="5" Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Состояние среды" FieldName="EnvState" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ip-адрес" FieldName="EnvIp" VisibleIndex="7" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mac-адрес" FieldName="EnvMac" VisibleIndex="8" Width="180px">
            </dx:GridViewDataTextColumn>
            
            
            <dx:GridViewDataColumn Caption="" VisibleIndex="9">
                <DataItemTemplate>
                    <a href="javascript:;">запустить</a>
                    <a href="javascript:;">остановить</a>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn Caption="" VisibleIndex="10">
                <DataItemTemplate>
                    <a href="javascript:;">удалить</a>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
    </dx:ASPxGridView>
            </div>
        </div>
       </div>


</asp:Content>
