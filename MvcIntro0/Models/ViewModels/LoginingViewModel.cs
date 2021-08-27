using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
