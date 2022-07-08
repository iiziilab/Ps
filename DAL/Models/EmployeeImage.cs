using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class EmployeeImage
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public long EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public byte[] Image { get; set; }
    }
}
