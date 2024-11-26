using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels
{
    public class VitalRangeVM
    {
        public Vitals Vitals { get; set; }
        public IEnumerable<Vitals> VitalsList { get; set; }
    }
}
