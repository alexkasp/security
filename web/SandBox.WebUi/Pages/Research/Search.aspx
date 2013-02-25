
        
<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.WebForm1" %>

<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Data.Linq" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	    <div id="content-top">
		    <div id="pagename">Поиск</div>
        </div>




<div id="content-main">
    <table class='panel'>
        <tbody>
            <tr>
                <td class='panel-left'>
                    <div class='panel-text'>
                        <cc1:LinqServerModeDataSource ID="LinqServerModeDataSource1" runat="server" 
                            ContextTypeName="SandBox.Db.SandBoxDataContext" TableName="EventsTableViews" />
                        <br />
                        <table style="width:100%;">
                            <tr>
                                <td style="width: 362px">
                                    <dx:ASPxTextBox ID="SearchTextBox" runat="server" Height="18px" Width="345px">
                                        <ClientSideEvents KeyPress="function(s, e) {
            if (e.htmlEvent.keyCode == 13) {
                ASPxClientUtils.PreventEventAndBubble(e.htmlEvent);
                SearchButton.DoClick();
            }
}" />
                                    </dx:ASPxTextBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="SearchButton" runat="server" AutoPostBack="False" 
                                        Height="16px" Text="Найти" Width="60px" 
                                        ClientInstanceName="SearchButton">
                                        <ClientSideEvents Click="function(s, e) {
	gridSearchView.PerformCallback('ApplyFilter');
}" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
<script type="text/javascript">
    $(document).ready(function () {

        $('#extfilterbtn').toggle(function () {

            $('#extfilter').slideDown();
        }, function () {

            $('#extfilter').slideUp();
        });

    })
</script>
                        <div id="extfilterbtn"><a href="#">Расширенный фильтр</a></div><br /><div id="extfilter" style="display:none; padding-bottom:30px;">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                            <dx:ASPxFilterControl ID="filter" runat="server" ClientInstanceName="filter" 
                                    EnableCallBacks="False">
                                <Columns>
                                    <dx:FilterControlSpinEditColumn ColumnType="Integer" DisplayName="№" 
                                        PropertyName="Id">
                                        <PropertiesSpinEdit DisplayFormatString="g">
                                        </PropertiesSpinEdit>
                                    </dx:FilterControlSpinEditColumn>
                                    <dx:FilterControlDateColumn ColumnType="DateTime" DisplayName="Время события" 
                                        PropertyName="timeofevent">
                                        <PropertiesDateEdit EditFormat="DateTime">
                                        </PropertiesDateEdit>
                                    </dx:FilterControlDateColumn>
                                    <dx:FilterControlComboBoxColumn ColumnType="String" DisplayName="Модуль" 
                                        PropertyName="ModuleId">
                                        <PropertiesComboBox>
                                            <Items>
                                                <dx:ListEditItem Selected="True" Text="Файловая система" 
                                                    Value="Файловая система" />
                                                <dx:ListEditItem Text="Реестр" Value="Реестр" />
                                                <dx:ListEditItem Text="Процессы" Value="Процессы" />
                                                <dx:ListEditItem Text="NDISMON" Value="NDISMON" />
                                                <dx:ListEditItem Text="TDIMON" Value="TDIMON" />
                                            </Items>
                                        </PropertiesComboBox>
                                    </dx:FilterControlComboBoxColumn>
                                    <dx:FilterControlSpinEditColumn ColumnType="Integer" DisplayName="PID" 
                                        PropertyName="pid1">
                                        <PropertiesSpinEdit DisplayFormatString="g">
                                        </PropertiesSpinEdit>
                                    </dx:FilterControlSpinEditColumn>
                                    <dx:FilterControlSpinEditColumn ColumnType="Integer" DisplayName="PID2" 
                                        PropertyName="pid2">
                                        <PropertiesSpinEdit DisplayFormatString="g">
                                        </PropertiesSpinEdit>
                                    </dx:FilterControlSpinEditColumn>
                                    <dx:FilterControlTextColumn DisplayName="Событие" PropertyName="EventCode" 
                                        ColumnType="String">
                                    </dx:FilterControlTextColumn>
                                    <dx:FilterControlTextColumn ColumnType="String" DisplayName="Цель" 
                                        PropertyName="who">
                                    </dx:FilterControlTextColumn>
                                    <dx:FilterControlTextColumn ColumnType="String" DisplayName="Объект" 
                                        PropertyName="dest">
                                    </dx:FilterControlTextColumn>
                                    <dx:FilterControlTextColumn ColumnType="String" DisplayName="Данные" 
                                        PropertyName="adddata1">
                                    </dx:FilterControlTextColumn>
                                    <dx:FilterControlTextColumn ColumnType="String" DisplayName="Дополтительные" 
                                        PropertyName="adddata2">
                                    </dx:FilterControlTextColumn>
                                </Columns>
                                <SettingsLoadingPanel Enabled="False" />
                                <ClientSideEvents Applied="function(s, e) {
	gridSearchView.PerformCallback('ApplyExtFilter');
}" />
                            </dx:ASPxFilterControl>
                            </ContentTemplate> </asp:UpdatePanel>
                            <div style="float:left; padding-top:10px;"><dx:ASPxButton runat="server" ID="btnApply" Text="Поиск" AutoPostBack="false" 
        UseSubmitBehavior="false">
    <ClientSideEvents Click="function() { gridSearchView.PerformCallback('ApplyExtFilter'); }" />
