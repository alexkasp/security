<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Light.master" CodeBehind="Register.aspx.cs" Inherits="SandBox.WebUi.Account.Register" %>

<asp:content id="ClientArea" contentplaceholderid="MainContent" runat="server">

    <div class="accountHeader">
    <h2>
        Создание пользователя
    </h2>
</div>
    <dx:ASPxLabel ID="lblUserName" runat="server" AssociatedControlID="tbUserName" Text="Имя пользователя:" />
	<div class="form-field">
	    <dx:ASPxTextBox ID="tbUserName" runat="server" Width="200px">
	        <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
	            <RequiredField ErrorText=" " IsRequired="true" />
	        </ValidationSettings>
	    </dx:ASPxTextBox>
    </div>
    <dx:ASPxLabel ID="lblPassword" runat="server" AssociatedControlID="tbPassword" Text="Пароль:" />
    <div class="form-field">
		<dx:ASPxTextBox ID="tbPassword" ClientInstanceName="Password" Password="true" runat="server"
	        Width="200px">
	        <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
	            <RequiredField ErrorText=" " IsRequired="true" />
	        </ValidationSettings>
	    </dx:ASPxTextBox>
    </div>
    <dx:ASPxLabel ID="lblConfirmPassword" runat="server" AssociatedControlID="tbConfirmPassword"
        Text="Потверждение пароля:" />
	<div class="form-field">
	    <dx:ASPxTextBox ID="tbConfirmPassword" Password="true" runat="server" Width="200px">
	        <ValidationSettings ValidationGroup="RegisterUserValidationGroup">
	            <RequiredField ErrorText=" " IsRequired="true" />
	        </ValidationSettings>
	        <ClientSideEvents Validation="function(s, e) {
				var originalPasswd = Password.GetText();
				var currentPasswd = s.GetText();
				e.isValid = (originalPasswd  == currentPasswd );
				e.errorText = ' ';
			}" />
	    </dx:ASPxTextBox>
    </div>
    <dx:ASPxButton ID="btnCreateUser" runat="server" Text="Создать" ValidationGroup="RegisterUserValidationGroup"
        OnClick="btnCreateUser_Click">
    </dx:ASPxButton>
</asp:content>