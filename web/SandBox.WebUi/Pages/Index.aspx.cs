using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages
{
    public partial class Index : BaseLightPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Главная";
        }
    }//end class
}//end namespace