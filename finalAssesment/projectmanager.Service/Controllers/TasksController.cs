using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using projectmanager.DAL;
using projectmanager.Service.Models;
using projectmanager.Service.Repository;

namespace projectmanager.Service.Controllers
{
    public class TasksController : ApiController
    {
        private readonly IContext dbContext = new ProjectManagerEntities();
        private TaskService taskService;

        public TasksController()
        {
            //dbContext = new TaskManagerEntities();
            //taskService = new TaskService(dbContext);
        }
        public TasksController(IContext context)
        {
            dbContext = context;
            //taskService = new TaskService(dbContext);
        }
        [HttpGet]
        public async Task<JsonResult<List<TasksModel>>> GetAllTasks()
        {
            //EntityMapper<Tasks, TasksModel> mapObj = new EntityMapper<Tasks, TasksModel>();

            //TaskService taskService = new TaskService(dbContext);
            taskService = new TaskService(dbContext);
            List<Tasks> taskList = await taskService.GetAllTasks();
            List<TasksModel> tasksModelList = new List<TasksModel>();

            var tasksList = taskList.Select(x => new TasksModel
            {
                Task_ID = x.Task_ID,
                Task = x.Task,
                Priority = x.Priority,
                Start_Date = x.Start_Date ?? null,
                End_Date = x.End_Date ?? null,
                User_ID = x.User_ID,
                Name = x.Users != null ? x.Users.FirstName + " " + x.Users.LastName : "",
                Project = x.Projects != null ? x.Projects.Project : "",
                ParentTask = x.ParentTask != null ? x.ParentTask.Parent_Task : "",
                TaskStatus = x.TaskStatus,
                Project_ID = x.Project_ID,
                Parent_ID = x.Parent_ID

            }).ToList();
            // AutoMapper.Mapper.Map(tasksList, tasksModelList);
            //foreach (var item in taskList)
            //{
            //    tasksModelList.Add(item);
            //}
            return Json<List<TasksModel>>(tasksList);
        }
        [HttpGet]
        public async Task<JsonResult<TasksModel>> GetTaskByID(int id)
        {
            //EntityMapper<Tasks, TasksModel> mapObj = new EntityMapper<Tasks, TasksModel>();
            //TaskService taskService = new TaskService(dbContext);
            taskService = new TaskService(dbContext);
            Tasks dalTask = await taskService.GetTask(id);
            TasksModel Tasks = new TasksModel();
            if (dalTask != null)
            {

                Tasks.Task_ID = dalTask.Task_ID;
                Tasks.Task = dalTask.Task;
                Tasks.Parent_ID = dalTask.Parent_ID;
                Tasks.ParentTask = dalTask.ParentTask != null ? dalTask.ParentTask.Parent_Task : "";
                Tasks.Start_Date = dalTask.Start_Date;
                Tasks.End_Date = dalTask.End_Date;
                Tasks.Priority = dalTask.Priority;
                Tasks.Project_ID = dalTask.Project_ID;
                Tasks.Project = dalTask.Projects != null ? dalTask.Projects.Project : "";

                Tasks.User_ID = dalTask.User_ID;
                Tasks.Name = dalTask.Users != null ? dalTask.Users.FirstName + " " + dalTask.Users.LastName : "";

                Tasks.TaskStatus = dalTask.TaskStatus;
            }
            else {
                throw new Exception("Task not available");
            }
            //Tasks = mapObj.Translate(dalTask);
            //AutoMapper.Mapper.Map(dalTask, Tasks);

            return Json<TasksModel>(Tasks);
        }

        [HttpGet]
        public async Task<JsonResult<List<TasksModel>>> getTasksListByProjectId(int id)
        {
            taskService = new TaskService(dbContext);
            List<Tasks> taskList = await taskService.GetTasksByProjectId(id);
            List<TasksModel> tasksModelList = new List<TasksModel>();

            var tasksList = taskList.Select(x => new TasksModel
            {
                Task_ID = x.Task_ID,
                Task = x.Task,
                Priority = x.Priority,
                Start_Date = x.Start_Date ?? null,
                End_Date = x.End_Date ?? null,
                User_ID = x.User_ID,
                Name = x.Users != null ? x.Users.FirstName + " " + x.Users.LastName : "",
                Project = x.Projects != null ? x.Projects.Project : "",
                ParentTask = x.ParentTask != null ? x.ParentTask.Parent_Task : "",
                TaskStatus = x.TaskStatus,
                Project_ID = x.Project_ID,
                Parent_ID = x.Parent_ID

            }).ToList();

            //Tasks = mapObj.Translate(dalTask);
            //AutoMapper.Mapper.Map(dalTask, Tasks);

            return Json<List<TasksModel>>(tasksList);
        }

