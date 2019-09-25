using projectmanager.DAL;
using projectmanager.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace projectmanager.Service.Controllers
{
    public class TasksController : ApiController
    {
        /*
        [HttpGet]
        public JsonResult<List<Models.Tasks>> GetAllTasks()
        {
            EntityMapper<Tasks, Models.Tasks> mapObj = new EntityMapper<Tasks, Models.Tasks>();
            List<Tasks> prodList = TasksDAL.GetAllTasks();
            List<Models.Tasks> Tasks = new List<Models.Tasks>();
            foreach (var item in prodList)
            {
                Tasks.Add(mapObj.Translate(item));
            }
            return Json<List<Models.Tasks>>(Tasks);
        }
        [HttpGet]
        public JsonResult<Models.Tasks> GetTask(int id)
        {
            EntityMapper<Tasks, Models.Tasks> mapObj = new EntityMapper<Tasks, Models.Tasks>();
            Tasks dalProject = TasksDAL.GetTask(id);
            Models.Tasks Tasks = new Models.Tasks();
            Tasks = mapObj.Translate(dalProject);
            return Json<Models.Tasks>(Tasks);
        }
        [HttpPost]
        public bool InsertTask(Models.Tasks task)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                //if task is parent task first isnert data into parent task table and later take identity column and insert record into task table
                if (task.IsParentTask == true) {
                    ParentTask PTaskObj = new ParentTask();
                    PTaskObj.Parent_Task = task.Task;
                    ParentTaskDAL.InsertParentTask(PTaskObj);
                    //TODO - retreive parentID and insert into task

                    task.Parent_ID = PTaskObj.Parent_ID;
                }

                EntityMapper<Models.Tasks, Tasks> mapObj = new EntityMapper<Models.Tasks, Tasks>();
                Tasks TaskObj = new Tasks();
                TaskObj = mapObj.Translate(task);
                status = TasksDAL.InsertTask(TaskObj);
            }
            return status;

        }
        [HttpPut]
        public bool UpdateTask(Models.Tasks task)
        {
            EntityMapper<Models.Tasks, Tasks> mapObj = new EntityMapper<Models.Tasks, Tasks>();
            Tasks TaskObj = new Tasks();
            TaskObj = mapObj.Translate(task);
            var status = TasksDAL.UpdateTask(TaskObj);
            return status;

        }
        [HttpDelete]
        public bool DeleteTask(int id)
        {
            var status = TasksDAL.DeleteTask(id);
            return status;
        }
        */
    }
}
