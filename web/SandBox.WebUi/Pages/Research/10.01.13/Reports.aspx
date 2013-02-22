<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />

<script type="text/javascript">
    function OnGetTrafficClick() {
        PageMethods.GetTraffic();
    }

    function OnGetProcessesClick() {
        PageMethods.GetProcesses();
    }
</script>

       <div id='page_header'>
          Отчеты
       </div>

       <table class='form'>
			<tbody>
                <tr>
                  <td><div class="simple_text">Выберите исследование для просмотра отчета:</div></td>
                  <td>
                       <dx:ASPxComboBox ID="cbResearch" runat="server" Width="200px">
                       <ValidationSettings ValidationGroup="CreateEtalonMachineValidationGroup">
	                      <RequiredField ErrorText=" " IsRequired="true" />
	                     </ValidationSettings>
                       </dx:ASPxComboBox>
                  </td>
                </tr>
                <tr>
                  <td></td>
                  <td>
                      <dx:ASPxButton ID="btnCreate" runat="server" Text="Получить" ValidationGroup="CreateEtalonMachineValidationGroup" onclick="BtnGetClick" AutoPostBack="false">
                      </dx:ASPxButton>
                  </td>
                </tr>
			</tbody>
</table>

<div class="page_table">           

    <div class="page_table">
            <dx:ASPxLabel ID="labelNoItems" runat="server" Text="">
            </dx:ASPxLabel>
    </div>

    <asp:UpdatePanel ID="UpdatePanelReports" runat="server"  UpdateMode="Conditional">
    <ContentTemplate>

    <table class='panel'>
			<tbody>

                <tr>
                  <td class='panel-left'>
                      <div class='panel-text-nomargin'>
                        <dx:ASPxHyperLink ID="linkGetTraffic" runat="server" Text="Получить перехват сетевого траффика" NavigateUrl="javascript:;" Visible="true">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
                
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text-nomargin'>
                        <dx:ASPxHyperLink ID="linkGetProcessList" runat="server" 
                              Text="Получить список процессов" 
                              NavigateUrl="~/Pages/Research/ProcessList.aspx" Visible="true">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>

                <tr>
                  <td class='panel-left'>
                      <div class='panel-text-nomargin'>
                        <dx:ASPxHyperLink ID="linkGetRegistryList" runat="server" 
                              Text="Получить образ реестра" NavigateUrl="~/Pages/Research/RegistryList.aspx" 
                              Visible="true">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>

                <tr>
                  <td class='panel-left'>
                      <div class='panel-text-nomargin'>
                        <dx:ASPxHyperLink ID="linkGetFileList" runat="server" Text="Получить образ файловой системы" NavigateUrl="javascript:;" Visible="true">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
                
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text-nomargin'>
                        <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Монитор событий" 
                              NavigateUrl="~/Pages/Research/SetMonitor.aspx" />
                      </div>
                  </td>
                </tr>

			</tbody>
		</table>

        
        <br />

        <br />

    <dx:ASPxGridView ID="gridViewReports" runat="server"  AutoGenerateColumns="False" 
            EnableTheming="True" Theme="DevEx"  
            KeyFieldName="Id" Width="800px" >
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Id"  VisibleIndex="0" Visible=False 
                ReadOnly="True">
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ResearchId" VisibleIndex="2" 
                Visible="False">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ModuleId" VisibleIndex="3" Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Действие" FieldName="ActionId" 
                VisibleIndex="4" Width="50px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Объект" FieldName="Object" VisibleIndex="5" 
                Width="100px">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Цель" FieldName="Target" VisibleIndex="6">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Время события" FieldName="TIme" 
                VisibleIndex="1" Width="120px" CellStyle-Wrap="True">
                <PropertiesDateEdit DisplayFormatString="{0:G}">     
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
        </Columns>
        <Settings ShowFilterRow="True" />
    </dx:ASPxGridView>
    </ContentTemplate>
    <Triggers>
         <asp:AsyncPostBackTrigger ControlID="gridViewReports" EventName="DataBinding" />
    </Triggers>
</asp:UpdatePanel>
        </div>
    
    <asp:LinqDataSource ID="ReportLinqDataSource" runat="server" 
        ContextTypeName="SandBox.Db.SandBoxDataContext" EntityTypeName="" 
        TableName="Rpts">
    </asp:LinqDataSource>

</asp:Content>
