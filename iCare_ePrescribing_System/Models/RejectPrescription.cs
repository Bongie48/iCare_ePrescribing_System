using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class RejectPrescription
    {
        [Key]
        public int RejectId { get; set; }
        [Required]
        public string RejectReason { get; set; }
        [Required]
        public DateTime RejectDate { get; set; }

        //Get Surgeon Email Address
        public int RejectedPresID { get; set; }
        [ForeignKey("RejectedPresID")]
        public virtual Prescription Prescription { get; set; }
    }
}
