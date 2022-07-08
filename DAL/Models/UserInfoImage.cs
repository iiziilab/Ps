using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserInfoImage
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserInfo")]
        public int userid { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public byte[] Image { get; set; }
    }
}
