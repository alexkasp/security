using System;
using System.Linq;

namespace SandBox.Db
{
    public class ConnectionManager
    {
        public static ConnectionSetting LoadSettings()
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                return  db.ConnectionSettings.FirstOrDefault();
            }
        }
        public static void SaveSettings(String rolename)
        {
            //todo:
        }
    }
}
