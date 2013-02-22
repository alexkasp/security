<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Resources.aspx.cs" Inherits="SandBox.WebUi.Pages.Information.Resources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />
<script type="text/javascript">
    // <![CDATA[
        var timeout;
        function scheduleGridUpdate(grid) {
            window.clearTimeout(timeout);
            timeout = window.setTimeout( 
                function() { grid.Refresh(); },
                5000
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
    // ]]>
</script> 

       
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
                                   <%=LirInfo() %>>
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
                        <dx:ASPxHyperLink ID="linkRegisterNewVm" ImageUrl ="../../Content/Images/Icons/add_lir.png" runat="server" Text="Добавить ЛИР" NavigateUrl="~/Pages/Information/CreateEtalonMachine.aspx">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
                
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text'>
                        <dx:ASPxHyperLink ID="linkCreateNewVm"  ImageUrl ="../../Content/Images/Icons/add_vlir.png" runat="server" Text="Создать ВЛИР" NavigateUrl="~/Pages/Information/CreateMachine.aspx" Visible="true">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>

			</tbody>
		</table>

    <div class="Header">
        <div>
            <div>

            <div class="page_table">
                <dx:ASPxLabel ID="labelNoItems" runat="server" Text="">
                </dx:ASPxLabel>
            </div>

            <div class='tbl_header'>
          Ресурсы
       </div>

            <dx:ASPxGridView ID="gridViewMachines" runat="server" 
        AutoGenerateColumns="False" EnableTheming="True" Theme="DevEx" Width="100%" 
                    onhtmlrowprepared="GridViewMachinesHtmlRowPrepared"
                    oncustombuttoncallback="GridViewMachinesCustomButtonCallback" KeyFieldName="Id">
        <ClientSideEvents Init="grid_Init" BeginCallback="grid_BeginCallback" EndCallback="grid_EndCallback" />
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width = "50px">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="runButton">
                        <Image ToolTip="Запустить" Url="../../Content/Images/Icons/run.png" />
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="stopButton">
                        <Image ToolTip="Остановить" Url="../../Content/Images/Icons/stop.png" />
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="Id" FieldName="Id" VisibleIndex="0" Visible=false>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Имя" FieldName="Name" VisibleIndex="1" Width="100px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Тип" FieldName="Type" VisibleIndex="2" Width="50px">
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataTextColumn Caption="Система" FieldName="System" VisibleIndex="3" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Состояние" FieldName="State" VisibleIndex="4" Width="200px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Тип среды" FieldName="EnvType" VisibleIndex="5" Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Состояние среды" FieldName="EnvState" VisibleIndex="6" Width="100px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ip-адрес" FieldName="EnvIp" VisibleIndex="7" Width="150px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mac-адрес" FieldName="EnvMac" VisibleIndex="8" Width="200px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn VisibleIndex="9" ButtonType="Image">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="editButton">
                        <Image ToolTip="Редактировать" Url="../../Content/Images/Icons/edit.png" />
                    </dx:GridViewCommandColumnCustomButton>
                    <dx:GridViewCommandColumnCustomButton ID="deleteButton">
                        <Image ToolTip="Удалить" Url="../../Content/Images/Icons/delete.png" />
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
         <SettingsLoadingPanel Mode="Disabled" />
    </dx:ASPxGridView>

            </div>
        </div>
    </div>
</asp:Content>
