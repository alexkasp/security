<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="SetMonitor.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.SetMonitor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="padding:5px">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server"
            Text="Контролируемые события для исследования" Theme="iOS">
        </dx:ASPxLabel>
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" Theme="Default" Width="100%" 
            AutoGenerateColumns="False" KeyFieldName="Id" 
            onprerender="ASPxGridView1_PreRender">
            <Columns>
                <dx:GridViewCommandColumn Visible="False" VisibleIndex="0">
                    <ClearFilterButton Visible="True">
                    </ClearFilterButton>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="Id" Visible="False" VisibleIndex="4">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Корневая классификация" FieldName="FClass" 
                    VisibleIndex="1">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Значение" FieldName="Value" 
                    VisibleIndex="3">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Вторичная классификация" FieldName="SClass" 
                    VisibleIndex="2">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Статус" FieldName="Status" VisibleIndex="5">
                    <PropertiesTextEdit>
                        <ValidationSettings ErrorText="Неверное значение">
                            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
                        </ValidationSettings>
                    </PropertiesTextEdit>
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsBehavior AllowFocusedRow="True" />
            <SettingsPager PageSize="30" Visible="False">
            </SettingsPager>
            <Settings ShowFilterRow="True" ShowGroupPanel="True" />
<SettingsLoadingPanel Text="Загрузка&amp;hellip;"></SettingsLoadingPanel>
        </dx:ASPxGridView>
        <div style="padding:3px">
        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Удалить выбранное событие" 
            Width="100%" onclick="ASPxButton2_Click">
        </dx:ASPxButton>
        </div>
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" 
            HeaderText="Новое событие" Theme="DevEx" Width="100%">
            <PanelCollection>
<%--<dx:PanelContent runat="server" SupportsDisabledAttribute="True">--%>
<dx:PanelContent ID="PanelContent1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <dx:ASPxLabel ID="ASPxLabel6" runat="server" 
                Text="Корневая классификация события">
            </dx:ASPxLabel>
            <br />
            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
                DataSourceID="SqlDataSource1" DataTextField="RootElementOfClassification" 
                DataValueField="RootElementOfClassification" Height="1.5em" 
                onselectedindexchanged="DropDownList3_SelectedIndexChanged" 
                ViewStateMode="Enabled" Width="25%" onprerender="DropDownList3_PreRender">
            </asp:DropDownList>
            <br />
            <br />
            <dx:ASPxLabel ID="ASPxLabel7" runat="server" 
                Text="Вторичная классификация события">
            </dx:ASPxLabel>
            <br />
            <asp:DropDownList ID="DDLSClass" runat="server" Height="1.5em" 
                onload="DDLSClass_Load" onprerender="DDLSClass_PreRender" 
                onselectedindexchanged="DDLSClass_SelectedIndexChanged" 
                ontextchanged="DDLSClass_TextChanged" Width="25%">
            </asp:DropDownList>
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:sandBoxConnectionString %>" 
                SelectCommand="SELECT [RootElementOfClassification] FROM [RootElementsOfClassification]">
            </asp:SqlDataSource>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Значение">
    </dx:ASPxLabel>
    <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Height="1.5em" Width="100%">
        <ValidationSettings ErrorText="Неверное значение">
            <RegularExpression ErrorText="Ошибка проверки регулярного выражения" />
        </ValidationSettings>
    </dx:ASPxTextBox>
    
    <br />
    <table style="width:100%;">
        <tr>
            <td style="width: 142px">
            <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Сохранить" 
                    OnClick="ASPxButton1_Click">
    </dx:ASPxButton>
                </td>
            <td style="width: 527px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
       
    </table>
    <br />
    
                </dx:PanelContent>
</PanelCollection>
        </dx:ASPxRoundPanel>
        <br />
    </div>
</asp:Content>
