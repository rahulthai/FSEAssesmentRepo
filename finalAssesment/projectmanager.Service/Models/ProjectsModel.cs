using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projectmanager.Service.Models
{
    public class ProjectsModel
    {
        public int Project_ID { get; set; }
        public string Project { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Priority { get; set; }
        public bool? Status { get; set; }
        public int? User_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}