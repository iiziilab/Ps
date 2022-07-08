using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserPermission
    {
        [Key]
        public int userpermissionId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        //[ForeignKey("UserInfo")]
        //public int userId { get; set; }
        //public virtual UserInfo UserInfo { get; set; }
        public Boolean ClientInsert { get; set; }
        public Boolean ClientUpdate { get; set; }
        public Boolean ClientDelete { get; set; }
        public Boolean ClientList { get; set; }
        public Boolean ClientDetails { get; set; }
        public Boolean ProjectInsert { get; set; }
        public Boolean ProjectUpdate { get; set; }
        public Boolean ProjectDelete { get; set; }
        public Boolean ProjectList { get; set; }
        public Boolean ProjectDetails { get; set; }
    }
}
