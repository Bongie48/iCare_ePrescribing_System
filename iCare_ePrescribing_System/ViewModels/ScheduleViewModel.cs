using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels
{
    public class ScheduleViewModel
    {
        public Schedule Schedule { get; set; }
        public IEnumerable<Schedule> schedulelist { get; set; }
    }
}
