<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="CurrentReportList.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.CurrentReportList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style = "padding:5px;">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="" Theme="iOS">
        </dx:ASPxLabel>
        <hr />
        <br />
        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Монитор событий">
        </dx:ASPxLabel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <dx:ASPxGridView ID="EventsView" runat="server" AutoGenerateColumns="False" 
                KeyFieldName="Id" onhtmlrowprepared="EventsView_HtmlRowPrepared" Width="100%">
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="Id" Name="Id" Visible="False" 
                        VisibleIndex="0">
                        <PropertiesTextEdit>
                            <ValidationSettings ErrorText="Неверное значение">
                                <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="RschId" Name="RschId" Visible="False" 
                        VisibleIndex="1">
                        <PropertiesTextEdit>
                            <ValidationSettings ErrorText="Неверное значение">
                                <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Модуль" FieldName="ModuleId" 
                        Name="ModuleId" VisibleIndex="2">
                        <PropertiesTextEdit>
                            <ValidationSettings ErrorText="Неверное значение">
                                <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Действие" FieldName="EventCode" 
                        Name="EventCode" VisibleIndex="3">
                        <PropertiesTextEdit>
                            <ValidationSettings ErrorText="Неверное значение">
                                <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Цель" FieldName="Dest" Name="Dest" 
                        VisibleIndex="4">
                        <PropertiesTextEdit>
                            <ValidationSettings ErrorText="Неверное значение">
                                <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Объект" FieldName="Who" Name="Who" 
                        VisibleIndex="5">
                        <PropertiesTextEdit>
                            <ValidationSettings ErrorText="Неверное значение">
                                <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Описание" FieldName="Description" 
                        Name="Description" VisibleIndex="6">
                        <PropertiesTextEdit>
                            <ValidationSettings ErrorText="Неверное значение">
                                <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                </Columns>
                <SettingsPager PageSize="50">
                </SettingsPager>
                <SettingsLoadingPanel Text="Загрузка&amp;hellip;" />
            </dx:ASPxGridView>
            <asp:Timer runat="server" Interval="1000" ontick="Unnamed1_Tick">
            </asp:Timer>
            <dx:ASPxButton ID="ASPxButton1" runat="server" onclick="ASPxButton1_Click" 
                Text="Остановить монитор" Visible="False">
            </dx:ASPxButton>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
