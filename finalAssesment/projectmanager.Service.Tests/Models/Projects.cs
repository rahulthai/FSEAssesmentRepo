﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projectmanager.Service.Tests.Models
{
    public class Projects
    {
        public int Project_ID { get; set; }
        public string Project { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Priority { get; set; }
    }
}