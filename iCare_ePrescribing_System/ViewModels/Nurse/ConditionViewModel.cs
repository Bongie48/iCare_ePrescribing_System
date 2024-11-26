using System;
using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels.Nurse;

public class ConditionViewModel
{
    public Patient Patient { get; set; }
    // A list of PatientConditions that will be bound to the form
    public List<PatientConditions> PatientConditions { get; set; } = new List<PatientConditions>();
    public ICollection<Condition> Conditions { get; set; }



}
