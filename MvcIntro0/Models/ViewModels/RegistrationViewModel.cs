using System.ComponentModel.DataAnnotations;

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
