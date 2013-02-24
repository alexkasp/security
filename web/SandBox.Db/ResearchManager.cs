using System;
using System.Collections.Generic;
using System.Linq;

namespace SandBox.Db
{
    public enum ResearchState
    {
        READY       = 0,
        STARTING    = 1,
        EXECUTING   = 2,
        COMPLETING  = 3,
        COMPLETED   = 4
    }
    public class EventChartItem
    {
        public string Sign;
        public string Module;
        public int Value;
    }

    public enum TrafficFileReady
    {
        NOACTION  = 0,
        EXECUTING = 1,
        COMPLETE  = 2
    }
    
    public class ResearchManager : DbManager
    {

        public static void InsertToStopRschSatus(int rschId, string value)
        {
            var db = new SandBoxDataContext();
            db.StopRschStatus.InsertOnSubmit(new StopRschStatus() { rschId = rschId, stopStatus = value });
            db.SubmitChanges();
        }

        public static StopEvents GetStopEventForRsch(/*IQueryable<events> evrnts,*/ int rschId)
        {
            var db = new SandBoxDataContext();
            return db.StopEvents.FirstOrDefault<StopEvents>(x => x.rschId == rschId);
            
        }

        public static PortList GetPortList(long id)
        {
            var db = new SandBoxDataContext();
            return db.PortList.FirstOrDefault<PortList>(x => x.Id == id);
        }


        public static IQueryable GetPortsViewForRsch(int rschId)
        {
            var db = new SandBoxDataContext();
            Research r = db.Researches.FirstOrDefault<Research>(x => x.Id == rschId);
            if (r != null)
                switch (r.State)
                {
                    case (int)ResearchState.COMPLETED:
                        {
                            return GetPortsViewWitoutLogic(rschId);
                        }
                    default:
                        {
                            Vm v = db.Vms.First<Vm>(x => x.Id == r.VmId);
                            return GetPortsViewWitoutLogic(v.EnvId);
                        }
                }
            return null; 
        }

        private static IQueryable GetPortsViewWitoutLogic(int p)
        {
            var db = new SandBoxDataContext();
            return from port in db.PortList
                   where port.rschID == p
                   orderby port.Id descending
                   select new 
                   { 
                       Id = port.Id, 
                       destination = port.destination, 
                       status = port.status, 
                       type = port.type, 
                       port = port.port 
                   };
        }

        public static Research GetRunnigResearchByVmID(int vmId)
        {
            var db = new SandBoxDataContext();
            return db.Researches.FirstOrDefault<Research>(x => (x.VmId == vmId) && (x.State == (int)ResearchState.EXECUTING));
        }

        public static int GetModuleIdByDescr(string descr)
        {
            var db = new SandBoxDataContext();
            var moddescr = db.EventsModulesDescriptions.FirstOrDefault<EventsModulesDescriptions>(x=>x.Description==descr);
            int code;
            Int32.TryParse(descr, out code);
            return moddescr == null ? code : moddescr.EventModuleID;
        }

        public static events GetEventById (long id)
        {
            var db = new SandBoxDataContext();
            return db.events.FirstOrDefault<events>(x => x.Id == id);
        }

        public static int GetEventIdByDescr(string descr)
        {
            var db = new SandBoxDataContext();
            var evt = db.EventsEventDescriptions.FirstOrDefault<EventsEventDescriptions>(x => x.EventsEventDescription == descr);
            int code;
            Int32.TryParse(descr, out code);
            return evt == null ? code : evt.EventID;
        }

        /// <summary>
        /// Возвращает представление для тыблицы событий исследования, учитывая состояние текущего ислледования
        /// </summary>
        /// <param name="rschId">идентификатор исследования</param>
        /// <returns>представление результатов из таблицы events</returns>
        public static IQueryable GetEventsTableViewByRschId(int rschId, bool dscnding = false)
        {
            var db = new SandBoxDataContext();
            Research r = db.Researches.FirstOrDefault<Research>(x => x.Id == rschId);
            if (r != null)
                switch (r.State)
                {
                    case (int)ResearchState.COMPLETED:
                        {
                            return GetEventsTableViewWithoutLogicByRschId(rschId, dscnding);
                        }
                    default:
                        {
                            Vm v = db.Vms.First<Vm>(x => x.Id == r.VmId);
                            return GetEventsTableViewWithoutLogicByRschId(v.EnvId, dscnding);
                        }
                }
            return null;
        }


