using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class RolePermission
    {
        [Key]
        public int rolepermissionId { get; set; }
        //public string Email { get; set; }
        //public string Role { get; set; }
        [ForeignKey("Role")]
        public int roleId { get; set; }
        public virtual Role Role { get; set; }
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
        public Boolean UserInsert { get; set; }
        public Boolean UserUpdate { get; set; }
        public Boolean UserDelete { get; set; }
        public Boolean UserList { get; set; }
        public Boolean UserDetails { get; set; }
        public Boolean RoleInsert { get; set; }
        public Boolean RoleUpdate { get; set; }
        public Boolean RoleDelete { get; set; }
        public Boolean RoleList { get; set; }
        public Boolean RoleDetails { get; set; }
        public Boolean PermissionInsert { get; set; }
        public Boolean PermissionUpdate { get; set; }
        public Boolean PermissionDelete { get; set; }
        public Boolean PermissionList { get; set; }
        public Boolean PermissionDetails { get; set; }
        public Boolean CellList { get; set; }
        public Boolean Upload { get; set; }
        public Boolean Menu { get; set; }
    }
}
