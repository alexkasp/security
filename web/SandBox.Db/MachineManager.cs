using System;
using System.Linq;

namespace SandBox.Db
{
    public class MachineManager: DbManager
    {
        public static IQueryable<Machine> GetMachines()
        {
            var db = new SandBoxDataContext();

            var machines = from m in db.Machines
                        orderby m.Id
                        select m;
            return machines;
        }

        public static String[] GetMachinesName()
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                var machines = from m in db.Machines
                               orderby m.Name
                               select m.Name;
                return machines.ToArray();
            }
        }

        public static Machine GetMachineByName(String name)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                return db.Machines.FirstOrDefault(x => x.Name == name);
            }
        }

        public static IQueryable GetItems()
        {
            var db = new SandBoxDataContext();

            var items = from i in db.Machines
                        join ms in db.MachinesStates
                        on i.State equals ms.State
                        select new { i.Id, i.Name, i.State, i.UniqueId, ms.Description };
            return items;
        }

        public static String GetDescription(Int32 machineState)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                return db.MachinesStates.FirstOrDefault(x => x.State == machineState).Description;
            }
        }

        public static String GetNameById(Int32 id)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                return db.Machines.FirstOrDefault(x => x.Id == id).Name;
            }
        }

        public static void UpdateState(String name, Int32 state)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Machine machine = db.Machines.FirstOrDefault(m => m.Name == name);
                        machine.State = state;
                db.SubmitChanges();
                TableUpdated(Table.MACHINES);
            }
        }

        public static void DeleteItem(Int32 id)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Machine machine =  db.Machines.FirstOrDefault(x => x.Id == id);
                db.Machines.DeleteOnSubmit(machine);
                db.SubmitChanges();
                TableUpdated(Table.MACHINES);
            }
        }

        public static void CreateItem(String name)
        {
            using (SandBoxDataContext db = new SandBoxDataContext())
            {
                Machine machine = new Machine {Name = name, State = -1, UniqueId = 2, Description = "null"};
                db.Machines.InsertOnSubmit(machine);
                db.SubmitChanges();
                TableUpdated(Table.MACHINES);
            }
        }
    }//end class
}//end namespace
