using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserCredential
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        //public string Role { get; set; }
        //public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Username { get; set; }
        //public string Token { get; set; }
    }
}
