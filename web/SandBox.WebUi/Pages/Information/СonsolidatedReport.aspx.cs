using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using SandBox.Connection;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Base;
using DevExpress.XtraCharts;
using System.Collections.Generic;
using System.Collections;

namespace SandBox.WebUi.Pages.Information
{
    public partial class СonsolidatedReport : BaseMainPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Сводный отчет";
            PageMenu = "~/App_Data/SideMenu/Information/InformationMenu.xml";

            WCCUsageOfDifferentTypesOfLIR.Series.Clear();
            var tcount = VmManager.GetVmCountForeachType();
            Series series = new Series("Статистика по использованию различных типов ЛИР", ViewType.Pie);
            foreach (var k in tcount.Keys)
            {
                series.Points.Add(new SeriesPoint(k, tcount[k]));
            }
            WCCUsageOfDifferentTypesOfLIR.Series.Add(series);
            series.Label.PointOptions.PointView = PointView.ArgumentAndValues;
            series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            series.Label.PointOptions.ValueNumericOptions.Precision = 0;


            var resbyOs = ResearchManager. GetResearchesCountFofOS();
            var badForOs = ResearchManager.GetBadCountForOS();
            foreach (var k in badForOs.Keys)
            {
                Series series1 = new Series("Колличечтво вредоносных воздействий для "+k, DevExpress.XtraCharts.ViewType.Bar);
                series1.Points.Add(new SeriesPoint(k, badForOs[k]));
                WCCUsageOSForResearch0.Series.Add(series1);
                series1.Label.PointOptions.PointView = PointView.Values;
                series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
            }

            //List<Series> rschCountSeries = new List<Series>();
           
            foreach (var k in resbyOs.Keys)
            {
                Series series1 = new Series("Колличечтво испытаний для "+k, DevExpress.XtraCharts.ViewType.Bar);
                series1.Points.Add(new SeriesPoint(k, resbyOs[k]));
                WCCUsageOSForResearch.Series.Add(series1);
                series1.Label.PointOptions.PointView = PointView.Values;
                series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
            }


        }
    }
}