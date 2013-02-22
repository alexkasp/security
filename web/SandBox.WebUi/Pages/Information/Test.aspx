<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="SandBox.WebUi.Pages.Information.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />
    <script type="text/javascript">
    // <![CDATA[
        var timeout;
        function scheduleGridUpdate(grid) {
            window.clearTimeout(timeout);
            timeout = window.setTimeout(
                function () { grid.Refresh(); },
                3000
            );
        }
        function grid_Init(s, e) {
            scheduleGridUpdate(s);
        }
        function grid_BeginCallback(s, e) {
            window.clearTimeout(timeout);
        }
        function grid_EndCallback(s, e) {
            scheduleGridUpdate(s);
        }

        function OnDeleteClick(element, key) {
            popup_delete.ShowAtElement(element);
            callbackPanel_delete.PerformCallback(key);
        }

        function OnAcceptDelete() {
            popup_delete.Hide();
        }

        function OnStartClick(key) {
            PageMethods.StartMachine(key);
        }

        function OnStopClick(key) {
            PageMethods.StopMachine(key);
        }
    // ]]>
    </script>

    <div id='page_header'>
          Информация по ресурсам
       </div>
		<table class='panel'>
			<tbody>
				
                <tr>
					<td class='panel-left'>
						<div class='panel-label'>ЛИР:</div>
						<asp:UpdatePanel ID="UpdatePanelVmInfo" runat="server"  UpdateMode="Always">
                             <ContentTemplate>
                               <div class='panel-text'>
                                   <dx:ASPxLabel ID="labelVmInfo" runat="server" Text="Обновление информации...">
                                   </dx:ASPxLabel>
                               </div>
                             </ContentTemplate>
                         </asp:UpdatePanel>
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
                        <dx:ASPxHyperLink ID="linkRegisterNewVm" runat="server" Text="Добавить ЛИР" NavigateUrl="~/Pages/Information/CreateEtalonMachine.aspx">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
                
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text'>
                        <dx:ASPxHyperLink ID="linkCreateNewVm" runat="server" Text="Создать ВЛИР" NavigateUrl="~/Pages/Information/CreateMachine.aspx" Visible="true">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>

			</tbody>
		</table>

    <dx:ASPxGridView ID="gridViewMachines" runat="server" 
        AutoGenerateColumns="False" EnableTheming="True" Theme="DevEx" Width="900px">
        <ClientSideEvents Init="grid_Init" BeginCallback="grid_BeginCallback" EndCallback="grid_EndCallback" />
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
                    <a href="javascript:;" onclick="OnStartClick('<%# Container.KeyValue %>')">запустить</a>
                    <a href="javascript:;" onclick="OnStopClick('<%# Container.KeyValue %>')">остановить</a>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn Caption="" VisibleIndex="10">
                <DataItemTemplate>
                    <a href="javascript:;" onclick="OnDeleteClick(this, '<%# Container.KeyValue %>')">удалить</a>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
        <SettingsLoadingPanel Mode="Disabled"/>
    </dx:ASPxGridView>

    <asp:UpdatePanel ID="PopupUpdatePanel" runat="server"  UpdateMode="Conditional">
                   <ContentTemplate>                     

                    <dx:ASPxPopupControl ID="popup_delete" ClientInstanceName="popup_delete" runat="server" AllowDragging="false" AllowResize="false" ShowCloseButton="false"
                            PopupHorizontalAlign="OutsideRight" HeaderText="Удаление ВЛИР:">
                            <ContentCollection>
                               <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                    <dx:ASPxCallbackPanel ID="callbackPanel_delete" ClientInstanceName="callbackPanel_delete" runat="server"
                                        Width="250px" Height="50px" OnCallback="CallbackPanelDeleteCallback" RenderMode="Table">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent1" runat="server">              
                                                <asp:Literal ID="deleteText" runat="server" Text=""></asp:Literal>
                                                <dx:ASPxButton ID="btnDelete" runat="server" Text="удалить" 
                                                    AutoPostBack="False" OnClick="BtnDeleteClick">
                                                    <ClientSideEvents Click="OnAcceptDelete" />
                                                </dx:ASPxButton>
                                           </dx:PanelContent>
                                        </PanelCollection>
                                    </dx:ASPxCallbackPanel>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>
                   </ContentTemplate>
                 </asp:UpdatePanel>

</asp:Content>
