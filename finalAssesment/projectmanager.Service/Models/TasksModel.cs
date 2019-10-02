using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projectmanager.Service.Models
{
    public class TasksModel
    {
        public long Task_ID { get; set; }
        public string Task { get; set; }
        public long? Parent_ID { get; set; }
        public int? Project_ID { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Priority { get; set; }
        public bool? Status { get; set; }
        public int? User_ID { get; set; }
        public bool IsParentTask { get; set; }
    }
}