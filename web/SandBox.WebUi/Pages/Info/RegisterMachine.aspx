<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="RegisterMachine.aspx.cs" Inherits="SandBox.WebUi.Pages.Info.RegisterMachine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Имя новой Лир:">
    </dx:ASPxLabel>
    <dx:ASPxTextBox ID="tbLir" runat="server" Width="170px">
    </dx:ASPxTextBox>
    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Система:">
    </dx:ASPxLabel>
    <asp:DropDownList ID="ddList" runat="server">
    </asp:DropDownList>
    <dx:ASPxButton ID="btnCreate" runat="server" Text="Создать" onclick="BtnCreateClick">
    </dx:ASPxButton>  
</asp:Content>

