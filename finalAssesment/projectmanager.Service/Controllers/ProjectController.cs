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
            AutoMapper.Mapper.Map(prodList, projectsModelList);
            //foreach (var item in prodList)
            //{
            //    projectsModelList.Add(item);
            //}
            return Json<List<ProjectsModel>>(projectsModelList);
        }
        [HttpGet]
        public async Task<JsonResult<ProjectsModel>> GetProjectAsync(int id)
        {
            //EntityMapper<Projects, ProjectsModel> mapObj = new EntityMapper<Projects, ProjectsModel>();
            //ProjectService projService = new ProjectService(dbContext);
            projService = new ProjectService(dbContext);
            Projects dalProject = await projService.GetProject(id);
            ProjectsModel Projects = new ProjectsModel();
            //Projects = mapObj.Translate(dalProject);
            AutoMapper.Mapper.Map(dalProject, Projects);

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