using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ClientCredential
    {
        [Key]
        public long Id { get; set; }
        [ForeignKey("ClientCompany")]
        public long ClientId { get; set; }
        public virtual ClientCompany ClientCompany { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
