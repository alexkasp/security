<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="СonsolidatedReport.aspx.cs" Inherits="SandBox.WebUi.Pages.Information.СonsolidatedReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div style="padding:5px;">

    <dx:ASPxLabel ID="LUsageOfDifferentTypesOfLIR" runat="server" 
        Text="Статистика по использованию различных типов ЛИР" Theme="iOS">
    </dx:ASPxLabel>
    <br />
    <dx:WebChartControl ID="WCCUsageOfDifferentTypesOfLIR" runat="server" 
        Height="400px" LoadingPanelText="Загрузка&amp;hellip;" Width="800px">
        <diagramserializable>
            <dx:SimpleDiagram>
            </dx:SimpleDiagram>
        </diagramserializable>
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

        <seriesserializable>
            <dx:Series ArgumentScaleType="Numerical" Name="ЛИР">
                <points>
                    <dx:SeriesPoint ArgumentSerializable="1" SeriesPointID="0" Values="324324324">
                    </dx:SeriesPoint>
                    <dx:SeriesPoint ArgumentSerializable="2" SeriesPointID="2" Values="435435435">
                    </dx:SeriesPoint>
                </points>
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
    <br />
    <dx:ASPxLabel ID="ASPxLabel1" runat="server" 
        Text="Статистика по использованию ОС для проведения исследований " Theme="iOS">
    </dx:ASPxLabel>
    <br />
    <dx:WebChartControl ID="WCCUsageOSForResearch" runat="server" Height="400px" 
        Width="800px" LoadingPanelText="Загрузка&amp;hellip;">
        <diagramserializable>
            <dx:XYDiagram>
                <axisx visibleinpanesserializable="-1">
                    <range sidemarginsenabled="True" />
                </axisx>
                <axisy gridspacingauto="False" visibleinpanesserializable="-1">
                    <range sidemarginsenabled="True" />
                </axisy>
            </dx:XYDiagram>
        </diagramserializable>
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

        <legend visible="False"></legend>
        <seriesserializable>
            <dx:Series Name="Ряд 1">
                <viewserializable>
                    <dx:SideBySideBarSeriesView>
                    </dx:SideBySideBarSeriesView>
                </viewserializable>
                <labelserializable>
                    <dx:SideBySideBarSeriesLabel LineVisible="True">
                        <fillstyle>
                            <optionsserializable>
                                <dx:SolidFillOptions />
                            </optionsserializable>
                        </fillstyle>
                        <pointoptionsserializable>
                            <dx:PointOptions>
                            </dx:PointOptions>
                        </pointoptionsserializable>
                    </dx:SideBySideBarSeriesLabel>
                </labelserializable>
                <legendpointoptionsserializable>
                    <dx:PointOptions>
                    </dx:PointOptions>
                </legendpointoptionsserializable>
            </dx:Series>
            <dx:Series Name="Ряд 2">
                <viewserializable>
                    <dx:SideBySideBarSeriesView>
                    </dx:SideBySideBarSeriesView>
                </viewserializable>
                <labelserializable>
                    <dx:SideBySideBarSeriesLabel LineVisible="True">
                        <fillstyle>
                            <optionsserializable>
                                <dx:SolidFillOptions />
                            </optionsserializable>
                        </fillstyle>
                        <pointoptionsserializable>
                            <dx:PointOptions>
                            </dx:PointOptions>
                        </pointoptionsserializable>
                    </dx:SideBySideBarSeriesLabel>
                </labelserializable>
                <legendpointoptionsserializable>
                    <dx:PointOptions>
                    </dx:PointOptions>
                </legendpointoptionsserializable>
            </dx:Series>
        </seriesserializable>

<SeriesTemplate><ViewSerializable>
<dx:SideBySideBarSeriesView></dx:SideBySideBarSeriesView>
</ViewSerializable>
<LabelSerializable>
<dx:SideBySideBarSeriesLabel LineVisible="True">
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
<PointOptionsSerializable>
<dx:PointOptions></dx:PointOptions>
</PointOptionsSerializable>
</dx:SideBySideBarSeriesLabel>
</LabelSerializable>
<LegendPointOptionsSerializable>
<dx:PointOptions></dx:PointOptions>
</LegendPointOptionsSerializable>
</SeriesTemplate>