        public static IQueryable<events> GetEventsByRschId(int rschId)
        {
            var db = new SandBoxDataContext();
            Research r = db.Researches.FirstOrDefault<Research>(x => x.Id == rschId);
            if (r != null)
                switch (r.State)
                {
                    case (int)ResearchState.COMPLETED:
                        {
                            return GetEventsWithoutLogicByRschId(rschId);
                        }
                    default:
                        {
                            Vm v = db.Vms.First<Vm>(x => x.Id == r.VmId);
                            return GetEventsWithoutLogicByRschId(v.EnvId);
                        }
                }
            return null;
        }

        public static IQueryable GetEventsViewWithSignByRschId(int rschId)
        {
            var db = new SandBoxDataContext();
            Research r = db.Researches.FirstOrDefault<Research>(x => x.Id == rschId);
            if (r != null)
                switch (r.State)
                {
                    case (int)ResearchState.COMPLETED:
                        {
                            return GetEventsViewWithSignWithoutLogic(rschId);
                        }
                    default:
                        {
                            Vm v = db.Vms.First<Vm>(x => x.Id == r.VmId);
                            return GetEventsViewWithSignWithoutLogic(v.EnvId);
                        }
                }
            return null;
        }
        public static List<EventChartItem> GetEventsChartView(int rschId)
        {
            List<EventChartItem> res = new List<EventChartItem>();
            var db = new SandBoxDataContext();
            Research r = db.Researches.FirstOrDefault<Research>(x => x.Id == rschId);
            if (r != null)
                switch (r.State)
                {
                    case (int)ResearchState.COMPLETED:
                        {
                            return GetChartCount(rschId);
                        }
                    default:
                        {
                            Vm v = db.Vms.First<Vm>(x => x.Id == r.VmId);
                            return GetChartCount(v.EnvId);
                        }
                }
            
            return null;
            //return res;
        }

        public static List<EventChartItem> GetChartCount(int rschId)
        {
            List<EventChartItem> res = new List<EventChartItem>();
            var db = new SandBoxDataContext();
            List<DirectoryOfEvents> dof = new List<DirectoryOfEvents>();
            var rdof = from d in db.DirectoryOfEvents
                       select d;
            dof = rdof.ToList<DirectoryOfEvents>();
            //var es = db.events.AsQueryable<events>().Select(x=>(x.rschId == rschId) && ((ReportManager.GetEvtSignif(x) == 0) || (ReportManager.GetEvtSignif(x) == 1)));
            var es = from evts in db.events
                     where evts.rschId == rschId //&& ((ReportManager.GetEvtSignif(evts) == 0) || (ReportManager.GetEvtSignif(evts) == 1))
                     select evts;
            List<events> ass = new List<events>();
            foreach (var e in es)
            {
                if (((ReportManager.GetEvtSignif(e, dof) == 0) || (ReportManager.GetEvtSignif(e,dof) == 1)))
                {
                    ass.Add(e);
                    
                }
            }
                foreach(var e in ass)
                {
                    res.Add(new EventChartItem() { Module = GetEvtModuleDescription(e.module), Sign = ReportManager.GetSignifStringValue(ReportManager.GetEvtSignif(e)), Value = 1 });
                }
            //return from e in ass.AsQueryable()
            //       select new
            //       {
            //           Id = e.Id,
            //           ModuleId = GetEvtModuleDescription(e.module),
            //           EventCode = GetEvtEvtDescription(e.@event),
            //           Who = e.who,
            //           Dest = e.dest,
            //           Description = e.descr,
            //           RschId = e.rschId,
            //           Sign = ReportManager.GetSignifStringValue(ReportManager.GetEvtSignif(e))
            //       };
            return res;
            
        }

        private static IQueryable<events> GetEventsWithoutLogicByRschId(int rschId)
        {
            var db = new SandBoxDataContext();
            return from evts in db.events
                   where evts.rschId == rschId
                   orderby evts.Id
                   select evts;
        }

