using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace iCare_ePrescribing_System.Models
{
    public class ActiveIngredient
    {
        [Key]
        public int ActiveID { get; set; }
        [Required]
        public string ActiveName { get; set; }

        // nav prop
        public ICollection<Allergies> Allergies { get; set; }
    }
}
