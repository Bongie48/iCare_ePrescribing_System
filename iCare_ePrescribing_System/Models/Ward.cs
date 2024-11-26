using System.ComponentModel.DataAnnotations;

namespace iCare_ePrescribing_System.Models
{
    public class Ward
    {
        [Key]
        public int WardID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name="Number Of Beds")]
        public int NoOfBeds { get; set; }

        //Nav prop
        public virtual ICollection<Bed> Beds { get; set; }
    }
}
