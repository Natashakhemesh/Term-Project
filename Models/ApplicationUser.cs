using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Wheels_Away.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Driving license")]
        public int DrivingLicense { get; set; }
        [Display(Name = "Age")]
        public int Age { get; set; }
    }
}
