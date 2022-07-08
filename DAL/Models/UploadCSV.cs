using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UploadCSV
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("Project")]
        public int projectId { get; set; }
        public virtual Project Project { get; set; }
        [ForeignKey("Cell")]
        public long cellId { get; set; }
        public virtual Cell Cell { get; set; }
        public string FileURL { get; set; }
        public string[] result { get; set; }
        public string data_1 { get; set; }
        public string seg_1 { get; set; }
        public string weights_1 { get; set; }
    }
}
