<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Empty.master" CodeBehind="DbError.aspx.cs" Inherits="SandBox.WebUi.Pages.Error.DbError" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/Error.css"  />
    <div id="error_container">
       <div class=center>Ошибка | нет соединения с базой данных</div>
       <div class=center>
          <a href="../../Account/Login.aspx">Обновить</a>
       </div>
    </div>
</asp:Content>
