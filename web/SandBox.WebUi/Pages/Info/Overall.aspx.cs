using System;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Info
{
    public partial class Overall : BaseLightPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Overall";
            //PageMenu  = "~/App_Data/SideMenu/Information/InformationMenu.xml";
        }
    }
}