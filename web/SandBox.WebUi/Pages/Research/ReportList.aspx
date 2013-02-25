<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ReportList.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.ReportList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />

	    <div id="content-top">
		    <div id="pagename">
           <dx:ASPxLabel ID="LHeader" runat="server" Theme="iOS">
           </dx:ASPxLabel></div>
</div>
    <div id="content-main">
       <table class='panel'>
			<tbody>
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text'>
                        <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Назад к списку исследований" NavigateUrl="~/Pages/Research/Current.aspx">
                        </dx:ASPxHyperLink>
                          <br />
                          <table style="width:100%;">
                              <tr>
                                  <td style="width: 200px">
                                      <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Операционная система:">
                                      </dx:ASPxLabel>
                                  </td>
                                  <td>
                                      <dx:ASPxLabel ID="LOS" runat="server">
                                      </dx:ASPxLabel>
                                  </td>
                              </tr>
                              <tr>
                                  <td style="width: 200px">
                                      <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Время начала исследования:">
                                      </dx:ASPxLabel>
                                  </td>
                                  <td>
                                      <dx:ASPxLabel ID="LStartTime" runat="server">
                                      </dx:ASPxLabel>
                                  </td>
                              </tr>
                              <tr>
                                  <td style="width: 200px">
                                      <dx:ASPxLabel ID="ASPxLabel3" runat="server" 
                                          Text="Времени прошло:">
                                      </dx:ASPxLabel>
                                  </td>
                                  <td>
                                      <dx:ASPxLabel ID="LStopTime" runat="server">
                                      </dx:ASPxLabel>
                                  </td>
                              </tr>
                              <tr>
                                  <td style="width: 200px">
                                      <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Времени осталось:">
                                      </dx:ASPxLabel>
                                  </td>
                                  <td>
                                      <dx:ASPxLabel ID="LTimeToWork" runat="server">
                                      </dx:ASPxLabel>
                                  </td>
                              </tr>
                              <tr>
                                  <td colspan="2">
                                      <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Параметры исследования">
                                      </dx:ASPxLabel>
                                  </td>
                              </tr>
                              <tr>
                                  <td colspan="2">
                                      <asp:TreeView ID="TreeView1" runat="server" ImageSet="Arrows" ShowLines="True">
                                          <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                          <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                                              HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                          <ParentNodeStyle Font-Bold="False" />
                                          <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
                                              HorizontalPadding="0px" VerticalPadding="0px" />
                                      </asp:TreeView>
                                  </td>
                              </tr>
                          </table>
                          <br />
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
                        <dx:ASPxHyperLink ID="linkGetTraffic" runat="server" 
                              Text="Получить перехват сетевого траффика" NavigateUrl="javascript:;" 
                              Visible="False" Enabled="False">
                        </dx:ASPxHyperLink>
                        <dx:ASPxButton ID="ASPxButton1" runat="server" 
                              Text="Запросить перехват сетевого трафика" onclick="BtnGetClick" Width="276px">
                    </dx:ASPxButton>
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
                      <dx:ASPxHyperLink ID="HLPorts" runat="server" 
                          NavigateUrl="~/Pages/Research/PortsList.aspx" Text="Получить список портов" />
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
                        <dx:ASPxHyperLink ID="linkGetFileList" runat="server" 
                              Text="Получить образ файловой системы" 
                              NavigateUrl="~/Pages/Research/FileList.aspx" Visible="true">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
                 <tr>
                  <td class='panel-left'>
                      <div class='panel-text-nomargin'>
                        <dx:ASPxHyperLink ID="ASPxHyperLink3" runat="server" Text="Диаграмма событий" 
                              NavigateUrl="~/Pages/Research/ChartOfEvents.aspx" />
                      </div>
                  </td>
                </tr>
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text-nomargin'>
                        <dx:ASPxHyperLink ID="ASPxHyperLink4" runat="server" Text="Распределение событий и классификация" 
                              NavigateUrl="~/Pages/Research/EventsReport.aspx" ViewStateMode="Enabled" />
                      </div>
                  </td>
                </tr>
                <tr>
                  <td class='panel-left'>
                      <dx:ASPxHyperLink ID="ASPxHyperLink5" runat="server" 
                          NavigateUrl="~/Pages/Research/Comparer.aspx" Text="Сравнить ветки реестра" />
                  </td>
                </tr>
			</tbody>
		</table>
        
