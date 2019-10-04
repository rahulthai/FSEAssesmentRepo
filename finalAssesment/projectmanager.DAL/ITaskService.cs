using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectmanager.DAL
{
    interface ITaskService
    {
        System.Threading.Tasks.Task<List<Tasks>> GetAllTasks();
        System.Threading.Tasks.Task<Tasks> GetTask(int task_ID);
        System.Threading.Tasks.Task<bool> InsertTask(Tasks TaskItem);
        System.Threading.Tasks.Task<bool> InsertParentTask(ParentTask TaskItem);
        System.Threading.Tasks.Task<bool> UpdateTask(Tasks item);
        System.Threading.Tasks.Task<bool> DeleteTask(int task_id);

    }
}
