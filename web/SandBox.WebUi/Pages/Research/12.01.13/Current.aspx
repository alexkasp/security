<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Current.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.Current" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />

<script type="text/javascript">
    function OnDeleteClick(element, key) {
        popup_delete.ShowAtElement(element);
        callbackPanel_delete.PerformCallback(key);
    }

    function OnAcceptDelete() {
        popup_delete.Hide();
    }

    function OnStartClick(key) {
        PageMethods.StartResearch(key);
    }

    function OnStopClick(key) {
        PageMethods.StopResearch(key);
    }

    function OnGetReportClick(key) {
        PageMethods.GetReport(key);
    }
</script>

             <asp:UpdatePanel ID="PopupUpdatePanel" runat="server"  UpdateMode="Conditional">
                   <ContentTemplate>                     

                    <dx:ASPxPopupControl ID="popup_delete" ClientInstanceName="popup_delete" runat="server" AllowDragging="false" AllowResize="false" ShowCloseButton="false"
                            PopupHorizontalAlign="OutsideRight" HeaderText="Удаление исследования:">
                            <ContentCollection>
                               <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                    <dx:ASPxCallbackPanel ID="callbackPanel_delete" ClientInstanceName="callbackPanel_delete" runat="server"
                                        Width="250px" Height="50px" OnCallback="CallbackPanelDeleteCallback" RenderMode="Table">
                                        <PanelCollection>
                                            <dx:PanelContent ID="PanelContent1" runat="server">              
                                                <asp:Literal ID="deleteText" runat="server" Text=""></asp:Literal>
                                                <dx:ASPxButton ID="btnDelete" runat="server" Text="удалить" AutoPostBack="False" OnClick="BtnDeleteClick">
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
       
       <div id='page_header'>
          Текущие исследования
       </div>

       <table class='panel'>
			<tbody>
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text'>
                        <dx:ASPxHyperLink ID="linkAddNewResearch" runat="server" Text="Создать новое исследование" NavigateUrl="~/Pages/Research/NewResearch.aspx">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
			</tbody>
        </table>

        <div class="page_table">           
            <asp:UpdatePanel ID="UpdatePanelResearches" runat="server"  UpdateMode="Conditional">
            <ContentTemplate>

                <div class="page_table">
                        <dx:ASPxLabel ID="labelNoItems" runat="server" Text="">
                        </dx:ASPxLabel>
                </div>

                <dx:ASPxGridView ID="gridViewResearches" runat="server" 
                    AutoGenerateColumns="False" EnableTheming="True" Theme="DevEx" 
                    Width="900px" onhtmlrowcreated="GridViewResearchesHtmlRowCreated" onhtmlrowprepared="GridViewResearchesHtmlRowPrepared">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Id" FieldName="Id" VisibleIndex="0" Visible=false>
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Название сессии" FieldName="ResearchName" 
                            VisibleIndex="1" Width="100px">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Дата создания" FieldName="CreatedDate" 
                            VisibleIndex="2" Width="100px">  
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Состояние" FieldName="State" 
                            VisibleIndex="3" Width="100px">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Наименование ВПО" FieldName="Malware" 
                            VisibleIndex="4" Width="100px">                
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Тип ЛИР" FieldName="VmType" 
                            VisibleIndex="5" Width="50px">                
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ОС" FieldName="VmSystem" VisibleIndex="6" 
                            Width="100px">                
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Времени прошло" FieldName="TimeElapsed" 
                            VisibleIndex="7" Width="100px">                
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn> 
                        <dx:GridViewDataTextColumn Caption="Времени осталось" FieldName="TimeLeft" 
                            VisibleIndex="8" Width="100px">                
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                        </dx:GridViewDataTextColumn>    
                        <dx:GridViewDataColumn Caption="" VisibleIndex="9">
                <DataItemTemplate>
                    <a href="javascript:;" onclick="OnStartClick('<%# Container.KeyValue %>')">запустить</a>
                    <%--<a href="javascript:;" onclick="OnStopClick('<%# Container.KeyValue %>')">остановить</a>--%>
                    <a id="stopA" runat="server" onclick="OnStopClick('<%# Container.KeyValue %>')">остановить</a>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn Caption="" VisibleIndex="10">
                <DataItemTemplate>
                <dx:ASPxHyperLink ID="linkA" runat="server" Text="Отчет" NavigateUrl="javascript;">
                </dx:ASPxHyperLink>
              </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewDataColumn Caption="" VisibleIndex="11">
                <DataItemTemplate>
                    <a href="javascript:;" onclick="OnDeleteClick(this, '<%# Container.KeyValue %>')">удалить</a>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
                    <Settings GridLines="Horizontal" />
                    <SettingsLoadingPanel Text="Загрузка&amp;hellip;" />
    </dx:ASPxGridView>

                <asp:Timer ID="Update_timer" runat="server" Interval="1000"  ontick="UpdateTimerTick">
                </asp:Timer>
            </ContentTemplate>
            <Triggers>
               <asp:AsyncPostBackTrigger ControlID="gridViewResearches" EventName="DataBinding" />
            </Triggers>
            </asp:UpdatePanel>
        </div>


</asp:Content>
