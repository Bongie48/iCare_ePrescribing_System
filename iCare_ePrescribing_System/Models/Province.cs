using System.ComponentModel.DataAnnotations;

namespace iCare_ePrescribing_System.Models
{
    public class Province
    {
        [Key]
        public int ProvinceId { get; set; }
        [Required]
        public string Name { get; set; }

        // nav prop
        public ICollection<City> City { get; set; }
    }
}
