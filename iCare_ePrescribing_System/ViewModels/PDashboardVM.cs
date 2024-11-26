using iCare_ePrescribing_System.Data;
using iCare_ePrescribing_System.Models;

namespace iCare_ePrescribing_System.ViewModels
{
    public class PDashboardVM
    {
        public int ActivePrescriptionCount { get; set; }
        public int UrgentPrescriptionCount { get; set; }
        public int LimitReachCount { get; set; }
        public int ActiveOrderCount { get; set; }
        public string userID { get; set; }
        public string profile { get; set; }
        public IEnumerable<RegisteredStaff> Fullname {get;set;}
        public IEnumerable<StaffMembers> StaffMembers { get; set; }
    }
}
