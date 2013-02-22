<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="RegistryList.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.RegistryList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />

       <div id='page_header'>
           <dx:ASPxLabel ID="ASPxLabel1" runat="server" Theme="iOS">
           </dx:ASPxLabel>
           <br />
           <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" 
               NavigateUrl="~/Pages/Research/RegsTree.aspx" Text="Просмотреть в виде дерева" />
           <dx:ASPxGridView ID="GVRegistry" runat="server" AutoGenerateColumns="False" 
               Width="1066px">
               <Columns>
                   <dx:GridViewDataTextColumn FieldName="KeyName" VisibleIndex="1" Caption="Узел">
                       <PropertiesTextEdit>
                           <ValidationSettings ErrorText="Неверное значение">
                               <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                           </ValidationSettings>
                       </PropertiesTextEdit>
                   </dx:GridViewDataTextColumn>
                   <dx:GridViewDataTextColumn Caption="Родительский узел" FieldName="Parent" 
                       VisibleIndex="0">
                       <PropertiesTextEdit>
                           <ValidationSettings ErrorText="Неверное значение">
                               <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                           </ValidationSettings>
                       </PropertiesTextEdit>
                   </dx:GridViewDataTextColumn>
               </Columns>
<SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
           </dx:ASPxGridView>
       </div>

</asp:Content>
