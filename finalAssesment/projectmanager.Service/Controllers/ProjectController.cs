using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using projectmanager.DAL;
using projectmanager.Service.Repository;

namespace projectmanager.Service.Controllers
{

    public class ProjectController : ApiController
    {
        [HttpGet]
        public JsonResult<List<Models.Projects>> GetAllProjects()
        {
            EntityMapper<Projects, Models.Projects> mapObj = new EntityMapper<Projects, Models.Projects>();
            List<Projects> prodList = ProjectDAL.GetAllProjects();
            List<Models.Projects> Projects = new List<Models.Projects>();
            foreach (var item in prodList)
            {
                Projects.Add(mapObj.Translate(item));
            }
            return Json<List<Models.Projects>>(Projects);
        }
        [HttpGet]
        public JsonResult<Models.Projects> GetProject(int id)
        {
            EntityMapper<Projects, Models.Projects> mapObj = new EntityMapper<Projects, Models.Projects>();
            Projects dalProject = ProjectDAL.GetProject(id);
            Models.Projects Projects = new Models.Projects();
            Projects = mapObj.Translate(dalProject);
            return Json<Models.Projects>(Projects);
        }
        [HttpPost]
        public bool InsertProject(Models.Projects Project)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                EntityMapper<Models.Projects, Projects> mapObj = new EntityMapper<Models.Projects, Projects>();
                Projects ProjectObj = new Projects();
                ProjectObj = mapObj.Translate(Project);
                status = ProjectDAL.InsertProject(ProjectObj);
            }
            return status;

        }
        [HttpPut]
        public bool UpdateProject(Models.Projects Project)
        {
            EntityMapper<Models.Projects, Projects> mapObj = new EntityMapper<Models.Projects, Projects>();
            Projects ProjectObj = new Projects();
            ProjectObj = mapObj.Translate(Project);
            var status = ProjectDAL.UpdateProject(ProjectObj);
            return status;

        }
        [HttpDelete]
        public bool DeleteProject(int id)
        {
            var status = ProjectDAL.DeleteProject(id);
            return status;
        }
    }
}