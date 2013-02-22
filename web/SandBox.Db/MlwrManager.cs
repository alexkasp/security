using System;
using System.Collections.Generic;
using System.Linq;

namespace SandBox.Db
{

    public class MlwrManager : DbManager
    {

        public static string GetStrMlwrClassification(int mlwrId)
        {
            String res = "Не классифицировано";
            SandBoxDataContext db = new SandBoxDataContext();
          
            string prrRes = (from clmlwr in db.MlwrCls
                          join d in db.ClMlwrs on clmlwr.ClMlwrId equals d.Id
                          where clmlwr.MlwrId == mlwrId
                          select d.Description).FirstOrDefault<string>();
            if (prrRes != null)
            {
                res = prrRes;
            }
            return res;
        }

        public static MlwrReport GetNetReportById(int mlwrReportId)
        {
            SandBoxDataContext db = new SandBoxDataContext();
            return db.MlwrReport.FirstOrDefault<MlwrReport>(x => x.Id == mlwrReportId);
        }

        public static MlwrReport GetNetReport(int mlwrId)
        {
            SandBoxDataContext db = new SandBoxDataContext();
            return db.MlwrReport.FirstOrDefault<MlwrReport>(x => x.mlwrId == mlwrId);
        }

        public static IQueryable GetNetRep(int mlwrId)
        {
            SandBoxDataContext db = new SandBoxDataContext();
            return from ml in db.MlwrReport
                   where ml.mlwrId == mlwrId
                   select new { Str = ml.message };
        }

        public static List<string> GetSCByFCName(string FCName)
        {
            SandBoxDataContext db = new SandBoxDataContext();
            var sl = from sc in db.RootClassificationScheme
                     where sc.RootElementOfClassification == FCName
                     select sc.SecondaryElementOfClassification;
            List<string> res = new List<string>();
            res.AddRange(sl.ToArray());
            return res;
        }


        public static int AddMlwrFeature(int mlwrId, string fclass, string sclass, string value)
        {
            int newMlwrId = -1;
             
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var fm = from f in db.MlwrFeature
                                select f.Id;
                int ewMlwrId = fm.Max() + 1;
                MlwrFeature mlwrF = new MlwrFeature()
                {
                    Id = ewMlwrId,
                    MlwrId = mlwrId,
                    FClass = fclass,
                    SClass = sclass,
                    Value = value
                };
                db.MlwrFeature.InsertOnSubmit(mlwrF);
                db.SubmitChanges();

            }
            return newMlwrId;
 
        }


        /// <summary>
        /// Получение  всех кооментариев в ВПО по его Id
        /// </summary>
        /// <param name="MlwrID"></param>
        /// <returns></returns>
        public static IQueryable<MlwrComments> GetCommentsById(int MlwrID)
        {
            var db = new SandBoxDataContext();
            var res = from c in db.MlwrComments
                      where c.MlwrId == MlwrID
                      select c;
            return res;
        }

        public static void InsertComment(int mlwrId, string comment)
        {
            var db = new SandBoxDataContext();
            db.MlwrComments.InsertOnSubmit(new MlwrComments() { CommentValue = comment, MlwrId = mlwrId });
            db.SubmitChanges();
        }

         /// <summary>
        /// Получение записей с направлениями воздействия СПО
        /// TODO: Перенести MlwrManager
        /// </summary>
        /// <param name="MlwrID">Id СПО</param>
        /// <returns></returns>
        public static IQueryable<MlwrTargeting> GetTargetingOfMlwr(int MlwrID)
        {
            var db = new SandBoxDataContext();
            var res = from t in db.MlwrTargeting
                      where t.MlwrId == MlwrID
                      select t;
            return res;
        }


        /// <summary>
        /// Получение всех элементов Research для заданного ВПО (по Id ВПО)
        /// TODO: Перенести MlwrManager
        /// </summary>
        /// <param name="MlwrID">Id ВПО</param>
        /// <returns></returns>
        public static IQueryable GetResearchesByMlwr(int MlwrID)
        {
            var db = new SandBoxDataContext();
            var researches = from r in db.Researches
                             join v in db.Vms on r.VmId equals v.Id
                             join vmd in db.ResearchesVmDatas on r.ResearchVmData equals vmd.Id
                             where r.MlwrId == MlwrID
                             select new
                             {
                                 r.Id,
                                 vmType = v.EnvType,
                                 r.Duration,
                                 r.State,
                                 r.ResearchName,
                                 vmName = vmd.VmName
                             };
            return researches;
        }

        public static IQueryable<Research> GetRrschsByMlwr(int MlwrID)
        {
            var db = new SandBoxDataContext();
            var researches = from r in db.Researches
                             join v in db.Vms on r.VmId equals v.Id
                             join vmd in db.ResearchesVmDatas on r.ResearchVmData equals vmd.Id
                             where r.MlwrId == MlwrID
                             select r;
            return researches;
        }

        //**********************************************************
        //* Получение всех элементов Mlwr
        //**********************************************************
        public static IQueryable<Mlwr> GetMlwrs()
        {
            var db = new SandBoxDataContext();

            var mlwrs = from m in db.Mlwrs
                      orderby m.Id
                      select m;
            return mlwrs;
        }

