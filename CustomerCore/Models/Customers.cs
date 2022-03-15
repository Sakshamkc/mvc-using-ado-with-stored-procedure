using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerCore.Models
{
    public class Customers
    {
        [Key]

        public int CustomerID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string Firstname { get; set; }

        [Required]
        [DisplayName("Middle Name")]
        public string Middlename { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string Lastname { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
