using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Db;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;

namespace SandBox.WebUi.Pages.Research
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void gridSearchView_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters == "ApplyExtFilter")
            {
                if (!gridSearchView.ClientVisible) gridSearchView.ClientVisible = true;
                gridSearchView.FilterExpression = filter.FilterExpression;
                gridSearchView.DataBind();
            }
            if (e.Parameters == "ApplyFilter")
            {
                if (!gridSearchView.ClientVisible) gridSearchView.ClientVisible = true;
                if (SearchTextBox.Text == "") { gridSearchView.FilterExpression = ""; }
                else { gridSearchView.FilterExpression = string.Format("contains([who],'{0}') or contains([dest],'{0}')", SearchTextBox.Text); }
                gridSearchView.DataBind();
            }
            if (!gridSearchViewPager.Visible) gridSearchViewPager.Visible = true;
            gridSearchViewPager.ItemCount = gridSearchView.VisibleRowCount;
            gridSearchViewPager.ItemsPerPage = gridSearchView.SettingsPager.PageSize;
            gridSearchViewPager.PageIndex = gridSearchView.PageIndex;
        }

        protected void gridSearchViewPager_PageIndexChanged(object sender, EventArgs e)
        {
            gridSearchView.PageIndex = gridSearchViewPager.PageIndex;
            gridSearchView.DataBind();
        }

        protected void gridSearchViewPager_PageSizeChanged(object sender, EventArgs e)
        {
            gridSearchView.SettingsPager.PageSize = gridSearchViewPager.ItemsPerPage;
            gridSearchView.DataBind();
        }
    }
}