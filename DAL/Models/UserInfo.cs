using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserInfo : BaseModel
    {
        [Key]
        public int userid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string[] Designation { get; set; }
        public int[] roleId { get; set; }
        [ForeignKey("Status")]
        public int statusId { get; set; }
        public virtual Status Status { get; set; }
        public string Password { get; set; }
    }
}
