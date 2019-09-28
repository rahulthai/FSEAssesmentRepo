using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projectmanager.Service.Models
{
    public class ParentTaskModel
    {
        public long Parent_ID { get; set; }
        public string Parent_Task { get; set; }
    }
}