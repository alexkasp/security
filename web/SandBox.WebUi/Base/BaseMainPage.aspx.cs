using System;
using System.Diagnostics;
using System.Web.UI;
using SandBox.Connection;
using SandBox.Db;

namespace SandBox.WebUi.Base
{
    public partial class BaseMainPage : Page
    {
        protected Int32 UserId;
        private static ConnectionClientEx _client;

        protected void Page_Load(object sender, EventArgs e)
        {
            var user = UserManager.GetUser(User.Identity.Name);
            if (user.ProviderUserKey != null) UserId = (Int32)user.ProviderUserKey;
            _client = ConnectionClientEx.Instance;
        }

        protected String PageMenu
        {
            set { if (Master != null) ((MainMaster)Master).SetMenuFile(value); }
        }

        protected String PageTitle
        {
            set { Title = Application.Get("ApplicationTitle") + " | " + value; }
        }

        protected String RootPath
        {
            get { return Request.Path.Substring(0, Request.Path.LastIndexOf("/")); }
        }

        protected static void SendPacket(byte[] data)
        {
            _client.Send(data);
        }

        protected static void SendPacket(Packet packet)
        {
            _client.Send(Packet.ToByteArray(packet));
        }

        protected Boolean IsUserInRole(String role)
        {
            return User.IsInRole(role);
        }
    }//end SandBoxPage
}//end namespace