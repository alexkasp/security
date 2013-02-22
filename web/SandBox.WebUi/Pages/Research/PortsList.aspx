<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="PortsList.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.PortsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding:5px;">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Theme="iOS" 
            EnableViewState="False">
        </dx:ASPxLabel>
        <hr />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server">
                </asp:Timer>
                <dx:ASPxButton ID="BUpdateView" runat="server" onclick="BUpdateView_Click" 
                    Text="Обновить таблицу">
                </dx:ASPxButton>
                <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
            KeyFieldName="Id" onhtmlrowprepared="ASPxGridView1_HtmlRowPrepared" 
            Width="100%">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="Id" Visible="False" VisibleIndex="0">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Номер порта" FieldName="port" 
                    VisibleIndex="1">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Тип" FieldName="type" VisibleIndex="2">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Статус" FieldName="status" VisibleIndex="3">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Адресат" FieldName="destination" 
                    VisibleIndex="4">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsPager PageSize="50">
            </SettingsPager>
<SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
        </dx:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        
        <br />
    </div>

</asp:Content>
