using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestProj.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Your UserName")]
        [StringLength(4, ErrorMessage = "UserName should be min four characters and max 20 characters.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter Your Password")]
        [StringLength(4, ErrorMessage = "Password should be min four characters and max 20 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter Your Email")]
        [RegularExpression(@"^[\w-\._\+%]+@(?:[\w-]+\.)+[\w]{2,6}$", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

        public bool Admin { get; set; }

        public bool Active { get; set; }
    }
}