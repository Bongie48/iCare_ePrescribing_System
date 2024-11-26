using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class MedInteraction
    {
        //Contra-Indication table
        [Key]
        public int MedInteractionID
        {
            get;
            set;
        }

        //Active Ingredients
        public int InteractionActiveID { get; set; }
        [ForeignKey("InteractionActiveID")]
        public virtual ActiveIngredient ActiveIngredient { get; set; }

        //Condition
        public int MedicalConditionID { get; set; }
        [ForeignKey("MedicalConditionID")]
        public virtual Condition Condition { get; set; }
    }
}
