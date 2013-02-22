using System;
using System.Linq;

namespace SandBox.Db
{
    public enum RequestState
    {
        EXECUTING        = 1,
        COMPLETE         = 2
    }

    public enum RequestType
    {
        LOAD_TRAFFIC    = 1,
        LOAD_PROCESSES  = 2,
        LOAD_FILES      = 3,
        LOAD_REGISTRY   = 4
    }

    public class RequestManager : DbManager
    {
        //**********************************************************
        //* Получение всех запросов
        //**********************************************************
        public static IQueryable<Request> GetRequests()
        {
            var db = new SandBoxDataContext();

            var requests = from r in db.Requests
                      orderby r.Id
                      select r;
            return requests;
        }

        //**********************************************************
        //* Получение всех запросов исследования
        //**********************************************************
        public static IQueryable<Request> GetRequests(Int32 researchId)
        {
            var db = new SandBoxDataContext();

            var requests = from r in db.Requests
                            orderby r.Id
                             where r.ResearchId == researchId
                              select r;
            return requests;
        }

        //**********************************************************
        //* Удалени всех запросов
        //**********************************************************
        public static void DeleteRequests()
        {
            var db = new SandBoxDataContext();

            var requests = from r in db.Requests
                           orderby r.Id
                           select r;
            db.Requests.DeleteAllOnSubmit(requests);
            db.SubmitChanges();
        }

        //**********************************************************
        //* Удалени всех запросов исследования
        //**********************************************************
        public static void DeleteRequests(Int32 researchId)
        {
            var db = new SandBoxDataContext();

            var requests = from r in db.Requests
                           orderby r.Id
                           where r.ResearchId == researchId
                           select r;
            db.Requests.DeleteAllOnSubmit(requests);
            db.SubmitChanges();
        }

        //**********************************************************
        //* Добавление нового запроса
        //**********************************************************
        public static void AddRequest(Int32 researchId, RequestType requestType)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Request request = new Request() {ResearchId = researchId, Type = (Int32)requestType, State = (Int32)RequestState.EXECUTING};
                db.Requests.InsertOnSubmit(request);
                db.SubmitChanges();
            }
        }

        //**********************************************************
        //* Проверка существования запроса
        //**********************************************************
        public static Boolean IsRequestExist(Int32 researchId, RequestType requestType)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var requests = from r in db.Requests
                               where r.ResearchId == researchId
                               where r.Type == (Int32) requestType
                               select r;

                return requests.Count() != 0;
            }
        }

        //**********************************************************
        //* Получение результата запроса
        //**********************************************************
        public static String GetRequestResult(Int32 researchId, RequestType requestType)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var request = (from r in db.Requests
                               where r.ResearchId == researchId
                               where r.Type == (Int32)requestType
                               select r).First();

                return request.State == (Int32)RequestState.EXECUTING ? String.Empty : request.Result;
            }
        }

        //**********************************************************
        //* Получение запроса
        //**********************************************************
        public static Request GetRequest(Int32 researchId, RequestType type)
        {
            var db = new SandBoxDataContext();

            var requests = from r in db.Requests
                           where r.ResearchId == researchId
                           where r.Type == (Int32)type
                           orderby r.Id
                           select r;
            return requests.First();
        }

        //**********************************************************
        //* Обновление запроса
        //**********************************************************
        public static void UpdateRequest(Int32 requestId, RequestState state, String result)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Request request = db.Requests.FirstOrDefault(x => x.Id == requestId);
                if (request == null) return;
                 request.State = (Int32)state;
                 request.Result = result;
                db.SubmitChanges();
            }
        }

        public static void ClearRequests(Int32 researchId)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var requests = from r in db.Requests
                              where r.ResearchId == researchId
                              select r;

                db.Requests.DeleteAllOnSubmit(requests);
                db.SubmitChanges();
            }
        }
    }//end class TaskManager
}//end namespace
