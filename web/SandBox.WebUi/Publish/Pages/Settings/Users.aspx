<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="SandBox.WebUi.Pages.Settings.Users" %>
<%@ Register TagPrefix="dxwgv" Namespace="DevExpress.Web.ASPxGridView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" type="text/css" href="../../Content/PageView.css"  />

<div id='page_header'>
    Управление пользователями
</div>

<table class='panel'>
			<tbody>
                <tr>
                  <td class='panel-left'>
                      <div class='panel-text'>
                        <dx:ASPxHyperLink ID="ASPxHyperLink2" runat="server" Text="Добавить пользователя" NavigateUrl="~/Account/AddUser.aspx">
                        </dx:ASPxHyperLink>
                      </div>
                  </td>
                </tr>
			</tbody>
</table>

<div class="page_table">
   <dx:ASPxGridView ID="gridViewUsers" runat="server" AutoGenerateColumns="False" 
        oncustombuttoncallback="gridViewUsers_CustomButtonCallback" 
        onrowdeleted="gridViewUsers_RowDeleted">
        <Columns>
            <dx:GridViewCommandColumn Name="Commands" VisibleIndex="4">
                <ClearFilterButton Visible="True">
                </ClearFilterButton>
                <CustomButtons>
                   <dx:GridViewCommandColumnCustomButton ID="cbDelete" Text="удалить">
                   </dx:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn Caption="id пользователя" FieldName="UserId" 
                VisibleIndex="0" Visible=false>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="имя пользователя" FieldName="Login" 
                VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="дата создания" FieldName="CreatedDate" 
                VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="дата последнего входа" 
                FieldName="LastLoginDate" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="группа" FieldName="Name" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
</div>
</asp:Content>
