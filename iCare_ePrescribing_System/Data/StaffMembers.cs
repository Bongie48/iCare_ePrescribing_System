using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace iCare_ePrescribing_System.Data
{
    public class StaffMembers:IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }=DateTime.Now;
        [Required]
        public string HealthCouncilRegistrationNumber { get; set; }
        public string? PasswordOne { get; set; }
        public string? ConfirmPasswordOne { get; set; }
        //public string PhotoUrl { get; set; }
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }
        public string? image { get; set; }
        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
    }
}
