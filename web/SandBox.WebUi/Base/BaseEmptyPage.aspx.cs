using System;
using System.Web.UI;
using SandBox.Connection;

namespace SandBox.WebUi.Base
{
    public partial class BaseEmptyPage : Page
    {
        private static ConnectionClientEx _client;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _client = ConnectionClientEx.Instance;
            }
        }

        protected String PageTitle
        {
            set { Title = Application.Get("ApplicationTitle") + " | " + value; }
        }

        protected Boolean Communication
        {
            get { return _client.IsConnected; }
        }

        protected static void SendPacket(byte[] data)
        {
            _client.Send(data);
        }
    }//end SandBoxPage
}//end namespace