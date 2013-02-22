<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ChartOfEvents.aspx.cs" Inherits="SandBox.WebUi.Pages.Research.ChartOfEvents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style = "padding:5px;">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="ASPxLabel" Theme="iOS">
        </dx:ASPxLabel>
        <table style="width:100%;">
            <tr>
                <td style="width: 140px">
                    <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Номер исследования:">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel7" runat="server">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
                <td style="width: 140px">
                    <dx:ASPxLabel ID="ASPxLabel6" runat="server" Text="ОС:">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel8" runat="server">
                    </dx:ASPxLabel>
                </td>
            </tr>
            <tr>
            <td height="1%">
                <dx:ASPxLabel ID="ASPxLabel10" runat="server" Text="Параметры исследования:">
                </dx:ASPxLabel>
            </td>
            <td rowspan="2">
                <asp:TreeView ID="TreeView2" runat="server" ImageSet="Simple">
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="Black" 
                        HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" />
                    <ParentNodeStyle Font-Bold="False" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
                        HorizontalPadding="0px" VerticalPadding="0px" />
                </asp:TreeView>
            </td>
            </tr>
            <tr>
            <td height="100%;">
                &nbsp;</td>
            </tr>
        </table>
        <hr />
        <div style = "padding:5px;">
            <table style="width:100%;">
                <tr>
                    <td colspan="2" height="3%">
                        &nbsp;</td>
                    <td rowspan="8">
        <dx:WebChartControl ID="WebChartControl1" runat="server" Height="600px" 
                LoadingPanelText="Загрузка&amp;hellip;" SideBySideEqualBarWidth="True" 
                Width="900px">
            <diagramserializable>
                <dx:XYDiagram>
                    <axisx gridspacingauto="False" visibleinpanesserializable="-1" visible="False">
                        <tickmarks minorvisible="False" />
                        <range sidemarginsenabled="False" />
<Tickmarks MinorVisible="False" visible="False"></Tickmarks>

<Range SideMarginsEnabled="False"></Range>
                    </axisx>
                    <axisy visible="False" visibleinpanesserializable="-1" minorcount="1">
                        <range auto="False" maxvalueserializable="7" minvalueserializable="0" 
                            sidemarginsenabled="False" />
                        <tickmarks minorvisible="False" visible="False" />
<Range Auto="False" MinValueSerializable="0" MaxValueSerializable="7" SideMarginsEnabled="False"></Range>
                        <gridlines visible="False">
                        </gridlines>
                    </axisy>
                </dx:XYDiagram>
            </diagramserializable>
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

            <seriesserializable>
                <dx:Series Name="Ряд 1" ShowInLegend="False" SynchronizePointOptions="False">
                    <viewserializable>
                        <dx:SideBySideRangeBarSeriesView>
                        </dx:SideBySideRangeBarSeriesView>
                    </viewserializable>
                    <labelserializable>
                        <dx:RangeBarSeriesLabel>
                            <fillstyle>
                                <optionsserializable>
                                    <dx:SolidFillOptions />
                                </optionsserializable>
                            </fillstyle>
                            <pointoptionsserializable>
                                <dx:RangeBarPointOptions>
                                </dx:RangeBarPointOptions>
                            </pointoptionsserializable>
                        </dx:RangeBarSeriesLabel>
                    </labelserializable>
                    <legendpointoptionsserializable>
                        <dx:RangeBarPointOptions>
                        </dx:RangeBarPointOptions>
                    </legendpointoptionsserializable>
                </dx:Series>
            </seriesserializable>

            <seriestemplate>
                <viewserializable>
                    <dx:SideBySideRangeBarSeriesView>
                    </dx:SideBySideRangeBarSeriesView>
                </viewserializable>
                <labelserializable>
                    <dx:RangeBarSeriesLabel>
                        <fillstyle>
                            <optionsserializable>
                                <dx:SolidFillOptions />
                            </optionsserializable>
                        </fillstyle>
                        <pointoptionsserializable>
                            <dx:RangeBarPointOptions>
                            </dx:RangeBarPointOptions>
                        </pointoptionsserializable>
                    </dx:RangeBarSeriesLabel>
                </labelserializable>
                <legendpointoptionsserializable>
                    <dx:RangeBarPointOptions>
                    </dx:RangeBarPointOptions>
                </legendpointoptionsserializable>
            </seriestemplate>

<CrosshairOptions showcrosshairlabels="False"><CommonLabelPositionSerializable>
<dx:CrosshairMousePosition></dx:CrosshairMousePosition>
</CommonLabelPositionSerializable>
</CrosshairOptions>

