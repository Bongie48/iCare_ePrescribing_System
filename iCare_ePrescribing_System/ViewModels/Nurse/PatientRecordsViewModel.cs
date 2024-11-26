using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels.Nurse
{
    public class PatientRecordsViewModel
    {
        public DateTime? SelectedDate { get; set; }
        public IEnumerable<Patient> patients { get; set; }

    }
}
