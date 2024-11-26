using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels
{
    public class RejectPrescriptionVM
    {
        public int RejectedPrescriptionID { get; set; }
        public string DoctorEmailAddress { get;set; }
        public RejectPrescription RejectPrescription { get; set; }
        public Prescription prescription { get; set; }
    }
}