<div class="page_table"> 
    <asp:UpdatePanel ID="UpdategridPanel" runat="server">
    <ContentTemplate>
    <dx:ASPxLabel ID="LPagingSize" runat="server" Text="Колличество строк на одной странице в таблице">
    </dx:ASPxLabel>
    <dx:ASPxComboBox ID="CBPagingSize" runat="server" ValueType="System.Int32" 
        Height="21px" LoadingPanelText="Загрузка&amp;hellip;" SelectedIndex="2" 
        Width="350px" AutoPostBack="True" 
        onselectedindexchanged="CBPagingSize_SelectedIndexChanged">
        <Items>
            <dx:ListEditItem Text="10" Value="10" />
            <dx:ListEditItem Text="20" Value="20" />
            <dx:ListEditItem Selected="True" Text="30" Value="30" />
            <dx:ListEditItem Text="40" Value="40" />
            <dx:ListEditItem Text="50" Value="50" />
            <dx:ListEditItem Text="60" Value="60" />
            <dx:ListEditItem Text="70" Value="70" />
            <dx:ListEditItem Text="80" Value="80" />
            <dx:ListEditItem Text="90" Value="90" />
            <dx:ListEditItem Text="100" Value="100" />
        </Items>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
    </dx:ASPxComboBox>
    <hr style="width: 100%;" />
    <dx:ASPxGridView ID="gridViewReports" runat="server"  AutoGenerateColumns="False" 
            EnableTheming="True" Theme="Default"  
            KeyFieldName="Id" Width="100%" 
        style="margin-top: 0px; margin-right: 9px;" 
        onhtmlrowprepared="gridViewReports_HtmlRowPrepared" EnableCallBacks="False" >
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="Id"  VisibleIndex="1" Visible=False 
                ReadOnly="True" Name="Id">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>

                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Время события" FieldName="timeofevent" 
                VisibleIndex="3">
                <PropertiesTextEdit>
                    <ValidationSettings ErrorText="Неверное значение">
                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="rschId" VisibleIndex="2" 
                Visible="False">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="pid2" VisibleIndex="6">
                <PropertiesTextEdit>
                    <ValidationSettings ErrorText="Неверное значение">
                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="pid" VisibleIndex="5">
                <PropertiesTextEdit>
                    <ValidationSettings ErrorText="Неверное значение">
                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <Settings HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ModuleId" VisibleIndex="4" Width="50px" 
                Caption="Модуль">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>
                <Settings HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Действие" FieldName="EventCode" 
                VisibleIndex="7" Width="50px">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>
                <Settings HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Описание" FieldName="Description" 
                VisibleIndex="12" Visible="False">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Объект" FieldName="who" VisibleIndex="9" 
                Width="100px">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>
                <Settings HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Цель" FieldName="dest" VisibleIndex="8">
                <PropertiesTextEdit>
                    <ValidationSettings ErrorText="Неверное значение">
                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <Settings HeaderFilterMode="CheckedList" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Дополнительно" FieldName="adddata2" 
                VisibleIndex="11">
                <PropertiesTextEdit>
                    <ValidationSettings ErrorText="Неверное значение">
                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Данные" FieldName="adddata1" 
                VisibleIndex="10">
                <PropertiesTextEdit>
                    <ValidationSettings ErrorText="Неверное значение">
                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsBehavior AllowFocusedRow="True" />
        <SettingsPager PageSize="30">
        </SettingsPager>
        <Settings ShowFilterRow="True" ShowGroupPanel="True" ShowFilterBar="Visible" 
            ShowFilterRowMenu="True" />

<SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
    </dx:ASPxGridView>
    <br />
    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" 
        LoadingPanelText="Загрузка&amp;hellip;" SelectedIndex="0" Width="200px">
        <Items>
            <dx:ListEditItem Selected="True" Text="Критически важное" Value="0" />
            <dx:ListEditItem Text="Важное" Value="1" />
        </Items>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
    </dx:ASPxComboBox>
    <dx:ASPxButton ID="ASPxButton2" runat="server" onclick="ASPxButton2_Click" 
        Text="Добавить в справочник выбранное событие" Width="200px">
    </dx:ASPxButton>
    </ContentTemplate>
    </asp:UpdatePanel>
&nbsp;</div>
</div>

</asp:Content>
