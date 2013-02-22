using System;
using System.Text.RegularExpressions;
using NLog;
using NLog.Targets;

namespace SandBox.Log
{
    public class GuiEventArgs : EventArgs
    {
        public Level Level;
        public String Message;
        public GuiEventArgs(Level level, String message)
        {
            Level = level;
            Message = message;
        }
    }//end GuiEventArgs

    public enum Level
    {
        INFO,
        TRACE,
        DEBUG,
        WARNING,
        ERROR,
        FATAL
    }

    public class MLogger
    {
        public delegate void GuiEventHandler(GuiEventArgs args);
        public static event GuiEventHandler OnLogToGui;

        private static readonly Logger NLogger = LogManager.GetCurrentClassLogger();
        
        private static void InvokeLogToGui(GuiEventArgs args)
        {
            GuiEventHandler handler = OnLogToGui;
            if (handler != null) handler(args);
        }

        public static void LogTo(Level level, Boolean fireEvent, String rawMessage)
        {
            String message = Regex.Replace(rawMessage, "cardno=[^&]*", "****************");
            
            switch (level)
            {
                case Level.INFO:
                     NLogger.Info(message);
                    break;
                case Level.TRACE:
                    NLogger.Trace(message);
                    break;
                case Level.DEBUG:
                     NLogger.Debug(message);
                    break;
                case Level.WARNING:
                     NLogger.Warn(message);
                    break;
                case Level.ERROR:
                    NLogger.Error(message);
                    break;
                case Level.FATAL:
                    NLogger.Fatal(message);
                    break;
            }

            if (fireEvent)
            {
                InvokeLogToGui(new GuiEventArgs(level, message));
            }
        }

        public static void LogTo(Object sender, Level level, Boolean fireEvent, String rawMessage)
        {
            String message = "[" + sender.GetType().Name + "] " + Regex.Replace(rawMessage, "cardno=[^&]*", "****************");

            switch (level)
            {
                case Level.INFO:
                    NLogger.Info(message);
                    break;
                case Level.TRACE:
                    NLogger.Trace(message);
                    break;
                case Level.DEBUG:
                    NLogger.Debug(message);
                    break;
                case Level.WARNING:
                    NLogger.Warn(message);
                    break;
                case Level.ERROR:
                    NLogger.Error(message);
                    break;
                case Level.FATAL:
                    NLogger.Fatal(message);
                    break;
            }

            if (fireEvent)
            {
                InvokeLogToGui(new GuiEventArgs(level, message));
            }
        }

        public static void SetLogFile(String filePath)
        {
            var currentConfig = LogManager.Configuration;

            var trg = (FileTarget)currentConfig.FindTargetByName("logFile");
            String tmpPath = trg.FileName.ToString();

            try
            {
                String currentPath = tmpPath.Substring(1, tmpPath.LastIndexOf(@"\"));
                String fileName = tmpPath.Substring(1 + tmpPath.LastIndexOf(@"\"), tmpPath.Length - tmpPath.LastIndexOf(@"\") - 2);

                if (!currentPath.Equals(filePath))
                {
                    LogTo(Level.INFO, true, "Move log file to path: " + filePath);
                    
                    trg.FileName = filePath + fileName;
                    
                    
                    currentConfig.AddTarget("logFile", trg);                   
                    LogManager.Configuration = currentConfig;

                    LogTo(Level.INFO, false, "Log file moved from path: " + currentPath);
                }
            }
            catch (Exception)
            {
               LogTo(Level.WARNING, true, "Can't move log to path: " + filePath);
            } 
        }
    }//end class MLogger
}
