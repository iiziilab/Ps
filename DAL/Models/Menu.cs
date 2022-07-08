using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Menu
    {
        [Key]
        public int ID { get; set; }
        public int Consideration { get; set; }
        public int Include { get; set; }
        public string Category { get; set; }
        public int CategoryIndex { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        [ForeignKey("Project")]
        public int projectId { get; set; }
        public virtual Project Project { get; set; }
        public int? cellId { get; set; }
        public bool isExpanded { get; set; }
        public int ForcedIn { get; set; }
    }

    public class MenuModel
    {
        public Menu[] Menu { get; set; }
        public string projectId { get; set; }
        public string cellId { get; set; }
    }

}
