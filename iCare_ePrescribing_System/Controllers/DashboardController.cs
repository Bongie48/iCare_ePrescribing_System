using iCare_ePrescribing_System.Data;
using iCare_ePrescribing_System.Models;
using iCare_ePrescribing_System.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace iCare_ePrescribing_System.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _Context;
        private readonly UserManager<StaffMembers> _userManager;
        public DashboardController(
            ApplicationDbContext context,
            UserManager<StaffMembers> userManager) 
        {
            _userManager = userManager;
            _Context = context;
        }
        public async Task<IActionResult> Pharmacist()
        {
            PDashboardVM dashboardVM = new PDashboardVM();
            dashboardVM.UrgentPrescriptionCount = 0;
            dashboardVM.ActivePrescriptionCount= 0;
            dashboardVM.LimitReachCount = 0;
            dashboardVM.ActiveOrderCount = 0;
            List<Prescription> prescriptions= _Context.Prescriptions.ToList();
            List<AddMedication>medication=_Context.AddMedication.ToList();
            List<MedicationOrder> orders=_Context.MedicationOrders.ToList();
            dashboardVM.Fullname = _Context.Staff.ToList();
            for(int i=0;i<prescriptions.Count();i=i+1)
            {
                if (prescriptions[i].UrgentStatus == 'y' && prescriptions[i].TrackStatus == "active")
                {
                    dashboardVM.UrgentPrescriptionCount++;
                }
                if (prescriptions[i].TrackStatus == "active" )
                {
                    dashboardVM.ActivePrescriptionCount += 1;
                }
            }
            for (int x = 0; x < medication.Count(); x = x + 1)
            {
                if (medication[x].ReOrderStatus == 'y')
                {
                    dashboardVM.LimitReachCount ++;
                }
            }
            for(int y=0; y < orders.Count(); y++)
            {
                if (orders[y].OrderStatus == "sent")
                {
                    dashboardVM.ActiveOrderCount++;
                }
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            TempData["FullName"] = user.FullName;
            TempData["image"] = user.image;
            dashboardVM.userID = user.Id;
            dashboardVM.profile = user.image;
            return View(dashboardVM);
        }
        public IActionResult Surgeon()
        {
            
            return View();
        }
        public IActionResult Nurse()
        {
            return View();
        }

        public async Task<IActionResult> Admin()
        {
            AdminDashboardVM dashboardVM = new AdminDashboardVM();
            dashboardVM.TotalUsers = 0;
            dashboardVM.TotalPrescruptions = 0;
            dashboardVM.TotalBeds = 0;
            dashboardVM.TotalPatients = 0;
            List<RegisteredStaff> registeredStaff=_Context.Staff.ToList();
            List<Prescription> prescriptions=_Context.Prescriptions.ToList();
            List<Bed> beds=_Context.Bed.ToList();
            List<Patient> patients=_Context.Patients.ToList();
            for(int x = 0; x < registeredStaff.Count(); x++)
            {
                dashboardVM.TotalUsers++;
            }
            for(int y = 0;y< prescriptions.Count(); y++)
            {
                dashboardVM.TotalPrescruptions++;
            }
            for(int x = 0;x < beds.Count(); x++)
            {
                dashboardVM.TotalBeds++;
            }
            for(int y = 0; y< patients.Count(); y++)
            {
                dashboardVM.TotalPatients++;
            }
            return View(dashboardVM);
        }
    }
}
