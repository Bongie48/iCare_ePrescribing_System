using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCare_ePrescribing_System.ViewModels
{
    public class PharmaPrescriptionVM
    {
        public Prescription prescription { get; set; }
        public IEnumerable<Prescription> PrescriptionList { get; set; }
        public int prescriptionID { get; set; }
        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string Priority { get; set; }
        public char urgency { get; set; }
        public string trackStatus { get; set; }
        public int prescriptionid { get; set; }
        public string loggeduser { get;set; }
        public string DoctorName { get; set; }    
        public string PatientName { get; set; }
        public string DisperseReason { get; set; }

        //Get admission ID through patient ID
        public int AdmittedPatientID { get; set; }
        public IEnumerable<PatientVitals> patientvitals { get;set; }
        public IEnumerable<Allergies> patientallergies { get;set; }
        public IEnumerable<PatientConditions> conditions { get;set; }
        public IEnumerable<PatientMedication> medicinelist { get;set; }
        public IEnumerable<PrescriptionItem> prescriptionitems { get;set; }

        //Get active ingredients
        public List<MedicationIngredient> PrescriptionActive { get; set; }
        public List<AdminMedIngredients> MedicationActive { get; set; }
        public List<MedInteraction> contraList { get;set; }
        public List<MedContra> medInteractionList { get;set; }

        //Patient Actives
        public List<MedContra> PatientMedActive { get; set; }
       
        //Reject Prescription
        public int RejectedPrescriptionID { get; set; }
        public string DoctorEmailAddress { get; set; }
        public RejectPrescription RejectPrescription { get; set; }
        public string profile { get; set; }

        //To Email Address
        public IEnumerable<SelectListItem> ToEmailAddress { get; set; }
        public string EmailText { get;set; }

        //local lists
        public MedicationIngredient PMed { get; set; }
        public AdminMedIngredients AMed { get; set; }
        public PatientConditions Pconditions { get; set; }
    }
}
