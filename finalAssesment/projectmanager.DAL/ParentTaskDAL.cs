using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectmanager.DAL
{
    public class ParentTaskDAL
    {
        static ProjectManagerEntities DbContext;
        static ParentTaskDAL()
        {
            DbContext = new ProjectManagerEntities();
        }

        public static bool InsertParentTask(ParentTask ptask)
        {
            bool status;
            try
            {
                DbContext.ParentTask.Add(ptask);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
    }
}
