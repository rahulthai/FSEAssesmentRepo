using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectmanager.DAL
{
    public class ProjectService : IProjectService
    {
        private IContext _DbContext;
        public ProjectService(IContext DbContext)
        {
            _DbContext = DbContext;
        }

        public List<Projects> GetAllProjects()
        {
            return _DbContext.Projects.ToList();
        }

        public Projects GetProject(int project_ID)
        {
            return _DbContext.Projects.Where(p => p.Project_ID == project_ID).FirstOrDefault();
        }
        public bool InsertProject(Projects ProjectItem)
        {
            bool status;
            try
            {
                _DbContext.Projects.Add(ProjectItem);
                _DbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public bool UpdateProject(Projects item)
        {
            bool status;
            try
            {
                Projects projectItem = _DbContext.Projects.Where(p => p.Project_ID == item.Project_ID).FirstOrDefault();
                if (projectItem != null)
                {
                    projectItem.Project = item.Project;
                    projectItem.StartDate = item.StartDate;
                    projectItem.EndDate = item.EndDate;
                    projectItem.Priority = item.Priority;
                    _DbContext.SaveChanges();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public bool DeleteProject(int project_id)
        {
            bool status;
            try
            {
                Projects projectItem = _DbContext.Projects.Where(p => p.Project_ID == project_id).FirstOrDefault();
                if (projectItem != null)
                {
                    _DbContext.Projects.Remove(projectItem);
                    _DbContext.SaveChanges();
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
