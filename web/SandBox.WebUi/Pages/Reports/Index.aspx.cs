using System;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Reports
{
    public partial class Index : BaseLightPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "Отчеты";
        }
    }
}