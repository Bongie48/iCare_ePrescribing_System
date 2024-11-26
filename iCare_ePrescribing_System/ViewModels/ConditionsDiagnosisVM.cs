using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels
{
    public class ConditionsDiagnosisVM
    {
        public Condition Condition { get; set; }
        public IEnumerable<Condition> ConditionList{ get; set; }
    }
}
