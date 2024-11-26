using System.ComponentModel.DataAnnotations;

namespace iCare_ePrescribing_System.Models
{
    public class Bed
    {
        [Key]
        public int BedID { get; set; }
        [Required]
        public int BedNo { get; set; }
        public string Status { get; set; }



        //Navigation propersties
        public int WardID { get; set; }
        public Ward Ward { get; set; }

    }
}
