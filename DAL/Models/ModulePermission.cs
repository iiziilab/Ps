using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ModulePermission
    {
        [Key]
        public int modulepermissionId { get; set; }
        public string ModuleName { get; set; }
        [ForeignKey("Status")]
        public int statusId { get; set; }
        public virtual Status Status { get; set; }

    }
}
