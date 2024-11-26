using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class PatientConditions
    {
        [Key]
        public int PatientConditionsId { get; set; }
        public DateTime Date { get; set; }

        // van prop
        public Patient Patient { get; set; }
        public int PatientId { get; set; }

        public Condition Condition { get; set; }
        public int ConditionID { get; set; }

        // Getting Conditions
        [NotMapped]
        public virtual List<Condition> conditions { get; set; } = new List<Condition>();
    }
}
