using System;
using SandBox.WebUi.Base;

namespace SandBox.WebUi 
{
    public partial class _Default : BaseEmptyPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = Application.Get("ApplicationTitle").ToString();
            Response.Redirect("~/Pages/Information/Index.aspx");
            //Response.Redirect("~/Pages/Index.aspx");
        }
    }//end class
}//end namespace