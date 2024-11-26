using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class RegisteredStaff
    {
        [Key]
        public int StaffId { get; set; }
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
        public DateTime BirthDate { get; set; } = DateTime.Now;
        [Required]
        public string HealthCouncilRegistrationNumber { get; set; }
        [Required]
        public string roletext { get; set; }
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }
        public string? image { get; set; }
    }
}
