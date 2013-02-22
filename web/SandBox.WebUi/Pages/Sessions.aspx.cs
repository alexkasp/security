using System;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages
{
    public partial class Sessions : BaseLightPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Сессии";
        }
    }
}