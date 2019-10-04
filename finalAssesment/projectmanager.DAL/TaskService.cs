using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectmanager.DAL
{
    public class TaskService: ITaskService
    {
        private IContext _DbContext;
        public TaskService(IContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            //&& x.Parent_ID!=null
            return await _DbContext.Tasks.Include(x=>x.ParentTask).OrderBy(x => x.Task).ToListAsync();
        }

        public async Task<Tasks> GetTask(int task_ID)
        {
            return await _DbContext.Tasks.Include(x=>x.Users).Include(x=>x.Projects).Where(p => p.Task_ID == task_ID && p.Status == true).FirstOrDefaultAsync();
        }

        public async Task<List<Tasks>> GetTasksByProjectId(int projectId)
        {
            //&& x.Parent_ID!=null
            return await _DbContext.Tasks.Include(x => x.ParentTask).Where(x => x.Project_ID == projectId).OrderBy(x => x.Task).ToListAsync();
        }

        public async Task<List<ParentTask>> GetParentTasks()
        {
            return await _DbContext.ParentTask.OrderBy(x => x.Parent_Task).ToListAsync();

        }

        public async Task<bool> InsertTask(Tasks TaskItem)
        {
            bool status;
            try
            {
                TaskItem.Status = true;
                _DbContext.Tasks.Add(TaskItem);
                await _DbContext.SaveChangesAsync();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public async Task<bool> InsertTask(Tasks TaskItem, ParentTask parentTask)
        {
            bool status;
            try
            {
                TaskItem.Status = true;
                _DbContext.Tasks.Add(TaskItem);
                _DbContext.ParentTask.Add(parentTask);

                await _DbContext.SaveChangesAsync();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public async Task<bool> InsertParentTask(ParentTask TaskItem)
        {
            bool status;
            try
            {
                _DbContext.ParentTask.Add(TaskItem);
                await _DbContext.SaveChangesAsync();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public async Task<bool> UpdateTask(Tasks item)
        {
            bool status;
            try
            {
                Tasks taskItem = _DbContext.Tasks.Where(p => p.Task_ID == item.Task_ID && p.Status == true).FirstOrDefault();
                if (taskItem != null)
                {
                    taskItem.Task = item.Task;
                    taskItem.Start_Date = item.Start_Date;
                    taskItem.End_Date = item.End_Date;
                    taskItem.Priority = item.Priority;
                    taskItem.User_ID = item.User_ID;
                    taskItem.Parent_ID = item.Parent_ID;
                    //taskItem.Project_ID = item.Project_ID;

                    await _DbContext.SaveChangesAsync();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public async Task<bool> UpdateTask(Tasks item, ParentTask parentItem)
        {
            bool status;
            try
            {
                Tasks taskItem = _DbContext.Tasks.Where(p => p.Task_ID == item.Task_ID && p.Status == true).FirstOrDefault();

                

                if (taskItem != null)
                {
                    ParentTask parenttaskItem = _DbContext.ParentTask.Where(p => p.Parent_Task == taskItem.Task).FirstOrDefault();

                    taskItem.Task = item.Task;
                    taskItem.Start_Date = null;
                    taskItem.End_Date = null;
                    taskItem.Priority = null;
                    taskItem.User_ID = null;
                    //taskItem.Project_ID = item.Project_ID;
                    taskItem.Parent_ID = null;
                    if (parenttaskItem != null)
                    {
                        parenttaskItem.Parent_Task = item.Task;
                    }
                    
                    await _DbContext.SaveChangesAsync();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public async Task<bool> DeleteTask(int task_id)
        {
            bool status;
            try
            {
                Tasks taskItem = _DbContext.Tasks.Where(p => p.Task_ID == task_id && p.Status == true).FirstOrDefault();
                if (taskItem != null)
                {
                    taskItem.Status = false;
                    taskItem.TaskStatus = "Completed";
                    await _DbContext.SaveChangesAsync();
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
