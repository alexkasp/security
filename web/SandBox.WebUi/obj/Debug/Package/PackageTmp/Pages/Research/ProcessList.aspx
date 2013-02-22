<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ProcessList.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.ProcessList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />

       <div id='page_header'>
          &nbsp;<dx:ASPxLabel ID="ASPxLabel1" runat="server" Theme="iOS">
           </dx:ASPxLabel>
           <br />
           <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" 
               Text="Просмотр дерева процессов" 
               NavigateUrl="~/Pages/Research/ProcTree.aspx" />
           <br />
           <br />
           <dx:ASPxGridView ID="GVProcesses" runat="server" AutoGenerateColumns="False" 
               Width="1036px" onafterperformcallback="GVProcesses_AfterPerformCallback">
               <Columns>
                   <dx:GridViewDataTextColumn FieldName="rschID" Visible="False" VisibleIndex="0">
                       <PropertiesTextEdit>
                           <ValidationSettings ErrorText="Неверное значение">
                               <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                           </ValidationSettings>
                       </PropertiesTextEdit>
                   </dx:GridViewDataTextColumn>
                   <dx:GridViewDataTextColumn Caption="Процесс" FieldName="procName" 
                       VisibleIndex="1">
                       <PropertiesTextEdit>
                           <ValidationSettings ErrorText="Неверное значение">
                               <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                           </ValidationSettings>
                       </PropertiesTextEdit>
                   </dx:GridViewDataTextColumn>
                   <dx:GridViewDataTextColumn Caption="pid" FieldName="pid1" VisibleIndex="2">
                       <PropertiesTextEdit>
                           <ValidationSettings ErrorText="Неверное значение">
                               <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                           </ValidationSettings>
                       </PropertiesTextEdit>
                   </dx:GridViewDataTextColumn>
                   <dx:GridViewDataTextColumn FieldName="pid2" VisibleIndex="3">
                       <PropertiesTextEdit>
                           <ValidationSettings ErrorText="Неверное значение">
                               <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                           </ValidationSettings>
                       </PropertiesTextEdit>
                   </dx:GridViewDataTextColumn>
                   <dx:GridViewDataTextColumn Caption="Число потоков" FieldName="streamsCount" 
                       VisibleIndex="4">
                       <PropertiesTextEdit>
                           <ValidationSettings ErrorText="Неверное значение">
                               <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                           </ValidationSettings>
                       </PropertiesTextEdit>
                   </dx:GridViewDataTextColumn>
               </Columns>
               <settingspager visible="False">
               </settingspager>
<SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
           </dx:ASPxGridView>
       </div>

</asp:Content>
