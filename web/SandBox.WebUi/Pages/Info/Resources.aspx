<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="Resources.aspx.cs" Inherits="SandBox.WebUi.Pages.Info.Resources" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    function OnCreateClick(element, key) {
        popup_create.ShowAtElement(element);
        callbackPanel_create.PerformCallback(key);
    }

    function OnAcceptCreate() {
        popup_create.Hide();
    }

    function OnDeleteClick(element, key) 
    {
        popup_delete.ShowAtElement(element);
        callbackPanel_delete.PerformCallback(key);
    }

    function OnAcceptDelete() 
    {
        popup_delete.Hide();
    }

    function OnStartClick(key) 
    {
        PageMethods.StartMachine(key);
    }

    function OnStopClick(key) 
    {
        PageMethods.StopMachine(key);
    }
</script>


<asp:UpdatePanel ID="PopupUpdatePanel" runat="server"  UpdateMode="Conditional">
    <ContentTemplate>
        <dx:ASPxPopupControl ID="popup_create" ClientInstanceName="popup_create" runat="server" AllowDragging="false" AllowResize="false" ShowCloseButton="false"
        PopupHorizontalAlign="OutsideRight" HeaderText="Создание ВЛИР:">
        <ContentCollection>
          <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dx:ASPxCallbackPanel ID="callbackPanel_create" ClientInstanceName="callbackPanel_create" runat="server"
                    Width="200px" Height="50px" OnCallback="CallbackPanelCreateCallback" RenderMode="Table">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent" runat="server">
                            <asp:Literal ID="litText" runat="server" Text=""></asp:Literal>
                            <dx:ASPxTextBox ID="tbId" runat="server" Visible="false">
                            </dx:ASPxTextBox>
                            <dx:ASPxTextBox ID="tbNewVmName" runat="server" Width="170px" >
                            </dx:ASPxTextBox>
                            <dx:ASPxButton ID="btnCreate" runat="server" Text="создать" 
                                AutoPostBack="False" OnClick="BtnCreateClick">
                                <ClientSideEvents Click="OnAcceptCreate" />
                            </dx:ASPxButton>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>    
    </dx:ASPxPopupControl>

    <dx:ASPxPopupControl ID="popup_delete" ClientInstanceName="popup_delete" runat="server" AllowDragging="false" AllowResize="false" ShowCloseButton="false"
        PopupHorizontalAlign="OutsideRight" HeaderText="Удаление ВЛИР:">
        <ContentCollection>
           <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <dx:ASPxCallbackPanel ID="callbackPanel_delete" ClientInstanceName="callbackPanel_delete" runat="server"
                    Width="250px" Height="50px" OnCallback="CallbackPanelDeleteCallback" RenderMode="Table">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">              
                            <asp:Literal ID="deleteText" runat="server" Text=""></asp:Literal>
                            <dx:ASPxButton ID="btnDelete" runat="server" Text="удалить" 
                                AutoPostBack="False" OnClick="BtnDeleteClick">
                                <ClientSideEvents Click="OnAcceptDelete" />
                            </dx:ASPxButton>
                       </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxCallbackPanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanelMachines" runat="server"  UpdateMode="Conditional">
    <ContentTemplate>

    <br />
    <dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Text="Добавить ЛИР" NavigateUrl="~/Pages/Info/RegisterMachine.aspx"/>
    <br /><br />

    <dx:ASPxGridView ID="gridViewMachines" runat="server" 
        AutoGenerateColumns="False" EnableTheming="True" Theme="DevEx" >
        <Columns>
            <dx:GridViewDataTextColumn Caption="Id" FieldName="Id" VisibleIndex="0" Visible=false>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Имя" FieldName="Name" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Тип" FieldName="Type" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Система" FieldName="System" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Состояние" FieldName="State" VisibleIndex="3">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataColumn Caption="" VisibleIndex="4">
                <DataItemTemplate>
                    <a href="javascript:;" onclick="OnStartClick('<%# Container.KeyValue %>')">запустить</a>
                    <a href="javascript:;" onclick="OnStopClick('<%# Container.KeyValue %>')">остановить</a>
                    <a href="javascript:;" onclick="OnCreateClick(this, '<%# Container.KeyValue %>')">создать ВЛИР</a>
                    <a href="javascript:;" onclick="OnDeleteClick(this, '<%# Container.KeyValue %>')">удалить</a>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
        </Columns>
        <ClientSideEvents CustomButtonClick="function(s, e) { if (e.buttonID == 'cbCreate') OnCreateClick(this, e.visibleIndex); }" />
    </dx:ASPxGridView>
    <asp:Timer ID="Update_timer" runat="server" Interval="1000"  ontick="UpdateTimerTick">
    </asp:Timer>
    </ContentTemplate>
    <Triggers>
         <asp:AsyncPostBackTrigger ControlID="gridViewMachines" EventName="DataBinding" />
    </Triggers>
</asp:UpdatePanel>
</asp:Content>
