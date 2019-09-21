using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projectmanager.Service.Models
{
    public class Users
    {
        public int User_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Employee_ID { get; set; }
        public int? Project_ID { get; set; }
        public int? Task_ID { get; set; }

    }
}