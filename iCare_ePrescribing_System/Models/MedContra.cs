using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace iCare_ePrescribing_System.Models
{
    public class MedContra
    {
        //Med-Interaction tables between active ingredients
        [Key]
        public int MedInterationID
        {
            get;
            set;
        }

        //Active Ingredients
        public int CurrentActiveID { get; set; }
        [ForeignKey("CurrentActiveID")]
        public virtual ActiveIngredient ActiveIngredient { get; set; }

        public int InteratingActiveID { get; set; }
        [ForeignKey("InteratingActiveID")]
        public virtual ActiveIngredient ActiveInteraction { get; set; }
    }
}
