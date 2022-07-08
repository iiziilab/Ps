using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Employee : BaseModel
    {
        [Key]
        public long EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }

        [ForeignKey("ClientCompany")]
        public long ClientId { get; set; }
        public virtual ClientCompany ClientCompany { get; set; }

        [ForeignKey("Role")]
        public int roleid { get; set; }
        public virtual Role Role { get; set; }
        public bool ChangePassword { get; set; }
        public bool EditProject { get; set; }
        public bool ViewProject { get; set; }
    }

    public class UpdatePermission
    {
        public long id { get; set; }
        public bool ChangePassword { get; set; }
        public bool EditProject { get; set; }
        public bool ViewProject { get; set; }
    }
    public class UpdateEmp
    {
        public long id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ContactNo { get; set; }
    }
}
