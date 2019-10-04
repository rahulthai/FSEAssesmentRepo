using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectmanager.DAL
{
    public class UserService : IUserService
    {
        private IContext _DbContext;
        public UserService(IContext DbContext)
        {
            _DbContext = DbContext;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _DbContext.Users.ToListAsync();
        }

        public async Task<Users> GetUser(int user_ID)
        {
            return await _DbContext.Users.Where(p => p.User_ID == user_ID).FirstOrDefaultAsync();
        }
        public async Task<bool> InsertUser(Users UserItem)
        {
            bool status;
            try
            {
                 _DbContext.Users.Add(UserItem);
                await _DbContext.SaveChangesAsync();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }
        public async Task<bool> UpdateUser(Users item)
        {
            bool status;
            try
            {
                Users userItem = _DbContext.Users.Where(p => p.User_ID == item.User_ID).FirstOrDefault();
                if (userItem != null)
                {
                    userItem.FirstName = item.FirstName;
                    userItem.LastName = item.LastName;
                    userItem.Employee_ID = item.Employee_ID;
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

        //public async Task<bool> UpdateUserProjectTask(int userid, int? projectid, int? taskid )
        //{
        //    bool status;
        //    try
        //    {
        //        Users userItem = _DbContext.Users.Where(p => p.User_ID == userid).FirstOrDefault();
        //        if (userItem != null)
        //        {
        //            userItem.Project_ID = projectid;
        //            userItem.Task_ID = taskid;
        //            await _DbContext.SaveChangesAsync();
        //        }
        //        status = true;
        //    }
        //    catch (Exception)
        //    {
        //        status = false;
        //    }
        //    return status;
        //}

        public async Task<bool> DeleteUser(int user_id)
        {
            bool status;
            try
            {
                Users userItem = _DbContext.Users.Where(p => p.User_ID == user_id).FirstOrDefault();
                if (userItem != null)
                {
                    _DbContext.Users.Remove(userItem);
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
