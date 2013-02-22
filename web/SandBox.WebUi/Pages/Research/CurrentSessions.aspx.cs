﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SandBox.WebUi.Pages.Research
{
    public partial class CurrentSessions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "*** | Текущие сессии";

            if (Master != null) ((MainMaster)Master).SetMenuFile("~/App_Data/SideMenu/Research/ResearchMenu.xml");
        }
    }
}