using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectmanager.DAL
{
    public class UsersDAL
    {
        static ProjectManagerEntities DbContext;
        static UsersDAL()
        {
            DbContext = new ProjectManagerEntities();
        }

        public static List<Users> GetAllUsers()
        {
            return DbContext.Users.ToList();
        }

        public static Users GetUser(int user_ID)
        {
            return DbContext.Users.Where(p => p.User_ID == user_ID).FirstOrDefault();
        }
        public static bool InsertUser(Users UserItem)
        {
            bool status;
            try
            {
                DbContext.Users.Add(UserItem);
                DbContext.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public static bool UpdateUser(Users item)
        {
            bool status;
            try
            {
                Users userItem = DbContext.Users.Where(p => p.User_ID == item.User_ID).FirstOrDefault();
                if (userItem != null)
                {
                    userItem.FirstName = item.FirstName;
                    userItem.LastName = item.LastName;
                    userItem.Employee_ID = item.Employee_ID;
                    userItem.Project_ID = item.Project_ID;
                    userItem.Task_ID = item.Task_ID;
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
        public static bool DeleteUser(int user_id)
        {
            bool status;
            try
            {
                Users userItem = DbContext.Users.Where(p => p.User_ID == user_id).FirstOrDefault();
                if (userItem != null)
                {
                    DbContext.Users.Remove(userItem);
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
