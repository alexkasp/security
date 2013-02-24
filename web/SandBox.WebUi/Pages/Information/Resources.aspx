<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Resources.aspx.cs" Inherits="SandBox.WebUi.Pages.Information.Resources" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	    <div id="content-top">
		    <div id="pagename">Ресурсы</div>
		    <div id="toolbuttons">
            <table>
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnAddLIR" AutoPostBack="False" runat="server" 
                            Text="Добавить ЛИР"  Visible="False">
                            <ClientSideEvents Click="function(s, e) {
	document.location.href = '/Pages/Information/CreateEtalonMachine.aspx';
}" />
                        </dx:ASPxButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnAddVLIR" AutoPostBack="False" runat="server" Text="Создать ВЛИР">
                            <ClientSideEvents Click="function(s, e) {
		document.location.href = '/Pages/Information/CreateMachine.aspx';
}" />
                        </dx:ASPxButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            </div>
		    <div id="tablenavbuttons">
                                                <dx:ASPxPager ID="gridResourceViewPager" runat="server" ItemCount="1000" 
                                        ItemsPerPage="100" 
                                        onpageindexchanged="gridResourceViewPager_PageIndexChanged" Visible="False" 
                                        onpagesizechanged="gridResourceViewPager_PageSizeChanged" 
                                        ShowNumericButtons="False" RenderMode="Lightweight">
                                        <Summary AllPagesText="{0}-{1} из {2}" Text="{0}-{1} из {2}" />
                                        <PageSizeItemSettings AllItemText="Все" Caption="" Visible="True">
                                        </PageSizeItemSettings>
        </dx:ASPxPager>
            </div>
		</div>
    <div id="content-main">

            <div class="page_table">
                <dx:ASPxLabel ID="labelNoItems" runat="server" Text="">
                </dx:ASPxLabel>
            </div>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                
                    <asp:Timer ID="Timer2" runat="server" Interval="5000" ontick="Timer2_Tick">
                    </asp:Timer>
            <dx:ASPxGridView ID="gridViewMachines" runat="server" 
        AutoGenerateColumns="False" EnableTheming="True" Theme="SandboxTheme" Width="100%" 
                    onhtmlrowprepared="GridViewMachinesHtmlRowPrepared"
                    oncustombuttoncallback="GridViewMachinesCustomButtonCallback" 
                        KeyFieldName="Id" EnableCallBacks="False" 
                        oncustomcallback="gridViewMachines_CustomCallback" 
                        ClientInstanceName="gridViewMachines" 
                        oncustombuttoninitialize="gridViewMachines_CustomButtonInitialize">
        <ClientSideEvents CustomButtonClick="function(s, e) {
 if (e.buttonID == 'btnDelete') {
 if (!confirm(&quot;Вы уверены, что хотите удалить ресурс?&quot;)) { return;}}
 gridViewMachines.PerformCallback(e.buttonID+','+ s.GetRowKey(e.visibleIndex));
}" />
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width = "50px" 
                Caption="Статус">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnStatus" Image-Url="../../Content/Images/Icons/run.png" Image-ToolTip="Запустить" Image-AlternateText="Запустить">
                        <Image AlternateText="Запустить" ToolTip="Запустить" 
                            Url="../../Content/Images/Icons/run.png">
                        </Image>
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewCommandColumn>

            <dx:GridViewDataTextColumn Caption="№" FieldName="Id" VisibleIndex="0">
                <PropertiesTextEdit>
                <ValidationSettings ErrorText="Неверное значение">
                <RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
                </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Имя" FieldName="Name" VisibleIndex="1" Width="100px">
                <PropertiesTextEdit>
                <ValidationSettings ErrorText="Неверное значение">
                <RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
                </ValidationSettings>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Тип" FieldName="Type" VisibleIndex="2" Width="50px">
                <PropertiesTextEdit>
                <ValidationSettings ErrorText="Неверное значение">
                <RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
                </ValidationSettings>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn> 
            <dx:GridViewDataTextColumn Caption="Система" FieldName="System" VisibleIndex="3" Width="150px">
                <PropertiesTextEdit>
                <ValidationSettings ErrorText="Неверное значение">
                <RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
                </ValidationSettings>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Исследование" FieldName="SessionName" 
                Name="SessionName" VisibleIndex="4">
                <PropertiesTextEdit>
                    <ValidationSettings ErrorText="Неверное значение">
                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <DataItemTemplate>
                    <dx:ASPxHyperLink ID="HLSession" runat="server" 
                        NavigateUrl="~/Pages/Research/CurrentReportList.aspx" />
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Состояние" FieldName="State" 
                VisibleIndex="5" Width="200px" Visible="False">
                <PropertiesTextEdit>
                <ValidationSettings ErrorText="Неверное значение">
                <RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
                </ValidationSettings>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="ВПО" FieldName="MlwrName" VisibleIndex="6" 
                Name="MlwrName">
                <PropertiesTextEdit>
                <ValidationSettings ErrorText="Неверное значение">
                <RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
                </ValidationSettings>
                </PropertiesTextEdit>
                <DataItemTemplate>
                    <dx:ASPxHyperLink ID="HLMlwr" runat="server" 
                        NavigateUrl="~/Pages/Malware/MalwareCard.aspx" />
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Тип среды" FieldName="EnvType" 
                VisibleIndex="7" Width="50px">
                <PropertiesTextEdit>
                <ValidationSettings ErrorText="Неверное значение">
                <RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
                </ValidationSettings>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Состояние среды" FieldName="EnvState" 
                VisibleIndex="8" Width="100px">
                <PropertiesTextEdit>
                <ValidationSettings ErrorText="Неверное значение">
                <RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
                </ValidationSettings>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ip-адрес" FieldName="EnvIp" 
                VisibleIndex="9" Width="100px">
                <PropertiesTextEdit>
                <ValidationSettings ErrorText="Неверное значение">
                <RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
                </ValidationSettings>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Mac-адрес" FieldName="EnvMac" 
                VisibleIndex="10" Width="120px">
                <PropertiesTextEdit>
                    <ValidationSettings ErrorText="Неверное значение">
                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <CellStyle HorizontalAlign="Left">
                </CellStyle>
            </dx:GridViewDataTextColumn>
            <dx:GridViewCommandColumn VisibleIndex="11" ButtonType="Image" Width = "50px" 
                Caption="Статус">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnDelete">
                        <Image ToolTip="Запустить" Url="../../Content/Images/Icons/delete.png" />
                    </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewCommandColumn>
        </Columns>
                <SettingsBehavior AllowFocusedRow="True" 
                    EnableRowHotTrack="True" />
         <SettingsLoadingPanel Mode="Disabled" />
    </dx:ASPxGridView>
                </ContentTemplate>
                </asp:UpdatePanel>
    </div>
</asp:Content>
