using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using projectmanager.DAL;
using projectmanager.Service.Models;
using projectmanager.Service.Repository;

namespace projectmanager.Service.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IContext dbContext = new ProjectManagerEntities();
        private UserService userService;

        public UsersController()
        {

        }

        public UsersController(IContext context)
        {
            dbContext = context;
        }
        [HttpGet]
        public async Task<JsonResult<List<UsersModel>>> GetAllUsers()
        {
            userService = new UserService(dbContext);
            List<Users> userList = await userService.GetAllUsers();
            List<UsersModel> usersModelList = new List<UsersModel>();
            AutoMapper.Mapper.Map(userList, usersModelList);
            return Json<List<UsersModel>>(usersModelList);
        }
        [HttpGet]
        public async Task<JsonResult<UsersModel>> GetUserAsync(int id)
        {
            userService = new UserService(dbContext);
            Users dalUser = await userService.GetUser(id);
            UsersModel Users = new UsersModel();
            AutoMapper.Mapper.Map(dalUser, Users);

            return Json<UsersModel>(Users);
        }
        [HttpPost]
        public async Task<bool> InsertUserAsync(UsersModel User)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                Users UserObj = new Users();
                AutoMapper.Mapper.Map(User, UserObj);

                userService = new UserService(dbContext);
                status = await userService.InsertUser(UserObj);
            }
            return status;

        }
        [HttpPut]
        public async Task<bool> UpdateUser(UsersModel User)
        {
            Users UserObj = new Users();
            AutoMapper.Mapper.Map(User, UserObj);

            userService = new UserService(dbContext);
            var status = await userService.UpdateUser(UserObj);
            return status;

        }
        [HttpDelete]
        public async Task<bool> DeleteUser(int id)
        {
            userService = new UserService(dbContext);
            var status = await userService.DeleteUser(id);
            return status;
        }
    }
}