        public static IQueryable GetEventsViewWithSignWithoutLogic(int rschId)
        {
            var db = new SandBoxDataContext();
            //var es = db.events.AsQueryable<events>().Select(x=>(x.rschId == rschId) && ((ReportManager.GetEvtSignif(x) == 0) || (ReportManager.GetEvtSignif(x) == 1)));
            var es = from evts in db.events
                     where evts.rschId == rschId //&& ((ReportManager.GetEvtSignif(evts) == 0) || (ReportManager.GetEvtSignif(evts) == 1))
                     select evts;
            List<events> ass = new List<events>();
            foreach (var e in es)
            {
                if (((ReportManager.GetEvtSignif(e) == 0) || (ReportManager.GetEvtSignif(e) == 1)))
                {
                    ass.Add(e);
                }
            }
            return from e in ass.AsQueryable()
                   select new 
                   {
                       Id = e.Id,
                       ModuleId = GetEvtModuleDescription(e.module), 
                       EventCode = GetEvtEvtDescription(e.@event), 
                       Who = e.who, 
                       Dest = e.dest, 
                       Description = e.descr, 
                       RschId = e.rschId, 
                       Sign = ReportManager.GetSignifStringValue(ReportManager.GetEvtSignif(e)) 
                   };
        }
        

        public static IQueryable GetSearchEventsView(string searchtxt)
        {
            var db = new SandBoxDataContext();
            return from evts in db.EventsTableViews
                   where evts.who.Contains(searchtxt)
                   orderby evts.rschId
                   select new { evts.Id, evts.ModuleId, evts.EventCode, evts.who, evts.dest, evts.Description, evts.rschId, pid = evts.pid1, evts.pid2, evts.adddata1, evts.adddata2, evts.timeofevent };

        }

        /// <summary>
        /// Возвращает ередставление для тыблицы событий исследования
        /// </summary>
        /// <param name="rschId">идентификатор исследования</param>
        /// <returns>представление результатов из таблицы events</returns>
        public static IQueryable GetEventsTableViewWithoutLogicByRschId(int rschId, bool dscnding = false)
        {
            var db = new SandBoxDataContext();
            if(!dscnding)
            return from evts in db.events
                   where evts.rschId == rschId
                   select new { Id = evts.Id, ModuleId = GetEvtModuleDescription(evts.module), EventCode = GetEvtEvtDescription(evts.@event), Who = evts.who, Dest = evts.dest, Description = evts.descr, RschId = evts.rschId };
            return from evts in db.events
                   where evts.rschId == rschId
                   orderby evts.Id descending
                   select new { Id = evts.Id, ModuleId = GetEvtModuleDescription(evts.module), EventCode = GetEvtEvtDescription(evts.@event), Who = evts.who, Dest = evts.dest, Description = evts.descr, RschId = evts.rschId };

        }

        public static IQueryable GetEventsForRsch(int rschId)
        {
            var db = new SandBoxDataContext();
            var rsch = GetResearch(rschId);
            switch (rsch.State)
            {
                case (int)ResearchState.EXECUTING:
                    {
                        return GetEventsTableViewWithoutLogicByRschId2(rschId, true);
                    }
                default:
                    {
                        return GetEventsTableViewWithoutLogicByRschId2(rschId);
                    }

            }

        }



        public static IQueryable GetEventsTableViewWithoutLogicByRschId2(int rschId, bool dscnding = false)
        {
            var db = new SandBoxDataContext();
            if (!dscnding)
                return from evts in db.EventsTableViews
                       where evts.rschId == rschId
                       orderby evts.Id
                       select new { evts.Id, evts.ModuleId, evts.EventCode, evts.who, evts.dest, evts.Description, evts.rschId, pid = evts.pid1, evts.pid2, evts.adddata1, evts.adddata2, evts.timeofevent };
            return from evts in db.EventsTableViews
                   where evts.rschId == rschId
                   orderby evts.Id descending
                   select new { evts.Id, evts.ModuleId, evts.EventCode, evts.who, evts.dest, evts.Description, evts.rschId, pid = evts.pid1, evts.pid2, evts.adddata1, evts.adddata2, evts.timeofevent };
//                return from evts in db.events
//                       where evts.rschId == rschId
//                       select new { Id = evts.Id, ModuleId = GetEvtModuleDescription(evts.module), EventCode = GetEvtEvtDescription(evts.@event), Who = evts.who, Dest = evts.dest, Description = evts.descr, RschId = evts.rschId, pid = evts.pid1, pid2 = evts.pid2, adddata1 = evts.adddata1, adddata2 = evts.adddata2, timeOfEvent = evts.timeofevent };
//            return from evts in db.events
//
//                   where evts.rschId == rschId
//                   orderby evts.Id descending
//                   select new { Id = evts.Id, ModuleId = GetEvtModuleDescription(evts.module), EventCode = GetEvtEvtDescription(evts.@event), Who = evts.who, Dest = evts.dest, Description = evts.descr, RschId = evts.rschId, pid = evts.pid1, pid2 = evts.pid2, adddata1 = evts.adddata1, adddata2 = evts.adddata2, timeOfEvent = evts.timeofevent };

        }

