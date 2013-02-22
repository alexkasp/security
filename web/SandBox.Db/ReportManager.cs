using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace SandBox.Db
{
    public class ReportManager : DbManager
    {

        public static long GetFsEventsCountForRsch(int rschId)
        {
            return GetEventsOfModuleCuntForRsch(1, rschId);
        }

        public static long GetRegEventsCountForRsch(int rschId)
        {
            return GetEventsOfModuleCuntForRsch(2, rschId);
        }

        public static long GetProcEventsCountForRsch(int rschId)
        {
            return GetEventsOfModuleCuntForRsch(3, rschId);
        }

        public static long GetNetEventsCountForRsch(int rschId)
        {
            return (GetEventsOfModuleCuntForRsch(4, rschId)+GetEventsOfModuleCuntForRsch(5, rschId));
        }


        public static long GetEventsOfModuleCuntForRsch(int moduleId, int rschId)
        {
            using (var db = new SandBoxDataContext())
            {
                Research r = db.Researches.FirstOrDefault<Research>(x => x.Id == rschId);
                if (r != null)
                    switch (r.State)
                    {
                        case (int)ResearchState.COMPLETED:
                            {
                                return db.events.Where<events>(x => (x.rschId == rschId) && (x.module == moduleId)).Count();
                            }
                        default:
                            {
                                Vm v = db.Vms.First<Vm>(x => x.Id == r.VmId);
                                return db.events.Where<events>(x => (x.rschId == v.EnvId) && (x.module == moduleId)).Count();
                            }
                    }
                return -1;
            }
        }

        public static string GetVnUsage()
        {
            using (var db = new SandBoxDataContext())
            {
                var row = db.vmusages.FirstOrDefault<vmusage>(x => true);
                return String.Format("{0}%", Math.Round(row.usage));
            }
        }

        public static string GetFsSatas()
        {
            using (var db = new SandBoxDataContext())
            {
                var row = db.vpodirs.FirstOrDefault<vpodir>(x => true);
                return String.Format("{0}%", Math.Round(row.Fs));
            }
        }

        public static string GetNetStats()
        {
            using (var db = new SandBoxDataContext())
            {
                var row = db.vpodirs.FirstOrDefault<vpodir>(x => true);
                return String.Format("{0}%", Math.Round(row.Net));
            }
        }

        public static string GetProcsStats()
        {
            using (var db = new SandBoxDataContext())
            {
                var row = db.vpodirs.FirstOrDefault<vpodir>(x => true);
                return String.Format("{0}%", Math.Round(row.Procs));
            }
        }

        public static string GetRegStats()
        {
            using (var db = new SandBoxDataContext())
            {
                var row = db.vpodirs.FirstOrDefault<vpodir>(x => true);
                return String.Format("{0}%", Math.Round(row.Reg));
            }
        }


        public static IQueryable<string> GetModules()
        {
            var db = new SandBoxDataContext();
            return from m in db.EventsModulesDescriptions
                   select m.Description;
        }

        public static IQueryable<string> GetEventsDescrByModule(string moduleDesctiption)
        {
            var db = new SandBoxDataContext();
            int modId = -1;
            var mod = db.EventsModulesDescriptions.FirstOrDefault<EventsModulesDescriptions>(x => x.Description == moduleDesctiption);
            if (mod != null)
            {
                modId = mod.EventModuleID;
            }
            if (modId != -1)
            {
                return from ev in db.ModulesVsEvents
                          where ev.Module == modId
                          select ResearchManager. GetEvtEvtDescription( ev.Event);
            }
            return null;

        }

        public static IQueryable GetRowDirectoriesOfEvents()
        {
            var db = new SandBoxDataContext();
            return from dOfE in db.DirectoryOfEvents
                   select new { Id = dOfE.Id, eventt = ResearchManager.GetEvtEvtDescription(dOfE.@event), module = ResearchManager.GetEvtModuleDescription(dOfE.module), significance =GetSignifStringValue(dOfE.significance), dest = dOfE.dest, Who = dOfE.who };
        }

        public static void DeleteDirectorysOfEvent(long id)
        {
            var db = new SandBoxDataContext();
            var dofe = db.DirectoryOfEvents.FirstOrDefault<DirectoryOfEvents>(x => x.Id == id);
            if(dofe!=null) db.DirectoryOfEvents.DeleteOnSubmit(dofe);
            db.SubmitChanges();
        }

        internal static string GetSignifStringValue(int s)
        {
            switch (s)
            {
                case 0:
                    {
                        return "Критически важное";
                    }
                case 1:
                    {
                        return "Важное";
                    }
                default:
                    {
                        return String.Format("Вес события {0}", s);
                    }
            }
        }


        public static void InsertStopEvent(int rschId,int m, int evt, string d, string wh)
        {
            StopEvents se = new StopEvents()
                {
                    dest = d,
                    module = m,
                    @event = evt,
                    who = wh,
                    rschId = rschId
                };
            var db = new SandBoxDataContext();
            db.StopEvents.InsertOnSubmit(se);
            db.SubmitChanges();
        }

        public static void InsertRowDirectoriesOfEvents(int segn, int m, int evt, string d, string wh)
        {
            DirectoryOfEvents dofe = new DirectoryOfEvents
            {
                significance = segn,
                module = m,
                @event = evt,
                dest = d, 
                who = wh
            };
            var db = new SandBoxDataContext();
            db.DirectoryOfEvents.InsertOnSubmit(dofe);
            db.SubmitChanges();
        }

        public static int GetEvtSignif(events evt)
        {
            var db = new SandBoxDataContext();
            var dofe = db.DirectoryOfEvents.FirstOrDefault<DirectoryOfEvents>(x => ((x.dest == evt.dest) && (x.module == evt.module) 
                                                            && (x.@event == evt.@event)&&(x.who==evt.who)));
            return dofe == null ? -1 : dofe.significance;
        }

        public static int GetEvtSignif(events evt, List<DirectoryOfEvents> data)
        {
            foreach(var d in data)
            {
                if((evt.dest==d.dest) && (evt.module == d.module) && (evt.@event == d.@event) && (evt.who == d.who))
                    return d.significance;
            }
            return -1;
        }


        public static IQueryable GetProcesses(Int32 researchId, bool rschIDTransform = false)
        {
            var db = new SandBoxDataContext();
            if (rschIDTransform)
            {
                Research r = db.Researches.FirstOrDefault<Research>(x => x.Id == researchId);              
                if (r.State != (int)ResearchState.COMPLETED)
                {
                    int envId = db.Vms.First<Vm>(x => x.Id == r.VmId).EnvId;
                    researchId = envId;
                }
            }
            var processes = from p in db.Procs
                            where p.RschId == researchId
                            select new { rschID = p.RschId, procName = p.Name, pid1 = p.Pid1, pid2 = p.Pid2, streamsCount = p.Count };
            return processes;
        }

        public static IQueryable<Procs> GetRowProcesses(Int32 researchId, bool rschIDTransform = false)
        {
            var db = new SandBoxDataContext();
            if (rschIDTransform)
            {
                Research r = db.Researches.FirstOrDefault<Research>(x => x.Id == researchId);
                if (r.State != (int)ResearchState.COMPLETED)
                {
                    int envId = db.Vms.First<Vm>(x => x.Id == r.VmId).EnvId;
                    researchId = envId;
                }
            }
            var processes = from p in db.Procs
                            where p.RschId == researchId
                            select p;
            return processes;
        }

        public static IQueryable GetProcesses2(Int32 researchId)
        {
            var db = new SandBoxDataContext();
            var processes = from p in db.Procs
                            where p.RschId == researchId
                            select new { rschID = p.RschId, procName = p.Name, pid1 = p.Pid1, pid2 = p.Pid2, streamsCount = p.Count };
            return processes;
        }

        public static IQueryable GetRegs(Int32 researchId, bool rschIDTransform = false)
        {

            var db = new SandBoxDataContext();
            if (rschIDTransform)
            {
                Research r = db.Researches.FirstOrDefault<Research>(x => x.Id == researchId);           
                if (r.State != (int)ResearchState.COMPLETED)
                {                 
                    int envId = db.Vms.First<Vm>(x => x.Id == r.VmId).EnvId;
                    researchId = envId;
                }
            }
            var regs = from r in db.Regs
                       where r.RschID == researchId
                       select new { KeyName = r.KeyName, Parent = r.KeyName/*, EventAdditionalInfo = r.EventAdditionalInfo */};
            return regs;
        }

        public static IQueryable<Regs> GetRowRegs(Int32 researchId, bool rschIDTransform = false)
        {

            var db = new SandBoxDataContext();
            if (rschIDTransform)
            {
                Research r = db.Researches.FirstOrDefault<Research>(x => x.Id == researchId);
                if (r.State != (int)ResearchState.COMPLETED)
                {
                    int envId = db.Vms.First<Vm>(x => x.Id == r.VmId).EnvId;
                    researchId = envId;
                }
            }
            var regs = from r in db.Regs
                       where r.RschID == researchId
                       select r;
            return regs;
        }

        public static IQueryable GetRegs2(Int32 researchId)
        {
            var db = new SandBoxDataContext();
            var regs = from r in db.Regs
                       where r.RschID == researchId
                       select new { KeyName = r.KeyName, Parent = r.KeyName/*, EventAdditionalInfo = r.EventAdditionalInfo */};
            return regs;
        }

        public static IQueryable GetFiles(Int32 researchId, bool rschIDTransform = false)
        {
            var db = new SandBoxDataContext();
            if (rschIDTransform)
            {
                Research r = db.Researches.FirstOrDefault<Research>(x => x.Id == researchId); 
                if (r.State != (int)ResearchState.COMPLETED)
                {
                    
                    int envId = db.Vms.First<Vm>(x => x.Id == r.VmId).EnvId;
                    researchId = envId;
                }
            }
            var files = from f in db.Files
                        where f.RschId == researchId
                        select new { Name = f.Name, IsDir = IsDirToStr(f.IsDir), EventAdditionalInfo = f.EventAdditionalInfo};
            return files;
        }

        public static IQueryable GetFiles2(Int32 researchId)
        {
            var db = new SandBoxDataContext();
            var files = from f in db.Files
                        where f.RschId == researchId
                        select new { Name = f.Name, IsDir = IsDirToStr(f.IsDir), EventAdditionalInfo = f.EventAdditionalInfo };
            return files;
        }

        private static string IsDirToStr(bool p)
        {
            if (p) return "директория";
            return "файл";
        }

        public static IQueryable<Report> GetReports()
        {
            var db = new SandBoxDataContext();

            var reports = from r in db.Reports
                           select r;
            return reports;
        }

        public static IQueryable<Report> GetReports(Int32 researchId)
        {
            var db = new SandBoxDataContext();

            var reports = from r in db.Reports
                          where r.ResearchId == researchId
                          select r;
            return reports;
        }

        public static void AddReport(Int32 researchId, Int32 modId, Int32 actionId, String obj, String target, String additional="")
        {            
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Report report = new Report { ResearchId = researchId, ModuleId = modId, ActionId = actionId, Object = obj, Target = target, TIme = DateTime.Now, Additional = additional};
                db.Reports.InsertOnSubmit(report);
                db.SubmitChanges();
            }
        }

        public static void ClearReports(Int32 researchId)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var reports = from r in db.Reports
                              where r.ResearchId == researchId
                                  select r;

                db.Reports.DeleteAllOnSubmit(reports);
                db.SubmitChanges();
            }
        }
    }//end Reports class
}//end namespace