<CrosshairOptions><CommonLabelPositionSerializable>
<dx:CrosshairMousePosition></dx:CrosshairMousePosition>
</CommonLabelPositionSerializable>
</CrosshairOptions>

<ToolTipOptions><ToolTipPositionSerializable>
<dx:ToolTipMousePosition></dx:ToolTipMousePosition>
</ToolTipPositionSerializable>
</ToolTipOptions>
    </dx:WebChartControl>
    <br />
    <dx:ASPxLabel ID="LStatisticsHarmfulInterference" runat="server" 
        Text="Статистика ОС по количеству зафиксированных вредоносных воздействий" 
        Theme="iOS">
    </dx:ASPxLabel>
    <br />
    <dx:WebChartControl ID="WCCUsageOSForResearch0" runat="server" Height="400px" 
        Width="800px" LoadingPanelText="Загрузка&amp;hellip;">
        <diagramserializable>
            <dx:XYDiagram>
                <axisx visibleinpanesserializable="-1">
                    <range sidemarginsenabled="True" />
                </axisx>
                <axisy gridspacingauto="False" visibleinpanesserializable="-1">
                    <range sidemarginsenabled="True" />
                </axisy>
            </dx:XYDiagram>
        </diagramserializable>
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>

        <legend visible="False"></legend>
        <seriesserializable>
            <dx:Series Name="Ряд 1">
                <viewserializable>
                    <dx:SideBySideBarSeriesView>
                    </dx:SideBySideBarSeriesView>
                </viewserializable>
                <labelserializable>
                    <dx:SideBySideBarSeriesLabel LineVisible="True">
                        <fillstyle>
                            <optionsserializable>
                                <dx:SolidFillOptions />
                            </optionsserializable>
                        </fillstyle>
                        <pointoptionsserializable>
                            <dx:PointOptions>
                            </dx:PointOptions>
                        </pointoptionsserializable>
                    </dx:SideBySideBarSeriesLabel>
                </labelserializable>
                <legendpointoptionsserializable>
                    <dx:PointOptions>
                    </dx:PointOptions>
                </legendpointoptionsserializable>
            </dx:Series>
            <dx:Series Name="Ряд 2">
                <viewserializable>
                    <dx:SideBySideBarSeriesView>
                    </dx:SideBySideBarSeriesView>
                </viewserializable>
                <labelserializable>
                    <dx:SideBySideBarSeriesLabel LineVisible="True">
                        <fillstyle>
                            <optionsserializable>
                                <dx:SolidFillOptions />
                            </optionsserializable>
                        </fillstyle>
                        <pointoptionsserializable>
                            <dx:PointOptions>
                            </dx:PointOptions>
                        </pointoptionsserializable>
                    </dx:SideBySideBarSeriesLabel>
                </labelserializable>
                <legendpointoptionsserializable>
                    <dx:PointOptions>
                    </dx:PointOptions>
                </legendpointoptionsserializable>
            </dx:Series>
        </seriesserializable>

<SeriesTemplate><ViewSerializable>
<dx:SideBySideBarSeriesView></dx:SideBySideBarSeriesView>
</ViewSerializable>
<LabelSerializable>
<dx:SideBySideBarSeriesLabel LineVisible="True">
<FillStyle><OptionsSerializable>
<dx:SolidFillOptions></dx:SolidFillOptions>
</OptionsSerializable>
</FillStyle>
<PointOptionsSerializable>
<dx:PointOptions></dx:PointOptions>
</PointOptionsSerializable>
</dx:SideBySideBarSeriesLabel>
</LabelSerializable>
<LegendPointOptionsSerializable>
<dx:PointOptions></dx:PointOptions>
</LegendPointOptionsSerializable>
</SeriesTemplate>

<CrosshairOptions><CommonLabelPositionSerializable>
<dx:CrosshairMousePosition></dx:CrosshairMousePosition>
</CommonLabelPositionSerializable>
</CrosshairOptions>

<ToolTipOptions><ToolTipPositionSerializable>
<dx:ToolTipMousePosition></dx:ToolTipMousePosition>
</ToolTipPositionSerializable>
</ToolTipOptions>
    </dx:WebChartControl>
    <br />

</div>
</asp:Content>
