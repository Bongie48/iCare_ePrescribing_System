using System.ComponentModel.DataAnnotations;
namespace iCare_ePrescribing_System.Models
{
    public class TestingDB
    {
        [Key]
        public int TestID { get; set; }
        [Required] public string Name { get; set; }
    }
}
