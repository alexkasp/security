<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Comparer.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.Comparer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxLabel ID="LHeader" runat="server" Theme="iOS">
    </dx:ASPxLabel>
    <asp:TreeView ID="TreeView1" runat="server" style="margin-left: 36px" 
        ImageSet="Simple">
        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" 
            HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
        <ParentNodeStyle Font-Bold="False" />
        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
            HorizontalPadding="0px" VerticalPadding="0px" />
    </asp:TreeView>
    <br />
    <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String">
    </dx:ASPxComboBox>
    <dx:ASPxButton ID="ASPxButton1" runat="server" onclick="ASPxButton1_Click" 
        Text="Сравнить">
    </dx:ASPxButton>
    <br />
    <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" 
        EnableTheming="True" Theme="Default" Width="600px" 
        onhtmldatacellprepared="ASPxTreeList1_HtmlDataCellPrepared">
        <Settings GridLines="Both" />
<SettingsCustomizationWindow Caption="Выбор колонок"></SettingsCustomizationWindow>

<SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>

<SettingsPopupEditForm Caption="Форма редактирования"></SettingsPopupEditForm>

<SettingsText ConfirmDelete="Подверждаете удаление?" CommandEdit="Изменить" CommandNew="Создать" CommandDelete="Удалить" CommandUpdate="Сохранить" CommandCancel="Отмена" RecursiveDeleteError="Узел имеет дочерние узлы." CustomizationWindowCaption="Выбор колонок" LoadingPanelText="Загрузка&amp;hellip;"></SettingsText>
    </dx:ASPxTreeList>
</asp:Content>
