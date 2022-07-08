using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Model
{
    public class ProjectDetails
    {
        public int Id { get; set; } //change it to long
        public string ProjectName { get; set; }
        public string ProjectNo { get; set; }
        public long ClientId { get; set; }
        public long[] EmployeeId { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DataType { get; set; }
        public int statusId { get; set; }
        public long cellId { get; set; }
        public Status Status { get; set; }
        
    }
}
