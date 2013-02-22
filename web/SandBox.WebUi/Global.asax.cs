using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using NLog;
using SandBox.Connection;
using SandBox.Data;
using SandBox.Db;
//using SandBox.Engine;
using SandBox.Log;


namespace SandBox.WebUi
{
    public class Global_asax : HttpApplication
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        
        private static ConnectionClientEx _client;
        private static Boolean            _communicationStatus;
        private static ConnectionSetting  _settings;
        //private static Worker             _worker;
        
        private static List<String>       _restrictions;
        private static ResearchTimeChecker _checker;
		        
            public static Boolean CommunicationStatus
		    {
                get { return _communicationStatus; }
		    }

		    public static Boolean DbStatus
		    {
                get { return DbManager.GetConnectionStatus(); }
		    }

			void Application_Start(object sender, EventArgs e) 
            {
                SandboxTheme.ThemesProviderEx.Register();
                Application.Add("ApplicationTitle", "web-console");
                MLogger.LogTo(Level.TRACE, false, "---------------------------------------------------------------");
                MLogger.LogTo(Level.TRACE, false, "Application " + Application.Get("ApplicationTitle") + "started");
                DevExpress.Web.ASPxClasses.ASPxWebControl.CallbackError += Application_Error;

                _restrictions   = new List<String> {"Content", "Scripts", "Error"};
                
			    if (DbManager.GetConnectionStatus())
                {
                    MLogger.LogTo(Level.TRACE, false, "Database connection: success");
                    _settings = ConnectionManager.LoadSettings();

                    _client = ConnectionClientEx.Instance;
                    _client.OnConnectionClientExEvent += OnConnectionClientEvent;
                    _client.Start(_settings.RemoteHost, _settings.RemotePort, _settings.Reconnect);

                    _checker = new ResearchTimeChecker(_client);
                    _checker.StartCheck();
			    }
                else
                {
                    MLogger.LogTo(Level.FATAL, false, "Database connection: error");
                }

			    /*_worker = Worker.Instance;
                _worker.OnWorkerEvent += OnWorkerEvent;
                if (_worker.DbStatus)
                {
                    MLogger.LogTo(Level.TRACE, false, "Database connection: success");
                    
                    //_checker = new ResearchTimeChecker(_client);
                    //_checker.StartCheck();
                }
                else
                {
                    MLogger.LogTo(Level.FATAL, false, "Database connection: error");
                }*/

                
			}

        /*private void OnWorkerEvent(WorkerEventArgs args)
        {
            switch (args.EventType)
            {
                case WorkerEventType.REMOTE_CONNECTION_ESTABLISHED:
                    break;
                case WorkerEventType.REMOTE_CONNECTION_LOST:
                    break;
            }
        }*/

        private void OnConnectionClientEvent(ConnectionClientEventArgs args)
		    {
                switch (args.EventType)
                {
                    case ConnectionClientEventType.CONNECTING:
                        MLogger.LogTo(Level.TRACE, false, (String)args.EventData);
                        break;
                    case ConnectionClientEventType.CONNECTED:
                        MLogger.LogTo(Level.TRACE, false, "Remote server connection: connected");
                         _communicationStatus = true;
                        break;
                    case ConnectionClientEventType.DISCONNECTED:
                        MLogger.LogTo(Level.TRACE, false, "Remote server connection: disconnected");
                        _communicationStatus = false;
                        break;
                    case ConnectionClientEventType.MESSAGE_RECEIVED:
                        MLogger.LogTo(Level.TRACE, false, "Srv ---> Web: " + DataUtils.ByteArrayToHexString((byte[])args.EventData));
                        PacketAnalyzer.AnalyzeReceived(Packet.ToPacket((byte[])args.EventData), _client);
                        break;
                    case ConnectionClientEventType.MESSAGE_SENDING:
                        MLogger.LogTo(Level.TRACE, false, "Web ---> Srv: " + DataUtils.ByteArrayToHexString((byte[])args.EventData));
                        break;
                }
            }

            void Application_End(object sender, EventArgs e)
            {
                if (_checker != null)
                    _checker.Dispose();
                
                if (_client != null)
                    _client.Dispose(); 

                /*if (_worker != null)
                    _worker.Dispose();*/

                MLogger.LogTo(Level.TRACE, false, "Web application stoped");
			}


            void Application_BeginRequest(object sender, EventArgs e)
            {
                Debug.Print("BeginRequest: " + HttpContext.Current.Request.Url);
            }


        void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                if (!CheckRestriction(HttpContext.Current.Request.Url.ToString()))
                {
                    HttpContext.Current.RewritePath("~/Account/Login.aspx");
                }
            }
        }


        void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
        }

        void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
        }


        void Application_Error(object sender, EventArgs e) 
        {
				// Code that runs when an unhandled error occurs
		}

		void Session_Start(object sender, EventArgs e) 
        {
            MLogger.LogTo(Level.TRACE, false, "New client session started...");
		}

		void Session_End(object sender, EventArgs e) 
        {
				// Code that runs when a session ends. 
				// Note: The Session_End event is raised only when the sessionstate mode
				// is set to InProc in the Web.config file. If session mode is set to StateServer 
				// or SQLServer, the event is not raised.
                MLogger.LogTo(Level.TRACE, false, "Session stopped");
		}

        private Boolean CheckRestriction(String url)
        {
            return _restrictions.Any(url.Contains);
        }
    }//end Global.asax class
	}//end namespace