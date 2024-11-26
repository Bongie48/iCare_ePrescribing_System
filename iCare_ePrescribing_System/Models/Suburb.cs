using System.ComponentModel.DataAnnotations;

namespace iCare_ePrescribing_System.Models
{
    public class Suburb
    {
        [Key]
        public int SuburbId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PostalCode { get; set; }

        // nav prop
        public int CityId { get; set; }
        public City City { get; set; }


    }
}
