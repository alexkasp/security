using System;
using System.Diagnostics;

namespace SandBox.WebUi {
    public partial class MainMaster : System.Web.UI.MasterPage 
    {
        protected void Page_Load(object sender, EventArgs e) 
        {
        }

        public void SetMenuFile(String menu)
        {
            //XmlDataSourceLeft.DataFile = menu;
            //XmlDataSourceLeft.DataBind();
        }
    }
}