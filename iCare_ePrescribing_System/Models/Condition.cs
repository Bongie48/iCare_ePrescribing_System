using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class Condition
    {
        [Key]
        public int ConditionID { get;set; }
        public string ConditionCode { get;set; }
        public string ConditionName { get; set; }

        // nav prop
        public ICollection<PatientConditions> PatientConditions { get; set; }
    }
}
