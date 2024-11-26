using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class Vitals
    {
        [Key]
        public int VitalId { get; set; }
        public string Name { get; set; }
        public double Lowlimit { get; set; }
        public double Highlimit { get; set; }

        // nav prop
        public ICollection<PatientVitals> PatientVitals { get; set; }
    }
}
