using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PieShop.Models
{
    public class Customer
    {
        [BindNever]
        public int Id { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
