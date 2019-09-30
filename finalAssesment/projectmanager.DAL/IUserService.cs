using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectmanager.DAL
{
    interface IUserService
    {
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUser(int user_ID);
        Task<bool> InsertUser(Users UserItem);
        Task<bool> UpdateUser(Users item);
        Task<bool> DeleteUser(int user_id);

    }
}
