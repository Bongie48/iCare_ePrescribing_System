using System.ComponentModel.DataAnnotations;
namespace iCare_ePrescribing_System.Models
{
    public class Anaesthesiologist
    {
        [Key]
        public int AnaesthesiologistID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
