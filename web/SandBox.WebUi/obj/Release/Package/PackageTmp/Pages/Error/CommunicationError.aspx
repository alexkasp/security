<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Empty.master" CodeBehind="CommunicationError.aspx.cs" Inherits="SandBox.WebUi.Pages.Error.CommunicationError" %>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/Error.css"  />
    <div id="error_container">
       <div class=center>Ошибка | нет соединения с удалённым сервером</div>
       <div class=center>
          <a href="../../Account/Login.aspx">Обновить</a>
       </div>
    </div>
</asp:Content>
