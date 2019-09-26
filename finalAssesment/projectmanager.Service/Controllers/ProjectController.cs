using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public IContext dbContext;
        public ProjectController()
        {
            dbContext = new ProjectManagerEntities();
        }
        [HttpGet]
        public JsonResult<List<ProjectsModel>> GetAllProjects()
        {
            //EntityMapper<Projects, ProjectsModel> mapObj = new EntityMapper<Projects, ProjectsModel>();

            ProjectService projService = new ProjectService(dbContext);
            List<Projects> prodList = projService.GetAllProjects();
            List<ProjectsModel> projectsModelList = new List<ProjectsModel>();
            AutoMapper.Mapper.Map(prodList, projectsModelList);
            //foreach (var item in prodList)
            //{
            //    projectsModelList.Add(item);
            //}
            return Json<List<ProjectsModel>>(projectsModelList);
        }
        [HttpGet]
        public JsonResult<ProjectsModel> GetProject(int id)
        {
            //EntityMapper<Projects, ProjectsModel> mapObj = new EntityMapper<Projects, ProjectsModel>();
            ProjectService projService = new ProjectService(dbContext);
            Projects dalProject = projService.GetProject(id);
            ProjectsModel Projects = new ProjectsModel();
            //Projects = mapObj.Translate(dalProject);
            AutoMapper.Mapper.Map(dalProject, Projects);

            return Json<ProjectsModel>(Projects);
        }
        [HttpPost]
        public bool InsertProject(ProjectsModel Project)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
               // EntityMapper<ProjectsModel, Projects> mapObj = new EntityMapper<ProjectsModel, Projects>();
                Projects ProjectObj = new Projects();
                //ProjectObj = mapObj.Translate(Project);
                AutoMapper.Mapper.Map(Project, ProjectObj);

                ProjectService projService = new ProjectService(dbContext);
                status = projService.InsertProject(ProjectObj);
            }
            return status;

        }
        [HttpPut]
        public bool UpdateProject(ProjectsModel Project)
        {
            //EntityMapper<ProjectsModel, Projects> mapObj = new EntityMapper<ProjectsModel, Projects>();
            Projects ProjectObj = new Projects();
            //ProjectObj = mapObj.Translate(Project);
            AutoMapper.Mapper.Map(Project, ProjectObj);

            ProjectService projService = new ProjectService(dbContext);
            var status = projService.UpdateProject(ProjectObj);
            return status;

        }
        [HttpDelete]
        public bool DeleteProject(int id)
        {
            ProjectService projService = new ProjectService(dbContext);

            var status = projService.DeleteProject(id);
            return status;
        }
    }
}