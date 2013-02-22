<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ReportList.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.ReportList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />

       <div id='page_header'>
          Отчет о исследовании №<%=Rs.Id%> (<%=Rs.ResearchName%>)
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
                        <dx:ASPxHyperLink ID="linkGetProcessList" runat="server" Text="Получить список процессов" NavigateUrl="javascript:;" Visible="true">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>

                <tr>
                  <td class='panel-left'>
                      <div class='panel-text-nomargin'>
                        <dx:ASPxHyperLink ID="linkGetRegistryList" runat="server" Text="Получить образ реестра" NavigateUrl="javascript:;" Visible="true">
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

			</tbody>
		</table>

        <br />
        
<div class="page_table"> 
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
  </div>

</asp:Content>
