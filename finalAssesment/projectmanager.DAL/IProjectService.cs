using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectmanager.DAL
{
    interface IProjectService
    {
        Task<List<Projects>> GetAllProjects();
        Task<Projects> GetProject(int project_ID);
        Task<bool> InsertProject(Projects ProjectItem);
        Task<bool> UpdateProject(Projects item);
        Task<bool> DeleteProject(int project_id);

    }
}
