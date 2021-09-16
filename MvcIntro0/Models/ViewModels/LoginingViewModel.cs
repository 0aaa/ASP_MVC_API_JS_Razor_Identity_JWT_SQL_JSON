using System.ComponentModel.DataAnnotations;

namespace MvcIntro0.Models.ViewModels
{
    public class LoginingViewModel
    {
        [Required(ErrorMessage ="Required")]
        public string Name { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Required")]
        public string Password { get; set; }


        public string ReturnURL { get; set; }
    }
}
