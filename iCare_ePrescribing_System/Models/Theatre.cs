using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.Models
{
    public class Theatre
    {
        [Key]
        public int TheatreId { get; set; }
        [Required]
        public string TheatreName { get; set; }
        public string Location { get; set; }
    }
}
