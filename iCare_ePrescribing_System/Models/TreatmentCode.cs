using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class TreatmentCode
    {

        [Key]
        public int TreatmentCodeId { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
