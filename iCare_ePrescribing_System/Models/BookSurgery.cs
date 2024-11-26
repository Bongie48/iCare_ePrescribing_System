using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
//using AspNetCore;

namespace iCare_ePrescribing_System.Models
{
    public class BookSurgery
    {
        [Key]
        public int SurgeryId { get; set; }
        [Required]
        public int SurgeonID { get; set; }
        [Required]
        public int PatientId { get; set; }
        public DateTime SurgeryDate { get; set; }
        public string Session { get; set; }
        public int? AnaesthesiologistId { get; set; }
        public int? TheatreId { get; set; }
        public int TreatmentCode { get; set; }

        public Surgeon Surgeon { get; set; }
        public Patient Patient { get; set; }
        public Anaesthesiologist? Anaesthesiologist { get; set; }
        public Theatre? Theatre { get; set; }

        public string Status { get; set; }
    }
}
