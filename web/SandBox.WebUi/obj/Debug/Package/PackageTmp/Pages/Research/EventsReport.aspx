<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="EventsReport.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.EventsReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div style="padding:5px;">

    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Theme="iOS">
    </dx:ASPxLabel>
    <br />
    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Theme="iOS" Visible="False">
    </dx:ASPxLabel>
    <hr />
    <p>Таблица важных событий</p>
       <dx:ASPxGridView ID="gridViewReports" runat="server"  AutoGenerateColumns="False" 
            EnableTheming="True" Theme="Default"  
            KeyFieldName="Id" Width="90%" 
        style="margin-top: 0px; margin-right: 9px;" >
        <Columns>
            <dx:GridViewDataTextColumn FieldName="Sign"  VisibleIndex="1" Name="Sign" 
                Caption="Важность" GroupIndex="0" SortIndex="0" SortOrder="Ascending">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>

            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Id" Name="Id" ReadOnly="True" 
                Visible="False" VisibleIndex="0">
                <PropertiesTextEdit>
                    <ValidationSettings ErrorText="Неверное значение">
                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                    </ValidationSettings>
                </PropertiesTextEdit>
                <EditFormSettings Visible="False" />
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="RschId" VisibleIndex="2" 
                Visible="False">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="ModuleId" VisibleIndex="3" Width="50px" 
                Caption="Модуль">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Действие" FieldName="EventCode" 
                VisibleIndex="4" Width="50px">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Описание" FieldName="Description" 
                VisibleIndex="10">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Объект" FieldName="Who" VisibleIndex="9" 
                Width="100px">
<PropertiesTextEdit>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
</PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Цель" FieldName="Dest" VisibleIndex="5">
                <PropertiesTextEdit>
                    <ValidationSettings ErrorText="Неверное значение">
                        <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
        </Columns>
        <SettingsPager PageSize="30">
        </SettingsPager>
        <Settings ShowFilterRow="True" />

<SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
    </dx:ASPxGridView>
    <br />
    Распределение событий<br />
    <dx:WebChartControl ID="WebChartControl1" runat="server" Height="300px" 
        Width="600px">
    </dx:WebChartControl>
    <br />
    Оставить комментарий к ВПО:<br />
    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Height="1.5em" Width="600px">
    </dx:ASPxTextBox>
    <dx:ASPxButton ID="ASPxButton1" runat="server" onclick="ASPxButton1_Click" 
        Text="Ок">
    </dx:ASPxButton>
    <br />
</div>
</asp:Content>
