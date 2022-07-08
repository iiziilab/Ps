using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User :BaseModel
    {
        [Key]
        public int id { get; set; }
        [ForeignKey("ClientCompany")]
        public long ClientId { get; set; }
        public virtual ClientCompany ClientCompany { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        [ForeignKey("Role")]
        public int roleid { get; set; }
        public virtual Role Role { get; set; }
    }
}
