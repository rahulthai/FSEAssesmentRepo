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
        List<Projects> GetAllProjects();
        Projects GetProject(int project_ID);
        bool InsertProject(Projects ProjectItem);
        bool UpdateProject(Projects item);
        bool DeleteProject(int project_id);

    }
}
