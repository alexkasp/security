using System;
using System.Web.UI;
using SandBox.Connection;
using SandBox.Db;

namespace SandBox.WebUi.Base
{
    public partial class BaseLightPage : Page
    {
        protected Int32 UserId;
        private static ConnectionClientEx _client;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _client = ConnectionClientEx.Instance;
                var user = UserManager.GetUser(User.Identity.Name);
                if (user != null)
                {
                    if (user.ProviderUserKey != null) UserId = (Int32)user.ProviderUserKey;
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx");
                }   
            }
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