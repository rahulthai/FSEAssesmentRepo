//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace projectmanager.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tasks
    {
        public long Task_ID { get; set; }
        public Nullable<long> Parent_ID { get; set; }
        public Nullable<int> Project_ID { get; set; }
        public string Task { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
        public Nullable<System.DateTime> End_Date { get; set; }
        public string Priority { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> User_ID { get; set; }
        public string TaskStatus { get; set; }
    
        public virtual ParentTask ParentTask { get; set; }
        public virtual Projects Projects { get; set; }
        public virtual Users Users { get; set; }
    }
}
