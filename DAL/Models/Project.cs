using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Project : BaseModel
    {
        [Key]
        public int Id { get; set; }//change it to long
        public string ProjectName { get; set; }
        public string ProjectNo { get; set; }
        [ForeignKey("ClientCompany")]
        public long ClientId { get; set; }
        public long[] EmployeeId { get; set; }   
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string DataType { get; set; }
        [ForeignKey("Status")]
        public int statusId { get; set; }
        public virtual Status Status { get; set; }
        public virtual ClientCompany ClientCompany { get; set; }
    }
}