        public static string GetEvtModuleDescription(int moduleId)
        { 
            var db = new SandBoxDataContext();
            var rowDescr = db.EventsModulesDescriptions.FirstOrDefault<EventsModulesDescriptions>(x => x.EventModuleID == moduleId);
            if (rowDescr != null)
            {
                if (rowDescr.Description != String.Empty)
                {
                    return rowDescr.Description;
                }
            }
            return moduleId.ToString();
        }

        public static string GetEvtEvtDescription(int evtId)
        {
            var db = new SandBoxDataContext();
            var rowDescr = db.EventsEventDescriptions.FirstOrDefault<EventsEventDescriptions>(x => x.EventID == evtId);
            if (rowDescr != null)
            {
                if (rowDescr.EventsEventDescription != String.Empty)
                {
                    return rowDescr.EventsEventDescription;
                }
            }
            return evtId.ToString();
        }

        /// <summary>
        /// Обновляет таблицу dbo.events 
        /// </summary>
        /// <param name="rschId">идентификатор исследования</param>
        /// <returns>идентификатор среды, котрый был заменен</returns>
    public static int UpdateEnents(int rschId)
        {
            int envId = -1;
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Research r = db.Researches.First<Research>(x => x.Id == rschId);
                Vm v = db.Vms.First<Vm>(x => x.Id == r.VmId);
                envId = v.EnvId;
                var evts = from ev in db.events
                            where ev.rschId == envId
                            select ev;
                var procs = from p in db.Procs
                            where p.RschId == envId
                            select p;
               
                var files = from f in db.Files
                            where f.RschId == envId
                            select f;
                var regs = from reg in db.Regs
                           where reg.RschID == envId
                           select reg;
                var ports = from port in db.PortList
                            where port.rschID == envId
                            select port;
                foreach (var prt in ports)
                {
                    prt.rschID = rschId;
                }
                foreach (var reg in regs)
                {
                    reg.RschID = rschId;
                }

                foreach (var f in files)
                {
                    f.RschId = rschId;
                }

                foreach (var p in procs)
                {
                    p.RschId = rschId;
                }

                foreach (SandBox.Db.events e in evts)
                {
                    e.rschId = rschId;
                }
              
                db.SubmitChanges();
                return envId; 
            }     
        }

        

        /// <summary>
        /// Вставка события для мониторинга
        /// </summary>
        /// <param name="rschId"></param>
        /// <param name="fClass"></param>
        /// <param name="sClass"></param>
        /// <param name="value"></param>
        /// <param name="Comments"></param>
        public static void InsertMonEvent(int rschId, string fClass, string sClass, string value, string Comments = "")
        {
            var db = new SandBoxDataContext();
            SandBox.Db.Monitor mon = new SandBox.Db.Monitor();
            mon.FClass = fClass;
            mon.SClass = sClass;
            mon.RschId = rschId;
            mon.Value = value;
            mon.Comments = Comments;
            try
            {
                Object thisLock = new Object();
                lock (thisLock)
                {
                    mon.Id = (from m in db.Monitor select m.Id).Max() + 1;
                }
            }
            catch
            {
                mon.Id = 1;
            }
            db.Monitor.InsertOnSubmit(mon);
            db.SubmitChanges();
        }

