using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App3.Models
{
    public class Client
    {
        [Key]
        public int id { get; set; }
        public string ClientName { get; set; }       
        public int ClientID   { get; set; }
        public string PhoneNumber { get; set; }
        public string  Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
