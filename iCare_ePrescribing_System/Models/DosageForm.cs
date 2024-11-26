using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace iCare_ePrescribing_System.Models
{
    public class DosageForm
    {
        [Key]
        public int DosageID { get; set; }
        [Required]
        public string DosageName { get; set; }

        // nav prop 
        public ICollection<Medication> Medication { get; set; }
    }
}
