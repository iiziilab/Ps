using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ClientCompany : BaseModel
    {
        [Key]
        public long ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        [Column(TypeName="varchar(7)")]
        public string Pincode { get; set; }
        //[Column(TypeName = "varchar(18)")]
        public string Contact1 { get; set; }
        //[Column(TypeName = "varchar(18)")]
        public string Contact2 { get; set; }
        public string Description { get; set; }
    }
}
