using System;
using SandBox.WebUi.Base;

namespace SandBox.WebUi.Pages.Error
{
    public partial class CommunicationError : BaseEmptyPage
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            PageTitle = "ошибка";
        }
    }
}