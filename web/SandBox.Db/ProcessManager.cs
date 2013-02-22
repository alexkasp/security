using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SandBox.Db
{
    public class ProcessManager : DbManager
    {
        /// <summary>
        /// Получение списка всех процессов для всех исследований
        /// </summary>
        /// <returns></returns>
        public static IQueryable<Proc> GetProcesses()
        {
            var db = new SandBoxDataContext();

            var processes = from p in db.Proc
                             orderby p.rschID
                             select p;
            return processes;
        }

        /// <summary>
        /// Возвращает все процессы для для заданного исследования
        /// </summary>
        /// <param name="rschId">id исследования для которого получаем процессы</param>
        /// <returns></returns>
        public static IQueryable<Proc> GetProcesses(int rschId)
        {
            var db = new SandBoxDataContext();
            var processes = from p in db.Proc
                            where p.rschID == rschId
                            orderby p.procName
                            select p;
            return processes;
        }




        public static IQueryable<Proc> GetProcessesTableView(int rschId)
        {
            var db = new SandBoxDataContext();
            var results = from proc in db.Proc
                          where proc.rschID == rschId
                          orderby proc.procName
                          select proc;
            return results;
        }

        public static IQueryable<Proc> GetProcessesTableView()
        {
            var db = new SandBoxDataContext();
            var results = from proc in db.Proc
                          orderby proc.procName
                          select proc;
            return results;
        }
    }
}