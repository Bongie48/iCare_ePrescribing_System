using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class Allergies
    {
        [Key]
        public int AllegyId { get; set; }
        [Required]
        public string Name { get; set; }

        // nav prop
        public Patient Patient { get; set; }
        public int PatientId { get; set; }

        public ActiveIngredient ActiveIngredient { get; set; }
        public int ActiveID { get; set; }

    }
}
