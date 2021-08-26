using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcIntro0.Models.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage ="Required")]
        public string Name { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Required")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Wrong input")]
        public string PasswordReiteration { get; set; }
    }
}
