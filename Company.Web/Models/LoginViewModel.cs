using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter Your UserName")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter Your Password")]        
        public string Password { get; set; }
    }
}