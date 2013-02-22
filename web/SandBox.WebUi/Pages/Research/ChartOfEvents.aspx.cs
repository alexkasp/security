using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using DevExpress.Web.ASPxEditors;
using SandBox.Connection;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Base;
using System.Collections;
using System.Collections.Generic;
using DevExpress.XtraCharts;

namespace SandBox.WebUi.Pages.Research
{
    public partial class ChartOfEvents : BaseMainPage
    {
        private Dictionary<string, int> DEventsCount = new Dictionary<string, int>();
        private Dictionary<string, int> DEventsCountCompare = new Dictionary<string, int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Отчет по событиям";
            PageMenu = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            ASPxLabel1.Text = "Исследование: " + ResearchManager.GetResearch((int)Session["rsch"]).ResearchName;
            LCurPie.Text = ASPxLabel1.Text;
            //DEventsCountCompare.Clear();
            DEventsCount.Clear();
            UpdateEventChart(0);
            ASPxLabel7.Text = String.Format("{0}", ResearchManager.GetResearch((int)Session["rsch"]).Id);
            ASPxLabel8.Text = String.Format("{0}",ResearchManager.GetRschOS((int)Session["rsch"]));
            var rschs = ResearchManager.GetReadyResearches();
            if (!IsPostBack)
            {
                ReportsBuilder.RschPropsListBuilder(TreeView2, ResearchManager.GetResearch((int)Session["rsch"]).Id);
                ASPxLabel4.Text = ResearchManager.GetResearch((int)Session["rsch"]).ResearchName;
                foreach (var r in rschs)
                {
                    ASPxComboBox1.Items.Add(new ListEditItem(r.ResearchName, r.Id));
                }
                
            }
            UpdatePieView();
        }

        private void UpdateEventChart( int yOfset = 0, int rsch = -1)
        {
            int r = rsch == -1 ? (int)Session["rsch"] : rsch;
            int virtualTime = 0;
            var evts = ResearchManager.GetEventsByRschId(r);
            foreach (var evt in evts)
            {
                int evtSignif = ReportManager.GetEvtSignif(evt);
                AddEventToChart(virtualTime, evt, evtSignif, yOfset);
                virtualTime++;
            }
        }

        private void UpdatePieView()
        {
            WebChartControl2.Series.Clear();
            Series series = new Series("Распределение важных и критически важных событий", ViewType.Pie);
            series.Label.PointOptions.PointView = PointView.ArgumentAndValues;
            series.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
            series.Label.PointOptions.ValueNumericOptions.Precision = 0;
            foreach (var k in DEventsCount.Keys)
            {
                series.Points.Add(new SeriesPoint(k, DEventsCount[k]));
            }
            WebChartControl2.Series.Add(series);
            if (DEventsCountCompare.Count > 0)
            {
                WebChartControl3.Visible = true;
                WebChartControl3.Series.Clear();
                Series series2 = new Series("Распределение важных и критически важных событий", ViewType.Pie);
                series2.Label.PointOptions.PointView = PointView.ArgumentAndValues;
                series2.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Percent;
                series2.Label.PointOptions.ValueNumericOptions.Precision = 0;
                foreach (var k in DEventsCountCompare.Keys)
                {
                    series2.Points.Add(new SeriesPoint(k, DEventsCountCompare[k]));
                }
                WebChartControl3.Series.Add(series2);
            }
            else 
            {
                WebChartControl3.Visible = false;
            }
        }

        private void AddEventToChart(int virtualTime, events evt, int evtSignif, int yOfset)
        {
            bool isSessioForThisEvent = false;
            int i = 0;
            string moduleDescr = ResearchManager.GetEvtModuleDescription(evt.module);
            int startValue = yOfset+GetOfsetForEvent(evtSignif);
            if (evtSignif == 0 || evtSignif == 1)
            {
                if (yOfset == 0)
                {
                    if (DEventsCount.Keys.Contains(moduleDescr))
                    {
                        DEventsCount[moduleDescr]++;
                    }
                    else
                    {
                        DEventsCount.Add(moduleDescr, 1);
                    }
                }
                else
                {
                    if (DEventsCountCompare.Keys.Contains(moduleDescr))
                    {
                        DEventsCountCompare[moduleDescr]++;
                    }
                    else
                    {
                        DEventsCountCompare.Add(moduleDescr, 1);
                    }
 
                }
            }
            for (i = 0; i < WebChartControl1.Series.Count; i++ )
            {
                if (WebChartControl1.Series[i].Name == moduleDescr)
                {
                    WebChartControl1.Series[i].Points.Add(new DevExpress.XtraCharts.SeriesPoint(virtualTime,new double[]{startValue, startValue+1}));
                    isSessioForThisEvent = true;
                    //заполнение словаря
                    break;
                }
            }
            if (!isSessioForThisEvent)
            {
                var s = new DevExpress.XtraCharts.Series(moduleDescr, DevExpress.XtraCharts.ViewType.SideBySideRangeBar);
                s.Points.Add(new DevExpress.XtraCharts.SeriesPoint(virtualTime, new double[] { startValue, startValue + 1 }));
                WebChartControl1.Series.Add(s);//SideBySideRangeBarSeriesView
            }
        }

        private int GetOfsetForEvent(int evtSignif)
        {
 	        switch(evtSignif)
            {
                case 0: 
                    {
                        return 2;
                    }
                case 1:
                    {
                        return 1;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            WebChartControl1.Series.Clear();
            UpdateEventChart(0);
            int rschId;
            if (ASPxComboBox1.SelectedItem.Text != "")
            {
                ASPxLabel3.Text = ASPxComboBox1.SelectedItem.Text;
                LComparePie.Text = ASPxComboBox1.SelectedItem.Text;
            }
            Int32.TryParse((string)ASPxComboBox1.SelectedItem.Value, out rschId);
            UpdateEventChart(4, rschId);
            UpdatePieView();
        }

        //protected void WebChartControl1_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        //{
            
        //}
    }
}