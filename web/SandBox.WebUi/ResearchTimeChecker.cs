using System;
using System.Linq;
using System.Text;
using System.Threading;
using SandBox.Connection;
using SandBox.Db;
using SandBox.Log;
using SandBox.WebUi.Pages.Information;

namespace SandBox.WebUi
{
    public class ResearchTimeChecker: IDisposable
    {
        private Timer                       _threadingTimer;
        private static ConnectionClientEx   _client;

        public ResearchTimeChecker(ConnectionClientEx client)
        {
            _client = client;
            _threadingTimer = new Timer(Check, null, 0, 1000);
        }

        public void StartCheck()
        {
            _threadingTimer.Change(0, 1000);
            MLogger.LogTo(Level.TRACE, false, "Checker thread started.");
        }

        public void StopCheck()
        {
            _threadingTimer.Change(Timeout.Infinite, Timeout.Infinite);
            MLogger.LogTo(Level.TRACE, false, "Checker thread stopped.");
        }
        
        public void Dispose()
        {
            StopCheck();
            _threadingTimer.Dispose();
            _threadingTimer = null;
        }

        private static void Check(object obj)
        {
            IQueryable<Research> researches = ResearchManager.GetResearches();

            foreach (var research in researches)
            {
                Int32 duration       = research.Duration;
                DateTime startTime   = DateTime.Now;
                DateTime stopTime    = DateTime.Now;
                DateTime currentTime = DateTime.Now;

                if (research.StartedDate.HasValue) //Сессия запущена
                {
                    startTime = research.StartedDate.Value;

                    if (research.StoppedDate.HasValue) //Сессия остановлена
                    {
                        stopTime = research.StoppedDate.Value;
                    }
                    else //Сессия выполняется
                    {
                        Int32 secondsElapsed = (Int32) (currentTime - startTime).TotalSeconds; //Прошло секунд с начала сессии
                        if (secondsElapsed > duration*60)
                        {
                            //Время сессии вышло, завершаем её
                            //**********************************
                            ResearchManager.UpdateResearchState(research.Id, ResearchState.COMPLETING);

                            //Останаливаем виртуалку
                            String machineName = VmManager.GetVmName(research.VmId);
                            if(machineName!=null)
                            {
                                Resources.StopVm(research.VmId);
                              
                            }
                           

                            ResearchManager.UpdateResearchStopTime(research.Id);
                            ResearchManager.UpdateResearchState(research.Id, ResearchState.COMPLETED);
                            //**********************************
                            //Обновление таблицы [dbo].[events]
                            int res = ResearchManager.UpdateEnents(research.Id);
                        }
                    }
                }
            }
        }// end Check function


    }//end class
}//end namesapce