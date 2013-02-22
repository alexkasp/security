using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SandBox.WebUi
{
    public partial class Research : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null) ((MainMaster)Master).SetMenuFile("~/App_Data/SideMenu/Research/ResearchMenu.xml");
            Response.Redirect("~/Pages/Research/Current.aspx");
        }
    }
}