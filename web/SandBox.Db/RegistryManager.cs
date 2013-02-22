using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SandBox.Db
{
    public class RegistryManager
    {
        /// <summary>
        /// Получение всей таблицы с запиями регистра
        /// </summary>
        /// <returns></returns>
        public static IQueryable<RegTest> GetRegistry()
        {
            var db = new SandBoxDataContext();
            IQueryable<RegTest> res = from r in db.RegTest
                                       select r;
            return res;
        }

        /// <summary>
        /// Получение всей таблицы с запиями регистра для соответствующего исследования
        /// </summary>
        /// <returns></returns>
        public static IQueryable<RegTest> GetRegistry(int rschID)
        {
            var db = new SandBoxDataContext();
            IQueryable<RegTest> res = from r in db.RegTest
                                      where r.rschID == rschID
                                      select r;
            return res;
        }



    }
}
