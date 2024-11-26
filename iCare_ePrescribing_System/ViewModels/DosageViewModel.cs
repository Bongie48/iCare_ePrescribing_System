using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels
{
    public class DosageViewModel
    {
        public DosageForm dosage { get; set; }
        public IEnumerable<DosageForm> dosagelist { get; set; }
    }
}
