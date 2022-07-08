using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PS.Utils
{
    public class ResponseMessage
    {
        public object data { get; set; }
        public int StatusCode { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        //public UserPermission userPermission { get; set; }
        public RolePermission rolePermission { get; set; }
    }
}