        /// <summary>
        /// Удаления события для мониторинга
        /// </summary>
        /// <param name="Id"></param>
        public static void DleteMonEvent(int researchId)
        {
            SandBoxDataContext db = new SandBoxDataContext();
            Monitor r = db.Monitor.FirstOrDefault(x => x.Id == researchId);
            if (r == null) return;
            db.Monitor.DeleteOnSubmit(r);
            db.SubmitChanges();
        }

        private static string ConverBoolToStatus(bool any)
        {
            if (any)
                return "Правило сработало";
            else
                return "Нет срабатываний";
        }
        public static IQueryable GetMonitoringEvents(int rschId)
        {
            var db = new SandBoxDataContext();
            var allevents = from re in db.RschEvents
                            where re.RschId == rschId
                            select re;
            return from rm in db.Monitor
                            where rm.RschId == rschId
                            select new { Id = rm.Id, FClass = rm.FClass, SClass = rm.SClass, Value = rm.Value, Status = ConverBoolToStatus(allevents.Any<RschEvents>(x => x.Value == rm.Value)) };
        }

        public static Dictionary<string, int> GetResearchesCountFofOS()
        {
            var db = new SandBoxDataContext();
            Dictionary<string, int> res = new Dictionary<string, int>();
            var researches = from rs in db.Researches
                             select rs;
            foreach (var r in researches)
            {
                var vm = VmManager.GetVm(r.VmId);
                if (vm != null)
                {
                    var system = db.VmSystems.FirstOrDefault<VmSystem>(x => x.System == vm.System);
                    if (system != null)
                    {
                        if (res.Keys.Contains(system.Description))
                        {
                            res[system.Description]++;
                        }
                        else
                        {
                            res.Add(system.Description, 1);
                        }
                    }
                }
 
            }
            return res;
        }

        public static string GetRschOS(int id)
        {
            var db = new SandBoxDataContext();
            string os = "Нет данных";
            var rsch = GetResearch(id);
            if (rsch == null) return os;
            Vm vm = db.Vms.FirstOrDefault<Vm>(x => x.Id == rsch.VmId);
            if (vm == null) return os;
            VmSystem vms = db.VmSystems.FirstOrDefault<VmSystem>(x => x.System == vm.System);
            if (vms == null) return os;
            os = vms.Description;
            return os;
        }

        public static Dictionary<string, int> GetBadCountForOS()
        {
            var db = new SandBoxDataContext();
            Dictionary<string, int> res = new Dictionary<string, int>();
            var researches = from rs in db.Researches
                             select rs;
            foreach (var r in researches)
            {
                var vm = VmManager.GetVm(r.VmId);
                if (vm != null)
                {
                    var system = db.VmSystems.FirstOrDefault<VmSystem>(x => x.System == vm.System);
                    if (system != null)
                    {
                        int toAdd = 0;
                        var evnts = from ev in db.events
                                    where ev.rschId == r.Id
                                    select ev;
                        foreach (var ev in evnts)
                        {
                            if (ReportManager.GetEvtSignif(ev) == 0 || ReportManager.GetEvtSignif(ev) == 1)
                            {
                                toAdd++;
                            }
                        }
                        if (res.Keys.Contains(system.Description))
                        {
                            res[system.Description]+=toAdd;
                        }
                        else
                        {
                            res.Add(system.Description, toAdd);
                        }
                    }
                }
            }
            return res;
        }


        public static IQueryable<Research> GetReadyResearches()
        {
            var db = new SandBoxDataContext();

            var researches = from r in db.Researches
                             where r.State == (int)ResearchState.COMPLETED
                             orderby r.Id
                             select r;
            return researches;
        }

        public static IQueryable<Research> GetExecutingResearches()
        {
            var db = new SandBoxDataContext();
            var researches = from r in db.Researches
                             where r.State ==  (int)ResearchState.EXECUTING
                             select r;
            return researches;
        }

        //**********************************************************
        //* Получение всех элементов Research
        //**********************************************************
        public static IQueryable<Research> GetResearches()
        {
            var db = new SandBoxDataContext();

            var researches = from r in db.Researches
                      orderby r.Id
                      select r;
            return researches;
        }

        //**********************************************************
        //* Получение всех элементов Research для пользователя userId
        //**********************************************************
        public static IQueryable<Research> GetResearches(Int32 userId)
        {
            var db = new SandBoxDataContext();

            var researches = from r in db.Researches
                              where r.UserId == userId
                               orderby r.Id
                                select r;
            return researches;
        }

