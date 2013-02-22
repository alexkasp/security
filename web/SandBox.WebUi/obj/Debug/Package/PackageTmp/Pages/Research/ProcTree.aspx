<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ProcTree.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.ProcTree" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id='page_header' style="padding:5px;">
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Theme="iOS">   
    </dx:ASPxLabel>
    <asp:TreeView ID="TreeView1" runat="server" ImageSet="Simple" Width="100%">
        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" 
            HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
        <ParentNodeStyle Font-Bold="False" />
        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
            HorizontalPadding="0px" VerticalPadding="0px" />
    </asp:TreeView>
</div>
</asp:Content>
