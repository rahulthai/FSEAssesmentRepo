using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectmanager.DAL
{
    public class TasksDAL
    {
        static ProjectManagerEntities DbContext;
        static TasksDAL()
        {
            DbContext = new ProjectManagerEntities();
        }

        public static List<Tasks> GetAllTasks()
        {
            return DbContext.Tasks.ToList();
        }

        public static Tasks GetTask(int task_ID)
        {
            return DbContext.Tasks.Where(p => p.Task_ID == task_ID).FirstOrDefault();
        }
        public static bool InsertTask(Tasks TaskItem)
        {
            bool status;
            try
            {
                DbContext.Tasks.Add(TaskItem);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public static bool UpdateTask(Tasks item)
        {
            bool status;
            try
            {
                Tasks taskItem = DbContext.Tasks.Where(p => p.Task_ID == item.Task_ID).FirstOrDefault();
                if (taskItem != null)
                {
                    taskItem.Task = item.Task;
                    taskItem.Project_ID = item.Project_ID;
                    taskItem.Parent_ID = item.Parent_ID;
                    taskItem.Start_Date = item.Start_Date;
                    taskItem.End_Date = item.End_Date;
                    taskItem.Priority = item.Priority;
                    DbContext.SaveChanges();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public static bool DeleteTask(int task_id)
        {
            bool status;
            try
            {
                Tasks taskItem = DbContext.Tasks.Where(p => p.Task_ID == task_id).FirstOrDefault();
                if (taskItem != null)
                {
                    DbContext.Tasks.Remove(taskItem);
                    DbContext.SaveChanges();
                }
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