        //**********************************************************
        //* Получение исследования
        //**********************************************************
        public static Research GetResearch(Int32 researchId)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                return db.Researches.FirstOrDefault(x => x.Id == researchId);
            }
        }


        //**********************************************************
        //* Получение исследования
        //**********************************************************
        public static Research GetResearch(Int32 userId, String researchName)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var research = from r in db.Researches
                               where r.UserId == userId
                               where r.ResearchName == researchName
                               orderby r.Id
                               select r;
                return research.First();
            }
        }

        //**********************************************************
        //* Получение исследования по VmId
        //**********************************************************
        public static Research GetResearchByVmId(Int32 vmId)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var research = from r in db.Researches
                               where r.VmId == vmId
                               where r.State != (Int32)ResearchState.COMPLETED
                               select r;
                return research.First();
            }
        }

        //**********************************************************
        //* Получение всех имен Research для пользователя с userId
        //**********************************************************
        public static List<String> GetResearchNameList(Int32 userId)
        {
            var db = new SandBoxDataContext();

            var researches = from r in db.Researches
                             where r.UserId == userId
                             orderby r.Id
                             select r.ResearchName;
            return researches.ToList();
        }

        //**********************************************************
        //* Получение всех имен проведенных Research для пользователя с userId
        //**********************************************************
        public static List<String> GetReadyResearchNameList(Int32 userId)
        {
            var db = new SandBoxDataContext();

            var researches = from r in db.Researches
                             where r.UserId == userId
                             where r.State == (Int32)ResearchState.COMPLETED
                             orderby r.Id
                             select r.ResearchName;
            return researches.ToList();
        }

        //**********************************************************
        //* Получение всех имен проведенных Research для пользователя с userId
        //**********************************************************
        public static Research GetResearchVmDataIdByVmData(String vmIp, DateTime startTime)
        {
            var db = new SandBoxDataContext();

            var researches = from r in db.Researches
                              join rvd in db.ResearchesVmDatas on r.ResearchVmData equals rvd.Id
                                where r.StartedDate.HasValue
                                where r.StartedDate.Value == startTime
                                where rvd.VmEnvIp == vmIp
                              orderby r.Id
                             select r;
            return researches.First();
        }

        //**********************************************************
        //* Получение всех элементов Research для отображения
        //**********************************************************
        public static IQueryable GetResearchesTableView()
        {
            var db = new SandBoxDataContext();

            var results = from rs in db.ResearchesTableViews
                          select
                              new
                                  {
                                      rs.Id,
                                      rs.User,
                                      rs.Malware,
                                      rs.VmType,
                                      rs.VmSystem,
                                      rs.State,
                                      rs.CreatedDate,
                                      rs.StartedDate,
                                      rs.StoppedDate,
                                      rs.ResearchName,
                                      TimeElapsed = GetElapsedTimeInMinutes(rs.StartedDate, rs.StoppedDate, rs.Duration),
                                      TimeLeft = GetLeftTimeInMinutes(rs.StartedDate, rs.Duration),
                                      rs.fsEventsCount,
                                      rs.regEventsCount,
                                      rs.netEventsCount,
                                      rs.procEventsCount
                                  };
            return results;
        }

        //private static long GetFsEventsCount(int rschId)
        //{
        //    var db = new SandBoxDataContext();
        //    return db.Files.Where<Files>(x => x.RschId == rschId).Count();
        //}

        //private static long GetRegEventsCount(int rschId)
        //{
        //    var db = new SandBoxDataContext();
        //    return db.Regs.Where<Regs>(x => x.RschID == rschId).Count();
        //}

        //private static long GetNetEventsCount(int rschId)
        //{
        //    var db = new SandBoxDataContext();
        //    return db.po.Where<Regs>(x => x.RschID == rschId).Count();
        //}


        //**********************************************************
        //* Получение всех элементов Research для отображения
        //**********************************************************
        public static IQueryable GetResearchesTableView(Int32 userId)
        {
            var db = new SandBoxDataContext();

            var researches = from r in db.Researches
                             join u in db.Users on r.UserId equals u.UserId
                             join m in db.Mlwrs on r.MlwrId equals m.Id
                             join v in db.Vms on r.VmId equals v.Id
                             join s in db.ResearchesStates on r.State equals s.State
                             orderby r.Id
                             where r.UserId == userId
                             select new { r.Id, User = u.Login, Malware = m.Path, VmType = v.Type, VmSystem = v.System, State = s.Description, r.CreatedDate, r.StartedDate, r.StoppedDate, r.ResearchName, r.Duration, r.TrafficFileReady, r.TrafficFileName };

            var results = from rs in researches
                          join vs in db.VmSystems on rs.VmSystem equals vs.System
                          join vt in db.VmTypes on rs.VmType equals vt.Type
                          orderby rs.Id
                          select
                              new
                              {
                                  rs.Id,
                                  rs.User,
                                  rs.Malware,
                                  VmType = vt.Description,
                                  VmSystem = vs.Description,
                                  rs.State,
                                  rs.CreatedDate,
                                  rs.StartedDate,
                                  rs.StoppedDate,
                                  rs.ResearchName,
                                  TimeElapsed = GetElapsedTimeInMinutes(rs.StartedDate, rs.StoppedDate, rs.Duration),
                                  TimeLeft = GetLeftTimeInMinutes(rs.StartedDate, rs.Duration)
                              };
            return results;
        }

        //**********************************************************
        //* Добавление нового исследования, возвращает researchId
        //**********************************************************
        public static Int32 AddResearch(Int32 userId, Int32 mlwrId, Int32 vmId, Int32 researchVmData, Int32 duration, String name = "")
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Research research = new Research
                                        {
                                            UserId = userId,
                                            MlwrId = mlwrId,
                                            VmId = vmId,
                                            ResearchVmData = researchVmData,
                                            State = (Int32)ResearchState.READY,
                                            CreatedDate = DateTime.Now,
                                            Duration = duration,
                                            ResearchName = name,
                                            TrafficFileReady = (Int32)TrafficFileReady.NOACTION
                                        };
                db.Researches.InsertOnSubmit(research);
                db.SubmitChanges();

                var researches = from r in db.Researches
                                 where r.UserId == userId
                                 select r.Id;

                return researches.Max();
            }
        }


        //**********************************************************
        //* Удаление исследования
        //**********************************************************
        public static void DeleteResearch(Int32 researchId)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Research res = db.Researches.FirstOrDefault(x => x.Id == researchId);
                if (res == null) return;

                DeleteResearchVmData(res.ResearchVmData); // Удаляем данные о Vm
                
                db.Researches.DeleteOnSubmit(res);
                db.SubmitChanges();
            }
            TaskManager.DeleteTasks(researchId);    //Удаляем все задачи, связанные с исследованием
            ReportManager.ClearReports(researchId); //Удаляем все отчеты, связанные с исследованием
            RequestManager.ClearRequests(researchId); //Удаляем все запросы, связанные с исследованием
            
        }

        //**********************************************************
        //* Обновление времени старта исследования
        //**********************************************************
        public static void UpdateResearchStartTime(Int32 researchId)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Research research =  db.Researches.FirstOrDefault(x => x.Id == researchId);
                if (research == null) return;
                research.StartedDate = DateTime.Now;
                db.SubmitChanges();
            }
        }

        //**********************************************************
        //* Обновление времени окончания исследования
        //**********************************************************
        public static void UpdateResearchStopTime(Int32 researchId)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Research research = db.Researches.FirstOrDefault(x => x.Id == researchId);
                if (research == null) return;
                research.StoppedDate = DateTime.Now;
                db.SubmitChanges();
            }
        }

        //**********************************************************
        //* Обновление информации о траффике
        //**********************************************************
        public static void UpdateTrafficInfo(Int32 researchId, TrafficFileReady fileReady, String fileName=null)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Research research = db.Researches.FirstOrDefault(x => x.Id == researchId);
                if (research == null) return;
                research.TrafficFileReady = (Int32)fileReady;
                if (fileName != null) research.TrafficFileName = fileName;
                db.SubmitChanges();
            }
        }

        //**********************************************************
        //* Обновление состояния исследования
        //**********************************************************
        public static void UpdateResearchState(Int32 researchId, ResearchState researchState)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Research research = db.Researches.FirstOrDefault(x => x.Id == researchId);
                if (research == null) return;
                research.State = (Int32)researchState;
                db.SubmitChanges();
            }
        }

        public static void UpdateResearchVmData(Int32 researchVmId,String vmEnvMac, String vmEnvIp)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                ResearchesVmData researchVm = db.ResearchesVmDatas.FirstOrDefault(x => x.Id == researchVmId);
                if (researchVm == null) return;
                researchVm.VmEnvIp = vmEnvIp;
                researchVm.VmEnvMac = vmEnvMac;
                db.SubmitChanges();
            }
        }

        //**********************************************************
        //* Получение данных о Vm
        //**********************************************************
        public static ResearchesVmData GetResearchVmData(Int32 vmDataId)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                return db.ResearchesVmDatas.FirstOrDefault(x => x.Id == vmDataId);
            }
        }

        //Получение исследования по имени виртуалки

        public static Research GetResearchByVmName(String VmName)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var research = from r in db.Researches
                               join datas in db.ResearchesVmDatas on r.ResearchVmData equals datas.Id
                               where datas.VmName == VmName
                               orderby r.Id descending
                               select r;
                return research.First();
            }
        }
        //**********************************************************
        //* Добавление данных о Vm, возвращает dataId
        //**********************************************************
        public static Int32 AddResearchVmData(String vmName, Int32 vmType, Int32 vmSystem, Int32 vmEnvType, String vmEnvMac, String vmEnvIp, String vmDescription = "")
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                ResearchesVmData researchesVmData = new ResearchesVmData
                {
                    VmName = vmName,
                    VmType = vmType,
                    VmSystem = vmSystem,
                    VmEnvType = vmEnvType,
                    VmEnvMac = vmEnvMac,
                    VmEnvIp = vmEnvIp,
                    VmDescription = vmDescription
                };
                db.ResearchesVmDatas.InsertOnSubmit(researchesVmData);
                db.SubmitChanges();

                var rd = from r in db.ResearchesVmDatas
                                 where r.VmName == vmName
                                 select r.Id;

                return rd.Max();
            }
        }

        //**********************************************************
        //* Удаление данных о Vm
        //**********************************************************
        public static void DeleteResearchVmData(Int32 researchVmDataId)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                ResearchesVmData res = db.ResearchesVmDatas.FirstOrDefault(x => x.Id == researchVmDataId);
                if (res == null) return;
                db.ResearchesVmDatas.DeleteOnSubmit(res);
                db.SubmitChanges();
            }
        }


        //**********************************************************
        //* Получение исследования по файлотраффику
        //**********************************************************
        public static Research GetResearchByTrafficFileName(String trafficFileName)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                return db.Researches.FirstOrDefault(x => x.TrafficFileName == trafficFileName);
            }
        }


        public static String GetElapsedTimeInMinutes(DateTime? startTime, DateTime? stopTime, Int32 duration)
        {
            String result = "нет данных";

            if (startTime.HasValue) //Сессия начата
            {
                if (stopTime.HasValue) //Сессия завершена
                {
                    if ((Int32)((stopTime.Value - startTime.Value).TotalMinutes) >= duration)
                    {
                        result = duration + " мин.";
                    }
                    else
                    {
                        result = (Int32)((stopTime.Value - startTime.Value).TotalMinutes) + " мин.";
                    }
                }
                else //Сессия не завершена
                {
                    if ((Int32)((DateTime.Now - startTime.Value).TotalMinutes) >= duration)
                    {
                        result = duration + " мин.";
                    }
                    else
                    {
                        result = (Int32)((DateTime.Now - startTime.Value).TotalMinutes) + " мин.";
                    }
                }
            }
            return result;
        }

        public static String GetLeftTimeInMinutes(DateTime? startTime, Int32 duration)
        {
            String result;

            if (startTime.HasValue) //Сессия начата
            {
                Int32 elapsedMins = (Int32)((DateTime.Now - startTime.Value).TotalMinutes);
                Int32 leftMins = duration - elapsedMins;
                
                if (leftMins <= 0)
                {
                    result = "время вышло";
                }
                else
                {
                    result = leftMins + " мин.";
                }
            }
            else
            {
                result = duration + " мин.";
            }
            return result;
        }
    }//end ResearchManager class
}//end namespace
