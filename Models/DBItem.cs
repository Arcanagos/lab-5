using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class DBItem
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
