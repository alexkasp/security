<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.History" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    var keyValue;
    function OnLoadClick(element, key) {
        popup_load.ShowAtElement(element);
        callbackPanel_load.PerformCallback(key);
    }

    function OnFolowClick(key) {
        PageMethods.FolowMalware(key);
    }

    function OnAcceptLoad() {
        popup_load.Hide();
    }
</script>

<asp:UpdatePanel ID="PopupUpdatePanel" runat="server"  UpdateMode="Conditional">
    <ContentTemplate>
        <dx:ASPxPopupControl ID="popup_load" ClientInstanceName="popup_load" runat="server" AllowDragging="false" AllowResize="false" ShowCloseButton="false"
        PopupHorizontalAlign="OutsideRight" HeaderText="Загрузка объекта в влир:">
        <ContentCollection>
          <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxCallbackPanel ID="callbackPanel_load" ClientInstanceName="callbackPanel_load" runat="server"
                    Width="200px" Height="50px" OnCallback="CallbackPanelLoadCallback" RenderMode="Table">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent" runat="server">
                            <asp:Literal ID="litText" runat="server" Text=""></asp:Literal>
                            <asp:DropDownList ID="ddMachines" runat="server" Width="170px" 
                                AutoPostBack="false" OnSelectedIndexChanged="ddMachines_SelectedIndexChanged">
                            </asp:DropDownList>
                            <dx:ASPxButton ID="btnLoad" runat="server" Text="загрузить" 
                                AutoPostBack="False" OnClick="BtnLoadClick">
                                <ClientSideEvents Click="OnAcceptLoad" />
                            </dx:ASPxButton>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>    
    </dx:ASPxPopupControl>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanelMalware" runat="server"  UpdateMode="Conditional">
    <ContentTemplate>
        <dx:ASPxGridView ID="gridViewMalware" runat="server" 
            AutoGenerateColumns="False" EnableTheming="True" Theme="DevEx" 
            onheaderfilterfillitems="GridViewMalwareHeaderFilterFillItems">
            <Columns>
                <dx:GridViewDataTextColumn Caption="Id" FieldName="Id" VisibleIndex="0" Visible=false>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataColumn Caption="Исполняемый файл" FieldName="Path" VisibleIndex="1">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Классификация" FieldName="Class" VisibleIndex="2">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="Дополнительно" FieldName="Loaded" VisibleIndex="3">
                </dx:GridViewDataColumn>
                <dx:GridViewDataColumn Caption="" VisibleIndex="4">
                <DataItemTemplate>
                    <a href="javascript:;" onclick="OnLoadClick(this, '<%# Container.KeyValue %>')">загрузить</a>
                    <a href="javascript:;" onclick="OnFolowClick('<%# Container.KeyValue %>')">следить</a>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            </Columns>
            <Settings ShowFilterRow="True" />
        </dx:ASPxGridView>
    </ContentTemplate>
    <Triggers>
         <asp:AsyncPostBackTrigger ControlID="gridViewMalware" EventName="DataBinding" />
    </Triggers>
</asp:UpdatePanel>
</asp:Content>
