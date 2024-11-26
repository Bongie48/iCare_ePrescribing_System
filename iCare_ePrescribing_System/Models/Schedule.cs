using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace iCare_ePrescribing_System.Models
{
    public class Schedule
    {
        
        [Key]
        public int ScheduleId { get; set; }
        [Required]
        public int ScheduleName { get; set; }
    }
}