        [HttpGet]
        public async Task<JsonResult<List<ParentTaskModel>>> GetParentTasks()
        {

            taskService = new TaskService(dbContext);
            List<ParentTask> taskList = await taskService.GetParentTasks();
            List<ParentTaskModel> tasksModelList = new List<ParentTaskModel>();

            var parentTasksList = taskList.Select(x => new ParentTaskModel
            {
                Parent_ID = x.Parent_ID,
                Parent_Task = x.Parent_Task

            }).ToList();
            // AutoMapper.Mapper.Map(tasksList, tasksModelList);
            return Json<List<ParentTaskModel>>(parentTasksList);
        }

        [HttpPost]
        public async Task<bool> InsertTaskAsync(TasksModel Task)
        {
                bool status = false;
                if (ModelState.IsValid)
                {
                    //System.Threading.Tasks.Task<bool> parenttaskstatus;
                    //System.Threading.Tasks.Task<bool> taskstatus;

                    var isParentTask = Task.IsParentTask;
                    var taskName = Task.Task;
                    taskService = new TaskService(dbContext);
                    ParentTask parentTask = new ParentTask();
                    Tasks TaskObj = new Tasks();
                    //AutoMapper.Mapper.Map(Task, TaskObj);
                    TaskObj.Task = Task.Task;
                    TaskObj.Parent_ID = Task.Parent_ID;

                    TaskObj.Project_ID = Task.Project_ID;
                    TaskObj.Start_Date = Task.Start_Date;
                    TaskObj.End_Date = Task.End_Date;
                    TaskObj.Priority = Task.Priority==null ||Task.Priority.Equals("") || Task.Priority.Equals("0") ? null : Task.Priority;
                    TaskObj.Status = true;
                    TaskObj.User_ID = Task.User_ID;

                    if (isParentTask)
                    {
                        parentTask.Parent_Task = taskName;

                        status = await taskService.InsertTask(TaskObj, parentTask);
                    }
                    else
                    {
                        status = await taskService.InsertTask(TaskObj);

                    }

                }
                return status;

        }
        [HttpPut]
        public async Task<bool> UpdateTask(TasksModel Task)
        {
            bool status = false;
            //EntityMapper<TasksModel, Tasks> mapObj = new EntityMapper<TasksModel, Tasks>();
            Tasks TaskObj = new Tasks();
            ParentTask parentTask = new ParentTask();
            var isParentTask = Task.IsParentTask;
            var taskName = Task.Task;
            //TaskObj = mapObj.Translate(Task);
            //AutoMapper.Mapper.Map(Task, TaskObj);

            TaskObj.Task_ID = Task.Task_ID;
            TaskObj.Task = Task.Task;

            TaskObj.Parent_ID = isParentTask ? null : Task.Parent_ID;
            //TaskObj.Project_ID = Task.Project_ID;
            TaskObj.Start_Date = Task.Start_Date;
            TaskObj.End_Date = Task.End_Date;
            TaskObj.Priority = Task.Priority==null || Task.Priority.Equals("") ? null : Task.Priority;
            TaskObj.Status = true;
            TaskObj.User_ID = Task.User_ID;

            //TaskService taskService = new TaskService(dbContext);
            taskService = new TaskService(dbContext);

            if (isParentTask)
            {
                parentTask.Parent_Task = taskName;

                status = await taskService.UpdateTask(TaskObj, parentTask);
            }
            else
            {
                status = await taskService.UpdateTask(TaskObj);

            }

            return status;

        }
        [HttpDelete]
        public async Task<bool> DeleteTask(int id)
        {
            //TaskService taskService = new TaskService(dbContext);
            taskService = new TaskService(dbContext);
            var status = await taskService.DeleteTask(id);
            return status;
        }
    }
}