<ToolTipOptions showforpoints="False" showforseries="True"><ToolTipPositionSerializable>
<dx:ToolTipMousePosition></dx:ToolTipMousePosition>
</ToolTipPositionSerializable>
</ToolTipOptions>
        </dx:WebChartControl>
                    </td>
                </tr>
                <tr>
                    <td align="center" height="45%" rowspan="3">
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" 
                            Text="Нет исследования для сравнения" Theme="iOS">
                        </dx:ASPxLabel>
                        <br />
                        <br />
                    </td>
                    <td dir="rtl" height="80px;">
                        критические<br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td dir="rtl" height="104px;">
                        важные<br />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td height="94px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" height="45%" rowspan="3">
                        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Нет текущего исследования" 
                            Theme="iOS">
                        </dx:ASPxLabel>
                    </td>
                    <td dir="rtl" height="94px;">
                        <br />
                        <br />
                        <br />
                        <br />
                        критические</td>
                </tr>
                <tr>
                    <td dir="rtl" height="114px;">
                        <br />
                        <br />
                        важные</td>
                </tr>
                <tr>
                    <td align="center" height="74px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" height="4%">
                        &nbsp;</td>
                </tr>
            </table>
        <div style = "padding-top:5px;">
            <table style="width:100%;">
                <tr>
                    <td colspan="2">
                        <dx:ASPxLabel ID="LCurPie" runat="server" Text="">
                        </dx:ASPxLabel>
            <dx:WebChartControl ID="WebChartControl2" runat="server" Height="600px" 
                LoadingPanelText="Загрузка&amp;hellip;" Width="600px">
                <diagramserializable>
                    <dx:SimpleDiagram>
                    </dx:SimpleDiagram>
                </diagramserializable>
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

                <seriesserializable>
                    <dx:Series Name="Ряд 1">
                        <viewserializable>
                            <dx:PieSeriesView RuntimeExploding="False">
                            </dx:PieSeriesView>
                        </viewserializable>
                        <labelserializable>
                            <dx:PieSeriesLabel LineVisible="True">
                                <fillstyle>
                                    <optionsserializable>
                                        <dx:SolidFillOptions />
                                    </optionsserializable>
                                </fillstyle>
                                <pointoptionsserializable>
                                    <dx:PiePointOptions>
                                    </dx:PiePointOptions>
                                </pointoptionsserializable>
                            </dx:PieSeriesLabel>
                        </labelserializable>
                        <legendpointoptionsserializable>
                            <dx:PiePointOptions>
                            </dx:PiePointOptions>
                        </legendpointoptionsserializable>
                    </dx:Series>
                </seriesserializable>
                <seriestemplate>
                    <viewserializable>
                        <dx:PieSeriesView RuntimeExploding="False">
                        </dx:PieSeriesView>
                    </viewserializable>
                    <labelserializable>
                        <dx:PieSeriesLabel LineVisible="True">
                            <fillstyle>
                                <optionsserializable>
                                    <dx:SolidFillOptions />
                                </optionsserializable>
                            </fillstyle>
                            <pointoptionsserializable>
                                <dx:PiePointOptions>
                                </dx:PiePointOptions>
                            </pointoptionsserializable>
                        </dx:PieSeriesLabel>
                    </labelserializable>
                    <legendpointoptionsserializable>
                        <dx:PiePointOptions>
                        </dx:PiePointOptions>
                    </legendpointoptionsserializable>
                </seriestemplate>

<CrosshairOptions><CommonLabelPositionSerializable>
<dx:CrosshairMousePosition></dx:CrosshairMousePosition>
</CommonLabelPositionSerializable>
</CrosshairOptions>

<ToolTipOptions><ToolTipPositionSerializable>
<dx:ToolTipMousePosition></dx:ToolTipMousePosition>
</ToolTipPositionSerializable>
</ToolTipOptions>
            </dx:WebChartControl>
                    </td>
                    <td>
                        <dx:ASPxLabel ID="LComparePie" runat="server" Text="">
                        </dx:ASPxLabel>
                        <dx:WebChartControl ID="WebChartControl3" runat="server" Height="600px" 
                            Visible="False" Width="600px">
                        </dx:WebChartControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <dx:ASPxLabel ID="ASPxLabel9" runat="server" Text="Сравнить с исследованием:">
                        </dx:ASPxLabel>
            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" ValueType="System.String" 
                Width="200px">
            </dx:ASPxComboBox>
            <dx:ASPxButton ID="ASPxButton1" runat="server" onclick="ASPxButton1_Click" 
                Text="Сравнить" Width="200px">
            </dx:ASPxButton>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        </div>
    </div>
</asp:Content>
