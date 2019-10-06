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

    public class ProjectController : ApiController
    {
        private readonly IContext dbContext = new ProjectManagerEntities();
        private ProjectService projService;

        public ProjectController()
        {
            //dbContext = new ProjectManagerEntities();
            //projService = new ProjectService(dbContext);
        }
        public ProjectController(IContext context)
        {
            dbContext = context;
            //projService = new ProjectService(dbContext);
        }
        [HttpGet]
        public async Task<JsonResult<List<ProjectsModel>>> GetAllProjects()
        {
            //EntityMapper<Projects, ProjectsModel> mapObj = new EntityMapper<Projects, ProjectsModel>();

            //ProjectService projService = new ProjectService(dbContext);
            projService = new ProjectService(dbContext);
            List<Projects> prodList = await projService.GetAllProjects();
            List<ProjectsModel> projectsModelList = new List<ProjectsModel>();

            var projectsList = prodList.Select(x => new ProjectsModel
            {
                Project_ID = x.Project_ID,
                Project = x.Project,
                Priority = x.Priority,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                User_ID = x.User_ID,
                TaskCount = x.Tasks != null ? x.Tasks.Where(t => t.Status == true).Count() : 0,
                CompletedTaskCount = x.Tasks != null ? x.Tasks.Where(t => t.Status == false && t.TaskStatus.Contains("Completed")).Count() : 0

            }).ToList();
          // AutoMapper.Mapper.Map(projectsList, projectsModelList);
            //foreach (var item in prodList)
            //{
            //    projectsModelList.Add(item);
            //}
            return Json<List<ProjectsModel>>(projectsList);
        }
        [HttpGet]
        public async Task<JsonResult<ProjectsModel>> GetProjectAsync(int id)
        {
            //EntityMapper<Projects, ProjectsModel> mapObj = new EntityMapper<Projects, ProjectsModel>();
            //ProjectService projService = new ProjectService(dbContext);
            projService = new ProjectService(dbContext);
            Projects dalProject = await projService.GetProject(id);
            ProjectsModel Projects = new ProjectsModel();

            if (dalProject != null)
            {
                Projects.Project_ID = dalProject.Project_ID;
                Projects.Project = dalProject.Project;
                Projects.StartDate = dalProject.StartDate;
                Projects.EndDate = dalProject.EndDate;
                Projects.Priority = dalProject.Priority;
                Projects.User_ID = dalProject.User_ID;
                Projects.Name = dalProject.Users1 != null ? dalProject.Users1.FirstName + " " + dalProject.Users1.LastName : "";
            }
            else
            {
                throw new Exception("Project not available");
            }
            //Projects = mapObj.Translate(dalProject);
            //AutoMapper.Mapper.Map(dalProject, Projects);

            return Json<ProjectsModel>(Projects);
        }
        [HttpPost]
        public async Task<bool> InsertProjectAsync(ProjectsModel Project)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
               // EntityMapper<ProjectsModel, Projects> mapObj = new EntityMapper<ProjectsModel, Projects>();
                Projects ProjectObj = new Projects();
                //ProjectObj = mapObj.Translate(Project);
                AutoMapper.Mapper.Map(Project, ProjectObj);

                //ProjectService projService = new ProjectService(dbContext);
                projService = new ProjectService(dbContext);
                status = await projService.InsertProject(ProjectObj);
            }
            return status;

        }
        [HttpPut]
        public async Task<bool> UpdateProject(ProjectsModel Project)
        {
            //EntityMapper<ProjectsModel, Projects> mapObj = new EntityMapper<ProjectsModel, Projects>();
            Projects ProjectObj = new Projects();
            //ProjectObj = mapObj.Translate(Project);
            AutoMapper.Mapper.Map(Project, ProjectObj);

            //ProjectService projService = new ProjectService(dbContext);
            projService = new ProjectService(dbContext);
            var status = await projService.UpdateProject(ProjectObj);
            return status;

        }
        [HttpDelete]
        public async Task<bool> DeleteProject(int id)
        {
            //ProjectService projService = new ProjectService(dbContext);
            projService = new ProjectService(dbContext);
            var status = await projService.DeleteProject(id);
            return status;
        }
    }
}