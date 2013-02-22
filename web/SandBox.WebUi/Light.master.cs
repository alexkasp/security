using System;
using System.Diagnostics;

namespace SandBox.WebUi 
{
    public partial class LightMaster : System.Web.UI.MasterPage 
    {
        protected void Page_Load(object sender, EventArgs e) 
        {
        }

        public void DisableMenu()
        {
            ((RootMaster)Master).MenuVisible = false;
        }
    }

    
}