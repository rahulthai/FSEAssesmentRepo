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
            return await _DbContext.Projects.Include(x=>x.Tasks).Include(x => x.Users).Where(x=>x.Status==true).ToListAsync();
        }

        public async Task<Projects> GetProject(int project_ID)
        {
            return await _DbContext.Projects.Include(u =>u.Users).Where(p => p.Project_ID == project_ID && p.Status==true).FirstOrDefaultAsync();
        }
        public async Task<bool> InsertProject(Projects ProjectItem)
        {
            bool status;
            try
            {
                ProjectItem.Status = true;
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
                Projects projectItem = _DbContext.Projects.Where(p => p.Project_ID == item.Project_ID && p.Status==true).FirstOrDefault();
                if (projectItem != null)
                {
                    projectItem.Project = item.Project;
                    projectItem.StartDate = item.StartDate;
                    projectItem.EndDate = item.EndDate;
                    projectItem.Priority = item.Priority;
                    projectItem.User_ID = item.User_ID;
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
                Projects projectItem = _DbContext.Projects.Where(p => p.Project_ID == project_id && p.Status == true).FirstOrDefault();
                if (projectItem != null)
                {
                    projectItem.Status = false;
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
