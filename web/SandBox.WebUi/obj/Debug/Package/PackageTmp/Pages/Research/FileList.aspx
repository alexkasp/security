<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="FileList.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.FileList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />

       <div id='page_header'>
          &nbsp;<dx:ASPxLabel ID="ASPxLabel1" runat="server" 
               Text="Нет текущего исследования" Theme="iOS">
           </dx:ASPxLabel>
           <dx:ASPxCheckBox ID="ASPxCheckBox1" runat="server" Checked="True" 
               CheckState="Checked" 
               Text="Сопоставление идентификатора среды с идентификатором исследования">
           </dx:ASPxCheckBox>
           <dx:ASPxGridView ID="GVFileList" runat="server" 
               AutoGenerateColumns="False" Width="979px">
               <Columns>
                   <dx:GridViewDataTextColumn FieldName="Name" VisibleIndex="0" 
                       Caption="Имя ресурса">
                       <PropertiesTextEdit>
                           <ValidationSettings ErrorText="Неверное значение">
                               <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                           </ValidationSettings>
                       </PropertiesTextEdit>
                   </dx:GridViewDataTextColumn>
                   <dx:GridViewDataTextColumn Caption="Дополнительная информация" FieldName="EventAdditionalInfo" 
                       VisibleIndex="2">
                       <PropertiesTextEdit>
                           <ValidationSettings ErrorText="Неверное значение">
                               <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                           </ValidationSettings>
                       </PropertiesTextEdit>
                   </dx:GridViewDataTextColumn>
                   <dx:GridViewDataTextColumn Caption="Тип" FieldName="Name" 
                       VisibleIndex="1">
                       <PropertiesTextEdit>
                           <ValidationSettings ErrorText="Неверное значение">
                               <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                           </ValidationSettings>
                       </PropertiesTextEdit>
                   </dx:GridViewDataTextColumn>
               </Columns>
               <SettingsPager Visible="False">
               </SettingsPager>
<SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
           </dx:ASPxGridView>
       </div>

</asp:Content>
