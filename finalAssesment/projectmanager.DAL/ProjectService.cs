using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<List<Projects>> GetAllProjects()
        {
            return await _DbContext.Projects.ToListAsync();
        }

        public async Task<Projects> GetProject(int project_ID)
        {
            return await _DbContext.Projects.Where(p => p.Project_ID == project_ID).FirstOrDefaultAsync();
        }
        public async Task<bool> InsertProject(Projects ProjectItem)
        {
            bool status;
            try
            {
                 _DbContext.Projects.Add(ProjectItem);
                await _DbContext.SaveChangesAsync();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public async Task<bool> UpdateProject(Projects item)
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
        public async Task<bool> DeleteProject(int project_id)
        {
            bool status;
            try
            {
                Projects projectItem = _DbContext.Projects.Where(p => p.Project_ID == project_id).FirstOrDefault();
                if (projectItem != null)
                {
                    _DbContext.Projects.Remove(projectItem);
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
