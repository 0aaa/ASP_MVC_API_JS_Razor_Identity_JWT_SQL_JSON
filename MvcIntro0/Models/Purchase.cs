using System.ComponentModel.DataAnnotations;

namespace MvcIntro0.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }


        [Required(ErrorMessage ="Required")]
        [StringLength(20, ErrorMessage = "Wrong length", MinimumLength = 2)]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Required")]
        [StringLength(20, ErrorMessage = "Wrong length", MinimumLength = 2)]
        public string Lastname { get; set; }


        [EmailAddress(ErrorMessage = "Wrong input")]
        [StringLength(20, ErrorMessage = "Wrong length", MinimumLength = 2)]
        public string Email { get; set; }


        [Phone(ErrorMessage = "Wrong input")]
        [StringLength(20, ErrorMessage = "Wrong length", MinimumLength = 2)]
        public string Phone { get; set; }


        public int BikeId { get; set; }

        public Bike Velo { get; set; }
    }
}