        //**********************************************************
        //* Получение Mlwr по имени
        //**********************************************************
        public static Mlwr GetMlwr(String path)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                return db.Mlwrs.FirstOrDefault(x => x.Path == path);
            }
        }

        //**********************************************************
        //* Получение Mlwr по id
        //**********************************************************
        public static Mlwr GetMlwr(Int32 id)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                return db.Mlwrs.FirstOrDefault(x => x.Id == id);
            }
        }

        public static Mlwr GetMlwrById(Int32 id)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                return db.Mlwrs.FirstOrDefault(x => x.Id == id);
            }
        }

        //**********************************************************
        //* Получение Mlwr для отображения
        //**********************************************************
        public static IQueryable GetMlwrsTableView()
        {
            var db = new SandBoxDataContext();

            var items = from m in db.Mlwrs
                        join mc in db.MlwrClasses
                            on m.Class equals mc.Class
                        join u in db.Users
                            on m.LoadedBy equals u.UserId
                        where m.IsDeleted != 1
                        select
                            new
                                {
                                    m.Id,
                                    m.Name,
                                    m.Path,
                                    m.ResearchCount,
                                    Class = mc.Description,
                                    Loaded = "Загружено " + m.LoadedDate + " пользователем " + u.Login
                                };
            return items;
        }

        /// <summary>
        /// Получение всех значений описаний из таблицы с классификацией ВПО
        /// </summary>
        /// <param name="mlwrId"></param>
        /// <returns></returns>
        public static IQueryable GetMlwrClass(int mlwrId)
        { 
            var db = new SandBoxDataContext();
            var items = from m in db.Mlwrs
                        join mc in db.MlwrClasses
                            on m.Class equals mc.Class
                        where m.Id == mlwrId
                        select mc.Description;               
            return items;

        }


        //**********************************************************
        //* Получение количества неудаленных Mlwr
        //**********************************************************
        public static Int32 GetMlwrsCount()
        {
            var db = new SandBoxDataContext();

            var mlwrs = from m in db.Mlwrs
                        orderby m.Id
                        where m.IsDeleted != 1
                        select m;
            return mlwrs.Count();
        }

        //**********************************************************
        //* Получение количества неудаленных неисследованных Mlwr
        //**********************************************************
        public static Int32 GetMlwrsUnresearchedCount()
        {
            var db = new SandBoxDataContext();

            var mlwrs = from m in db.Mlwrs
                        orderby m.Id
                        where m.IsDeleted != 1
                        where m.ResearchCount == 0
                        select m;
            return mlwrs.Count();
        }

        //**********************************************************
        //* Получение Path по id
        //**********************************************************
        public static String GetPath(Int32 id)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Mlwr mlwr = db.Mlwrs.FirstOrDefault(x => x.Id == id);
                return mlwr == null ? null : mlwr.Path;
            }
        }

        //**********************************************************
        //* Получение всех классов Mlwrs
        //**********************************************************
        public static List<String> GetMlwrClassList()
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var classes = from mc in db.MlwrClasses
                            orderby mc.Description
                            select mc.Description;
                return classes.ToList();
            }
        }

        //**********************************************************
        //* Получение всех имен Mlwr
        //**********************************************************
        public static List<String> GetMlwrNameList()
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var names = from m in db.Mlwrs
                            orderby m.Name
                            select m.Name;
                return names.ToList();
            }
        }

        //**********************************************************
        //* Получение всех путей Mlwr
        //**********************************************************
        public static List<String> GetMlwrPathList()
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var pathes = from m in db.Mlwrs
                            orderby m.Path
                            select m.Path;
                return pathes.ToList();
            }
        }

        //**********************************************************
        //* Получение списка дополнительной информации Mlwr
        //**********************************************************
        public static List<String> GetMlwrLoadedList()
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var items = from m in db.Mlwrs
                                join u in db.Users
                                    on m.LoadedBy equals u.UserId
                            select new { Loaded = "Загружено " + m.LoadedDate + " пользователем " + u.Login };
                return items.Select(vr => vr.Loaded).ToList();
            }
        }

        //**********************************************************
        //* Добавление нового Mlwr
        //**********************************************************
        public static void AddMlwr(String name, String path, Int32 loadedBy)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Mlwr mlwr = new Mlwr()
                              {
                                  Name = name,
                                  Path = path,
                                  ResearchCount = 0,
                                  Class = 0,
                                  LoadedDate = DateTime.Now,
                                  LoadedBy = loadedBy
                };
                db.Mlwrs.InsertOnSubmit(mlwr);
                db.SubmitChanges();
            }
            TableUpdated(Table.MLWRS);
        }

        //**********************************************************
        //* Отметка Mlwr по имени как удаленного
        //**********************************************************
        public static void DeleteMlwr(String name)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var mlwr =  db.Mlwrs.FirstOrDefault(x => x.Name == name);
                if (mlwr == null) return;
                mlwr.IsDeleted = 1;
                db.SubmitChanges();
            }
        }

        //**********************************************************
        //* Отметка Mlwr по id как удаленного
        //**********************************************************
        public static void DeleteMlwr(Int32 id)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var mlwr = db.Mlwrs.FirstOrDefault(x => x.Id == id);
                if (mlwr == null) return;
                mlwr.IsDeleted = 1;
                db.SubmitChanges();
            }
        }


    }//end class MlwrManager
}//end namespace