</dx:ASPxButton></div><div style="float:left;padding-left:20px; padding-top:10px;">
  <dx:ASPxButton runat="server" ID="btnReset" Text="Очистить" AutoPostBack="false" 
        UseSubmitBehavior="false">
    <ClientSideEvents Click="function() { filter.Reset(); }" />
</dx:ASPxButton></div>                         
                            </div>

                           
                            <br />
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <div class="page_table">
                                <div style="float:left">Результаты поиска:</div><div style="float:left"><asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                    AssociatedUpdatePanelID="UpdatePanel3">
                                                <ProgressTemplate>
                <img src="/content/images/progress.gif">
            </ProgressTemplate>
                                </asp:UpdateProgress></div><br />
                <hr style="width: 100%;" />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                    <dx:ASPxPager ID="gridSearchViewPager" runat="server" ItemCount="1000" 
                                        ItemsPerPage="100" 
                                        onpageindexchanged="gridSearchViewPager_PageIndexChanged" Visible="False" 
                                        onpagesizechanged="gridSearchViewPager_PageSizeChanged" 
                                        ShowNumericButtons="False" Theme="SandboxTheme">
                                        <Summary AllPagesText="{0}-{1} из {2}" Text="{0}-{1} из {2}" />
                                        <PageSizeItemSettings AllItemText="Все" Caption="" Visible="True">
                                        </PageSizeItemSettings>
        </dx:ASPxPager>

                <dx:ASPxGridView ID="gridSearchView" runat="server"  AutoGenerateColumns="False" 
            EnableTheming="True" Theme="Default"  
            Width="100%" 
        style="margin-top: 0px; margin-right: 9px;" KeyFieldName="Id" 
                    DataSourceID="LinqServerModeDataSource1" 
                    ClientInstanceName="gridSearchView" 
                    oncustomcallback="gridSearchView_CustomCallback" ClientVisible="False" 
                    EnableCallBacks="False" >
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="Id"  VisibleIndex="0" 
                ReadOnly="True" Caption="№">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ModuleId" 
                VisibleIndex="2" Caption="Модуль">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="EventCode" VisibleIndex="5" 
                            Caption="Событие">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="who" VisibleIndex="6" Caption="Цель">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="dest" VisibleIndex="7" Caption="Объект">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="10" 
                            Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="rschId" 
                VisibleIndex="11" GroupIndex="0" SortIndex="0" SortOrder="Ascending" 
                            Caption="Исследование">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="pid1" 
                VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="pid2" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="adddata1" VisibleIndex="8" 
                            Caption="Данные">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="adddata2" 
                VisibleIndex="9" Caption="Дополнительные">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataDateColumn FieldName="timeofevent" VisibleIndex="1" 
                            Caption="Время события">
                        </dx:GridViewDataDateColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" AutoExpandAllGroups="True" 
                        EnableCustomizationWindow="True" EnableRowHotTrack="True" />
                    <SettingsPager PageSize="50" Visible="False">
                    </SettingsPager>
                    <SettingsLoadingPanel Text="Загрузка&amp;hellip;">
                    </SettingsLoadingPanel>
                </dx:ASPxGridView>
                                           </ContentTemplate> </asp:UpdatePanel>

</div>
</div>

</asp:Content>
