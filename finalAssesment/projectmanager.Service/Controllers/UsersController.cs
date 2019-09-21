using projectmanager.DAL;
using projectmanager.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace projectmanager.Service.Controllers
{
    public class UsersController : ApiController
    {
        [HttpGet]
        public JsonResult<List<Models.Users>> GetAllUsers()
        {
            EntityMapper<Users, Models.Users> mapObj = new EntityMapper<Users, Models.Users>();
            List<Users> prodList = UsersDAL.GetAllUsers();
            List<Models.Users> Users = new List<Models.Users>();
            foreach (var item in prodList)
            {
                Users.Add(mapObj.Translate(item));
            }
            return Json<List<Models.Users>>(Users);
        }
        [HttpGet]
        public JsonResult<Models.Users> GetUser(int id)
        {
            EntityMapper<Users, Models.Users> mapObj = new EntityMapper<Users, Models.Users>();
            Users dalProject = UsersDAL.GetUser(id);
            Models.Users Users = new Models.Users();
            Users = mapObj.Translate(dalProject);
            return Json<Models.Users>(Users);
        }
        [HttpPost]
        public bool InsertUser(Models.Users user)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                EntityMapper<Models.Users, Users> mapObj = new EntityMapper<Models.Users, Users>();
                Users UserObj = new Users();
                UserObj = mapObj.Translate(user);
                status = UsersDAL.InsertUser(UserObj);
            }
            return status;

        }
        [HttpPut]
        public bool UpdateUser(Models.Users user)
        {
            EntityMapper<Models.Users, Users> mapObj = new EntityMapper<Models.Users, Users>();
            Users UserObj = new Users();
            UserObj = mapObj.Translate(user);
            var status = UsersDAL.UpdateUser(UserObj);
            return status;

        }
        [HttpDelete]
        public bool DeleteUser(int id)
        {
            var status = UsersDAL.DeleteUser(id);
            return status;
        }

    }
}
