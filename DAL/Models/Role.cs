using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Role
    {
        [Key]
        public int roleId { get; set; }
        public string roleName { get; set; }
        [ForeignKey("Status")]
        public int statusId { get; set; }
        public virtual Status Status { get; set; }
    }
}
