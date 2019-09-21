using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectmanager.DAL
{
    public class ProjectDAL
    {
        static ProjectManagerEntities DbContext;
        static ProjectDAL()
        {
            DbContext = new ProjectManagerEntities();
        }

        public static List<Projects> GetAllProjects()
        {
            return DbContext.Projects.ToList();
        }

        public static Projects GetProject(int project_ID)
        {
            return DbContext.Projects.Where(p => p.Project_ID == project_ID).FirstOrDefault();
        }
        public static bool InsertProject(Projects ProjectItem)
        {
            bool status;
            try
            {
                DbContext.Projects.Add(ProjectItem);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public static bool UpdateProject(Projects item)
        {
            bool status;
            try
            {
                Projects projectItem = DbContext.Projects.Where(p => p.Project_ID == item.Project_ID).FirstOrDefault();
                if (projectItem != null)
                {
                    projectItem.Project = item.Project;
                    projectItem.StartDate = item.StartDate;
                    projectItem.EndDate = item.EndDate;
                    projectItem.Priority = item.Priority;
                    DbContext.SaveChanges();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public static bool DeleteProject(int project_id)
        {
            bool status;
            try
            {
                Projects projectItem = DbContext.Projects.Where(p => p.Project_ID == project_id).FirstOrDefault();
                if (projectItem != null)
                {
                    DbContext.Projects.Remove(projectItem);
                    DbContext.SaveChanges();
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
