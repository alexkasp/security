using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts;

namespace SandBox.WebUi
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.WebChartControl1.Series.Clear();
            //this.WebChartControl1.Series.Add(new DevExpress.XtraCharts.Series("OS1", DevExpress.XtraCharts.ViewType.Bar));
            //this.WebChartControl1.Series.Add(new DevExpress.XtraCharts.Series("OS2", DevExpress.XtraCharts.ViewType.Bar));
            //this.WebChartControl1.Series.Add(new DevExpress.XtraCharts.Series("OS3", DevExpress.XtraCharts.ViewType.Bar));
            Series series1 = new Series("Очет об испытаниях OS1 ", DevExpress.XtraCharts.ViewType.Bar);
            Series series2 = new Series("Очет об испытаниях OS2", DevExpress.XtraCharts.ViewType.Bar);
            Series series3 = new Series("Очет об испытаниях OS3", DevExpress.XtraCharts.ViewType.Bar);
            series1.Points.Add(new SeriesPoint("OS1", 80));
            series2.Points.Add(new SeriesPoint("OS2", 20));
            series3.Points.Add(new SeriesPoint("OS3", 40));

            this.WebChartControl1.Series.AddRange(new Series[] { series1, series2, series3});


        }
    }
}