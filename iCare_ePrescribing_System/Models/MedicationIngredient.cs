using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class MedicationIngredient
    {
        [Key]
        public int MedIngredientId { get; set; }
        public int activeid { get; set; }
        //Getting Ingredients
        [ForeignKey("activeid")]
        public virtual ActiveIngredient ActiveIngredient { get; set; }
        public int medicineid { get; set; }
        //Getting Medication
        [ForeignKey("medicineid")]
        public virtual AddMedication AddMedication { get; set; }
        [Required]
        public double Strength { get; set; }
        //Getting active name and medicine
        public virtual List<MedicationIngredient> prescriptionItemsActive { get; set; } = new List<MedicationIngredient>();
    }
}
