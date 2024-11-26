using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace iCare_ePrescribing_System.Models
{
    public class Surgeon
    {
        [Key]
        public int SurgeonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ContactNumber { get; set;}
        public string Email { get; set; }
        public string Specialization { get; set; }
        public string HealthCounsilRegistrationNumber { get; set; }
    }
}
