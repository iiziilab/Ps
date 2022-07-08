using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Cell
    {
        [Key]
        public long cellId { get; set; }
        [ForeignKey("Project")]
        public int projectId { get; set; }
        public virtual Project Project { get; set; }
        public string cellName { get; set; }
    }
}
