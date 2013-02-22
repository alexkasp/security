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
using System.Web.UI;
using System.Collections.Generic;
using DevExpress.XtraCharts;

namespace SandBox.WebUi.Pages.Research
{
    public partial class EventsReport : BaseMainPage
    {
        public Db.Research Rs;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Распределение событий и классификация";
            PageMenu = "~/App_Data/SideMenu/Research/ResearchMenu.xml";
            Int32 researchId = Convert.ToInt32(Request.QueryString["research"]);
            Rs = ResearchManager.GetResearch(researchId);
            if (Rs == null)
            {
                Response.Redirect("~/Error");
            }
            ASPxLabel1.Text = String.Format("Исследование (№{0}): {1}", Rs.Id,Rs.ResearchName);
            string Os = ResearchManager.GetRschOS(Rs.Id);
            if (Os != null && Os != String.Empty)
                ASPxLabel2.Text = String.Format("ОС: {0}", Os);
            else ASPxLabel2.Visible = false;
            if (!IsPostBack)
            {
               
            }
            gridViewReports.DataSource = ResearchManager.GetEventsViewWithSignByRschId(Rs.Id);
            gridViewReports.DataBind();
            var test = ResearchManager.GetEventsChartView(Rs.Id);
            List<string> serNames = new List<string>();
            Dictionary<string, List<EventChartItem>> dict = new Dictionary<string, List<EventChartItem>>();
            foreach (var t in test)
            {
                if (!serNames.Contains(t.Sign))
                {
                    serNames.Add(t.Sign);
                }
                if (dict.ContainsKey(t.Sign))
                {
                    if (dict[t.Sign].Count == 0 || dict[t.Sign] == null)
                    {
                        dict[t.Sign] = new List<EventChartItem>();
                        EventChartItem item = new EventChartItem() { Module = t.Module, Value = 1, Sign = t.Sign };
                    }
                    else
                    {
                        bool plased = false;
                        foreach (var p in dict[t.Sign])
                        {
                            if (p.Module == t.Module)
                            {
                                p.Value++;
                                plased = true;
                                break;
                            }
                        }
                        if (!plased)
                        {
                            dict[t.Sign].Add(new EventChartItem() { Module = t.Module, Value = 1, Sign = t.Sign });
                        }
                    }
                }
                else
                {
                    var i = new List<EventChartItem>() { new EventChartItem() { Module = t.Module, Sign = t.Sign, Value = 1 } };
                    dict.Add(t.Sign, i);
                }
            }
           
            //foreach (var t in test)
            //{
            //    if (dict.ContainsKey(t.Sign))
            //    {
            //        if (dict[t.Sign].Count == 0 || dict[t.Sign] == null)
            //        {
            //            dict[t.Sign] = new List<EventChartItem>();
            //            EventChartItem item = new EventChartItem() { Module = t.Module, Value = 1, Sign = t.Sign };
            //        }
            //        else
            //        {
            //            bool plased = false;
            //            foreach (var p in dict[t.Sign])
            //            {
            //                if (p.Module == t.Module)
            //                {
            //                    p.Value++;
            //                    plased = true;
            //                    break;
            //                }
            //            }
            //            if (!plased)
            //            {
            //                dict[t.Sign].Add(new EventChartItem() { Module = t.Module, Value = 1, Sign = t.Sign });
            //            }
            //        }
            //    }
            //    else
            //    {
            //        var  i = new List<EventChartItem>(){ new EventChartItem(){ Module = t.Module, Sign = t.Sign, Value = 1}};
            //        dict.Add(t.Sign, i);
            //    }
            //}
            WebChartControl1.Series.Clear();
            foreach (var k in dict.Keys)
            {
                Series series1 = new Series(k, DevExpress.XtraCharts.ViewType.Bar);
                foreach(var p in dict[k])
                    series1.Points.Add(new SeriesPoint(p.Module, p.Value));
                WebChartControl1.Series.Add(series1);
                //.Series.Add(series1);
                series1.Label.PointOptions.PointView = PointView.Values;
                series1.Label.PointOptions.ValueNumericOptions.Format = NumericFormat.Number;
            }


            //WebChartControl1
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            MlwrManager.InsertComment(Rs.MlwrId, ASPxTextBox1.Text);
        }
    }
}