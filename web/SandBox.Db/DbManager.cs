using System;

namespace SandBox.Db
{
    public enum Table
    {
        USERS,
        ROLES,
        REPORTS,
        
        VMS,
        MLWRS
    }
    
    public class DbManagerEventArgs : EventArgs
    {
        public Table Table;
        public DbManagerEventArgs(Table table)
        {
            Table = table;
        }
    }//end GuiEventArgs
    
    public class DbManager
    {
        public delegate void DbManagerEventHandler(Table table);
        public static event DbManagerEventHandler OnTableUpdated;

        protected static void TableUpdated(Table table)
        {
            DbManagerEventHandler handler = OnTableUpdated;
            if (handler != null) handler(table);
        }

        public static Boolean GetConnectionStatus()
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                return db.DatabaseExists();
            }
        }
    }//end DbManager class
}
