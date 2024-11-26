using System.ComponentModel.DataAnnotations;

namespace iCare_ePrescribing_System.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }

        // nav prop
        public int ProvinceId { get; set; }
        public Province Province { get; set; }

        public ICollection<Suburb> Suburb { get; set; }
    }
}
