<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="True"
    CodeBehind="Current.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.Current" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Content/PageView.css" />
    <script type="text/javascript">

        function TestStartEvent(value) {
            OnStartClick(value);
        }

        function TestStopEvent(value) {
            PageMethods.StopResearch(value);
        }

        function OnDeleteClick(element, key) {
            if (confirm("Вы уверены, что хотите удалить исследование?")) {
                PageMethods.WebDelResearch(key);
                //           popup_delete.ShowAtElement(element);
                //           callbackPanel_delete.PerformCallback(key);
            }
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
    <asp:UpdatePanel ID="PopupUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <dx:ASPxPopupControl ID="popup_delete" ClientInstanceName="popup_delete" runat="server"
                AllowDragging="false" AllowResize="false" ShowCloseButton="false" PopupHorizontalAlign="OutsideRight"
                HeaderText="Удаление исследования:">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        <dx:ASPxCallbackPanel ID="callbackPanel_delete" ClientInstanceName="callbackPanel_delete"
                            runat="server" Width="250px" Height="50px" OnCallback="CallbackPanelDeleteCallback"
                            RenderMode="Table">
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1" runat="server">
                                    <asp:Literal ID="deleteText" runat="server" Text=""></asp:Literal>
                                    <dx:ASPxButton ID="btnDelete" runat="server" Text="удалить" AutoPostBack="False"
                                        OnClick="BtnDeleteClick">
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
	    <div id="content-top">
		    <div id="pagename">Исследования</div>
		    <div id="toolbuttons">
            <table>
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnNew" AutoPostBack="False" runat="server" 
                            Text="Создать исследование" onclick="btnNew_Click"></dx:ASPxButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnComp" AutoPostBack="False" runat="server" Text="Сравнить исследование"></dx:ASPxButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnDel" AutoPostBack="False" runat="server" 
                            Text="Удалить исследование">
                            <ClientSideEvents Click="function(s, e) {
	if (confirm ('Удалить выбранные исследования?')) {
    gridViewResearches.PerformCallback('DeleteSelected');}
}" />
                        </dx:ASPxButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            </div>
		    <div id="tablenavbuttons"><img src="../../Content/Images/btn_tablenav.jpg" /></div>
		</div>
    <div id="content-main">
        <asp:UpdatePanel ID="UpdatePanelResearches" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="page_table">
                    <dx:ASPxLabel ID="labelNoItems" runat="server" Text="">
                    </dx:ASPxLabel>
                </div>
                <dx:ASPxGridView ID="gridViewResearches" runat="server" KeyFieldName="Id" AutoGenerateColumns="False"
                    EnableTheming="True" Width="100%" OnHtmlRowCreated="GridViewResearchesHtmlRowCreated"
                    OnHtmlRowPrepared="GridViewResearchesHtmlRowPrepared" 
                    ClientInstanceName="gridViewResearches" 
                    Theme="SandboxTheme" oncustomcallback="gridViewResearches_CustomCallback" 
                    ondatabinding="gridViewResearches_DataSelect" >
                    <ClientSideEvents RowClick="function(s, e) {
	s.ExpandDetailRow(e.visibleIndex);
}" />
                    <Columns>
                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" 
                            Width="20px">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="№" FieldName="Id" VisibleIndex="1"
                            Name="Id" UnboundType="Integer" Width="50px" SortIndex="0" 
                            SortOrder="Descending">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <Settings AllowHeaderFilter="False" />
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Название" FieldName="ResearchName" 
                            VisibleIndex="2">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <Settings AllowHeaderFilter="False" />
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Дата создания" FieldName="CreatedDate" 
                            VisibleIndex="3" UnboundType="DateTime" Width="130px">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <Settings AllowHeaderFilter="False" />
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Статус" FieldName="State" VisibleIndex="4" 
                            Width="80px">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <Settings HeaderFilterMode="CheckedList" />
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ВПО" FieldName="Malware" VisibleIndex="6">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <Settings HeaderFilterMode="CheckedList" />
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Тип" FieldName="VmType" VisibleIndex="7" 
                            Width="160px">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <Settings HeaderFilterMode="CheckedList" />
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="ОС" FieldName="VmSystem" VisibleIndex="8" 
                            Width="170px">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <Settings HeaderFilterMode="CheckedList" />
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Длительность" FieldName="TimeElapsed" 
                            VisibleIndex="5" Width="120px">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <Settings AllowHeaderFilter="False" />
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Файл. система" FieldName="fsEventsCount" 
                            VisibleIndex="9" Width="120px">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Реестр" VisibleIndex="10" 
                            FieldName="regEventsCount" Width="70px">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Сеть" FieldName="netEventsCount" 
                            VisibleIndex="11" Width="50px">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Процессы" FieldName="procEventsCount" 
                            VisibleIndex="12" Width="80px">
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Времени осталось" FieldName="TimeLeft" 
                            ShowInCustomizationForm="True" Visible="False" VisibleIndex="13">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <Settings AllowHeaderFilter="False" />
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Монитор" ShowInCustomizationForm="True" 
                            Visible="False" VisibleIndex="15">
                            <PropertiesTextEdit>
                                <ValidationSettings ErrorText="Неверное значение">
                                    <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                </ValidationSettings>
                            </PropertiesTextEdit>
                            <DataItemTemplate>
                                <dx:ASPxHyperLink ID="HLCurrentReport" runat="server" 
                                    Text="Текущее состояние исследования" />
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn Caption="" VisibleIndex="14" Visible="False">
                            <DataItemTemplate>
                                <dx:ASPxButton ID="BStart" runat="server" Text="запустить" EnableViewState="false"
                                    ClientSideEvents-Click='<%# "function(s,e){TestStartEvent(" + Container.KeyValue + ");}" %>'>
                                </dx:ASPxButton>
                                <dx:ASPxButton ID="BStop" runat="server" Text="остановить" EnableViewState="false"
                                    ClientSideEvents-Click='<%# "function(s,e){TestStopEvent(" + Container.KeyValue + ");}" %>'>
                                </dx:ASPxButton>
                                <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="" Visible="false">
                                    <%--<a href="javascript:;" onclick="OnStartClick('<%# Container.KeyValue %>')">запустить</a>--%>
                                </dx:ASPxHyperLink>
                                <%--<a href="javascript:;" onclick="OnStopClick('<%# Container.KeyValue %>')">остановить</a>--%>
                                <%-- <a id="stopA" runat="server" onclick="OnStopClick('<%# Container.KeyValue %>')">остановить</a>--%>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="" VisibleIndex="16" Visible="False">
                            <DataItemTemplate>
                                <dx:ASPxHyperLink ID="linkA" runat="server" Text="Отчет" NavigateUrl="javascript;">
                                </dx:ASPxHyperLink>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn Caption="" VisibleIndex="17" Visible="False">
                            <DataItemTemplate>
                                <a href="javascript:;" onclick="OnDeleteClick(this, '<%# Container.KeyValue %>')">удалить</a>
                            </DataItemTemplate>
                            <CellStyle HorizontalAlign="Left">
                            </CellStyle>
                        </dx:GridViewDataColumn>
                    </Columns>
                    <SettingsBehavior 
                        ColumnResizeMode="NextColumn" ConfirmDelete="True" 
                        EnableCustomizationWindow="True" EnableRowHotTrack="True" 
                        HeaderFilterMaxRowCount="10" />
                    <SettingsPager>
                        <PageSizeItemSettings Items="10, 20, 50, 100" ShowAllItem="True">
                        </PageSizeItemSettings>
                    </SettingsPager>
                    <Settings GridLines="Horizontal" ShowHeaderFilterButton="True" 
                        EnableFilterControlPopupMenuScrolling="True" ShowFilterBar="Visible" 
                        ShowHeaderFilterBlankItems="False" />
                    <SettingsLoadingPanel Text="Загрузка&amp;hellip;" Mode="Disabled" />
                    <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" 
                        ShowDetailButtons="False" />
                    <Styles>
                        <Cell HorizontalAlign="Left">
                        </Cell>
                    </Styles>
                    <Templates>
                        <DetailRow>
                   		<div class="detailrow">
			                <div id="detailrowheader">
				                <div class="detailrowparams" >ДОПЛНИТЕЛЬНЫЕ ПАРАМЕТРЫ ИССЛЕДОВАНИЯ</div>
				                <div class="detailrowevents" >РАСПРЕДЕЛЕНИЕ СОБЫТИЙ</div>
				                <div class="detailrowreport" >

                               <%-- <a href="." runat="server" id ="zhopa">
                                    <img src="../../Content/images/btn_report.png" alt="Подробный отчет" border="0" title="Подробный отчет" runat=server id="reportImg" />
                                </a>--%>
                                    <asp:HyperLink ID="HyperLink1" runat="server" 
                                        ImageUrl="~/Content/Images/btn_report.png" 
                                        NavigateUrl="~/Pages/Research/ReportList.aspx">HyperLink</asp:HyperLink>
                                </div>
			                </div>
			                <div id="detailrowcontent">
				                <div class="detailrowparams" >
                                    <dx:ASPxGridView ID="detailGrid"  runat="server" 
                                        OnBeforePerformDataSelect="Detail_DataSelect" AutoGenerateColumns="False" 
                                        Width="100%">     
                                        <ClientSideEvents Init="function(s, e) {
 drawHalfPie('chartHolder1',Array(2,3));    
 drawHalfPie('chartHolder2',Array(45,27));    
 drawHalfPie('chartHolder3',Array(52,15));    
 drawHalfPie('chartHolder4',Array(22,22)); 
}" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="ModuleX" VisibleIndex="0" 
                                                Caption="Module X" Width="100px">
                                                <CellStyle Font-Bold="True" Font-Underline="True">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="TypeX" VisibleIndex="2" Width="200px">
                                                <EditCellStyle Font-Bold="True">
                                                </EditCellStyle>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ValueX" VisibleIndex="3">
                                                <PropertiesTextEdit>
                                                    <ValidationSettings ErrorText="Неверное значение">
                                                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <PropertiesTextEdit>
                                                    <ValidationSettings ErrorText="Неверное значение">
                                                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                                                    </ValidationSettings>
                                                </PropertiesTextEdit>
                                                <CellStyle Font-Bold="True">
                                                </CellStyle>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <SettingsPager Visible="False">
                                        </SettingsPager>
                                        <Settings GridLines="Horizontal" ShowColumnHeaders="False" 
                                            ShowHeaderFilterBlankItems="False" />
                                        <SettingsLoadingPanel Text="Загрузка&amp;hellip;" />                                
                                        <Styles>
                                            <Table BackColor="White">
                                            </Table>
                                            <Cell BackColor="White">
                                                <BorderLeft BorderWidth="0px" />
                                                <BorderTop BorderWidth="0px" />
                                                <BorderRight BorderWidth="0px" />
                                                <BorderBottom BorderColor="#D8D7D7" BorderStyle="Solid" BorderWidth="1px" />
                                            </Cell>
                                        </Styles>
                                        <Border BorderStyle="None" />
                                        <BorderBottom BorderColor="#D8D7D7" BorderStyle="Solid" BorderWidth="1px" />
                                    </dx:ASPxGridView>
                                </div>
				                <div class="detailrowevents" >
					                <img src="../../Content/images/bluebox.png" />&nbsp;&nbsp;ВАЖНЫЕ&nbsp;&nbsp;&nbsp;&nbsp;<img src="../../Content/images/redbox.png" />&nbsp;&nbsp;ОЧЕНЬ ВАЖНЫЕ
                                    <span id="DetailCharts" runat="server">
					                <table border="0" cellspacing="0" cellpadding="0">
					                  <tr>
						                <td>&nbsp;</td>
						                <td width="150">&nbsp;</td>
						                <td width="150">&nbsp;</td>
						                <td width="150">&nbsp;</td>
					                  </tr>
					                  <tr align="center">
						                <td>
							                <div class="chartHolder"><div id="chartHolder1"></div></div>						
						                </td>
						                <td>
							                <div class="chartHolder"><div id="chartHolder2"></div></div>						
						                </td>
						                <td>
							                <div class="chartHolder"><div id="chartHolder3"></div></div>						
						                </td>
						                <td>
							                <div class="chartHolder"><div id="chartHolder4"></div></div>							
						                </td>
					                  </tr>
					                  <tr align="center">
						                <td height="30">ФАЙЛОВАЯ СИСТЕМА</td>
						                <td height="30">РЕЕСТР</td>
						                <td height="30">СЕТЬ</td>
						                <td height="30">ПРОЦЕССЫ</td>
					                  </tr>
				                  </table>
                                  </span>
				                </div>
			                </div>
		                </div>
                        </DetailRow>
                    </Templates>
                </dx:ASPxGridView>
                <asp:Timer ID="Update_timer" runat="server" 
                    OnTick="UpdateTimerTick">
                </asp:Timer>
                &nbsp;
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="gridViewResearches" EventName="DataBinding" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
