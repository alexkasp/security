<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="DirectoryOfEvents.aspx.cs" Inherits="SandBox.WebUi.Pages.Information.DirectoryOfEvents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div style="padding:5px">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Справочник событий" 
        Theme="iOS"> 
    </dx:ASPxLabel>
    <hr />
    <div style="width: 50%;">
        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Создать новое событие">
        </dx:ASPxLabel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        

        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" 
            LoadingPanelText="Загрузка&amp;hellip;" Width="100%" SelectedIndex="0" 
            AutoPostBack="True">
            <Items>
                <dx:ListEditItem Text="Критически важное" Value="0" Selected="True" />
                <dx:ListEditItem Text="Важное" Value="1" />
            </Items>
<ValidationSettings ErrorText="Неверное значение">
<RegularExpression ErrorText="Ошибка проверки регулярного выражения"></RegularExpression>
</ValidationSettings>
        </dx:ASPxComboBox>
        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" 
            Width="100%" AutoPostBack="True" 
            onselectedindexchanged="ASPxComboBox2_SelectedIndexChanged" 
                LoadingPanelText="Загрузка&amp;hellip;">
        </dx:ASPxComboBox>
        <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" ValueType="System.String" 
            Width="100%">
        </dx:ASPxComboBox>
        </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Субъект воздействия">
        </dx:ASPxLabel>
        <dx:ASPxTextBox ID="ASPxTextBox2" runat="server" Width="100%">
        </dx:ASPxTextBox>
        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Объект воздействия">
        </dx:ASPxLabel>
        <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="100%">
        </dx:ASPxTextBox>
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Добавить" 
            onclick="ASPxButton1_Click">
        </dx:ASPxButton>
    </div>
    <br />
    <div <%--style="position:relative; float:left; width: 50%; padding-right:5px;"--%>>
       <dx:ASPxGridView ID="ASPxGridView1" runat="server" 
        AutoGenerateColumns="False" Width="100%" KeyFieldName="Id" Theme="Default" 
            ViewStateMode="Enabled">
           <Columns>
               <dx:GridViewDataTextColumn Caption="Модуль" VisibleIndex="3" FieldName="module">
                   <PropertiesTextEdit>
                       <ValidationSettings ErrorText="Неверное значение">
                           <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                       </ValidationSettings>
                   </PropertiesTextEdit>
               </dx:GridViewDataTextColumn>
               <dx:GridViewDataTextColumn Caption="Субъект воздействия" FieldName="Who" 
                   Name="Who" VisibleIndex="5">
                   <PropertiesTextEdit>
                       <ValidationSettings ErrorText="Неверное значение">
                           <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                       </ValidationSettings>
                   </PropertiesTextEdit>
               </dx:GridViewDataTextColumn>
               <dx:GridViewDataTextColumn Caption="Тип события" VisibleIndex="4" 
                   FieldName="eventt">
                   <PropertiesTextEdit>
                       <ValidationSettings ErrorText="Неверное значение">
                           <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                       </ValidationSettings>
                   </PropertiesTextEdit>
               </dx:GridViewDataTextColumn>
               <dx:GridViewDataTextColumn Caption="Объект воздействия" VisibleIndex="6" 
                   FieldName="dest">
                   <PropertiesTextEdit>
                       <ValidationSettings ErrorText="Неверное значение">
                           <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                       </ValidationSettings>
                   </PropertiesTextEdit>
               </dx:GridViewDataTextColumn>
               <dx:GridViewDataTextColumn Caption="Id" FieldName="Id" Name="Id" 
                   Visible="False" VisibleIndex="1">
                   <PropertiesTextEdit>
                       <ValidationSettings ErrorText="Неверное значение">
                           <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                       </ValidationSettings>
                   </PropertiesTextEdit>
               </dx:GridViewDataTextColumn>
               <dx:GridViewDataTextColumn Caption="Важность" FieldName="significance" 
                   GroupIndex="0" SortIndex="0" SortOrder="Ascending" VisibleIndex="2">
                   <PropertiesTextEdit>
                       <ValidationSettings ErrorText="Неверное значение">
                           <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                       </ValidationSettings>
                   </PropertiesTextEdit>
               </dx:GridViewDataTextColumn>
           </Columns>
           <SettingsBehavior AllowFocusedRow="True" />
           <SettingsPager AlwaysShowPager="True" PageSize="100">
           </SettingsPager>
<SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
       </dx:ASPxGridView>
        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Удалить выбранное событие" 
            onclick="ASPxButton2_Click">
        </dx:ASPxButton>
    </div>
    </div>
</asp:Content>
