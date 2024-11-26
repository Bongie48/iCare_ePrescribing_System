using iCare_ePrescribing_System.Data;
using iCare_ePrescribing_System.Models;
using iCare_ePrescribing_System.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using iCare_ePrescribing_System.Repository;
using System.Runtime.CompilerServices;
using System.Collections;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Identity;
using System.Data;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System.IO;
using iText.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer; 
using Microsoft.CodeAnalysis.CSharp.Syntax;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iCare_ePrescribing_System.ViewModels.Nurse;

namespace iCare_ePrescribing_System.Controllers
{
    public class PharmaController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _webHost;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<StaffMembers> _userManager;
        public PharmaController(
            ApplicationDbContext dbContext,
            IEmailSender emailSender,
            IWebHostEnvironment webHost,
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            UserManager<StaffMembers> userManager)
        {
            this.dbContext = dbContext;            this._emailSender = emailSender;
            _webHost = webHost;
            _roleRepository= roleRepository;
            _userRepository= userRepository;
            _userManager = userManager;
        }
        //Getting active Prescription
        public async Task<IActionResult> Prescription()
        {
            PrescriptionVM prescriptionVM = new PrescriptionVM();
            var list = await dbContext.Prescriptions
           .Include(x => x.RegisteredStaff)
           .Include(y=>y.Patient)
           .Where(x => x.TrackStatus == "active")
           .OrderByDescending(x=>x.DateIssued)
           .ToListAsync();
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> Prescription(string recordSearh)
        {
            ViewData["SearchRecord"] = recordSearh;
            var record = from x in dbContext.Prescriptions
                         .Include(x => x.Patient)
                         .Include(x => x.RegisteredStaff)
                         .Where(x => x.TrackStatus == "active")
                         .OrderByDescending(x=>x.DateIssued)
                         select x;
            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.UrgentStatus.ToString().Contains(recordSearh));
            }
            return View(await record.AsNoTracking().ToListAsync());
        }
        //Get by ID--update method
        public async Task <IActionResult> Despense(int? ID)
        {
            PharmaPrescriptionVM pharmaPrescription = new PharmaPrescriptionVM();
            try
            {
               
                PharmaPrescriptionVM pharmaPrescriptionVM = new PharmaPrescriptionVM();
                pharmaPrescription.RejectPrescription = new RejectPrescription();

                int HoldID = 0;
                if (ID == null || ID == 0)
                {
                    return NotFound();
                }

                var dbC = dbContext.Prescriptions.Find(ID);

                if (dbC == null)
                {
                    return NotFound();
                }

                pharmaPrescription.prescriptionid = dbC.PrescriptionId;
                pharmaPrescription.prescriptionid = dbC.PrescriptionId;
                pharmaPrescription.DoctorID = dbC.MemberId;
                pharmaPrescription.PatientID = dbC.PatientId;
                pharmaPrescription.PrescriptionDate = dbC.DateIssued;
                pharmaPrescription.urgency = dbC.UrgentStatus;

                if (dbC.UrgentStatus == 'y')
                {
                    pharmaPrescription.Priority = "Urgent";
                }
                else
                {
                    pharmaPrescription.Priority = "Not Urgent";
                }

                //Get Doctor and Patient name
                List<RegisteredStaff> stafflist = dbContext.Staff.ToList();
                for (int x = 0; x < stafflist.Count(); x = x + 1)
                {
                    if (stafflist[x].StaffId == dbC.MemberId)
                    {
                        pharmaPrescription.DoctorName = stafflist[x].Name + " " + stafflist[x].Surname;
                    }
                }

                var patients = dbContext.Patients.Find(dbC.PatientId);
                pharmaPrescription.PatientName = dbC.Patient.Name + " " + dbC.Patient.Surname;

                IEnumerable<Admission> addmission = dbContext.Admission.ToList();
                List<Admission> admissionlist = dbContext.Admission.ToList();

                for (int x = 0; x < admissionlist.Count(); x = x + 1)
                {
                    if (admissionlist[x].PatientID == dbC.PatientId)
                    {
                        pharmaPrescription.AdmittedPatientID = admissionlist[x].AdmisionID;
                    }
                }

                IEnumerable<PatientVitals> vitallist = dbContext.PatientVitals
                    .Where(x => x.AdmisionID == pharmaPrescription.AdmittedPatientID)
                    .Include(x => x.Vitals)
                    .OrderByDescending(x => x.CaptureTime);
                pharmaPrescription.patientvitals = vitallist;

                IEnumerable<Allergies> allergylist = dbContext.Allergies
                    .Where(x => x.PatientId == pharmaPrescription.PatientID)
                    .OrderBy(x => x.Name);
                pharmaPrescription.patientallergies = allergylist;

                IEnumerable<PatientConditions> conditionlist = dbContext.PatientConditions
                    .Where(x => x.PatientId == pharmaPrescription.PatientID)
                    .Include(x => x.Condition)
                    .OrderByDescending(x => x.Date);
                pharmaPrescription.conditions = conditionlist;

                IEnumerable<PatientMedication> medlist = dbContext.PatientMedication
                    .Where(x => x.PatientId == dbC.PatientId)
                    .OrderBy(x => x.Medication.MedicationName)
                    .Include(x => x.Medication)
                    .ThenInclude(x => x.DosageForm);
                pharmaPrescription.medicinelist = medlist;

                //Get prescribed medication
                pharmaPrescription.prescriptionitems = dbContext.PrescriptionItem
                    .OrderBy(x => x.medication.MedicationName)
                    .Include(x => x.medication)
                    .ThenInclude(x => x.DosageForm)
                    .Where(x => x.PresID == dbC.PrescriptionId)
                    .ToList();

                //Active ingre on prescr med
                pharmaPrescription.PrescriptionActive = dbContext.MedicationIngredients
                    .OrderBy(x => x.ActiveIngredient.ActiveName)
                    .Include(x => x.ActiveIngredient)
                    .Include(x => x.AddMedication)
                    .ThenInclude(x => x.DosageForm)
                    .ToList();

                //Active ingre on med currently taken
                pharmaPrescription.MedicationActive = dbContext.AdminMedIngredients
                    .OrderBy(x => x.ActiveIngredient.ActiveName)
                    .Include(x => x.ActiveIngredient)
                    .ToList();

                //Contra
                pharmaPrescription.contraList = dbContext.MedInteraction
                    .Include(x => x.ActiveIngredient)
                    .Include(x => x.Condition)
                    .ToList();

                //Inter 
                pharmaPrescription.medInteractionList = dbContext.MedContra
                    .Include(x => x.ActiveIngredient)
                    .Include(x => x.ActiveInteraction)
                    .ToList();

                pharmaPrescription.EmailText = dbC.RegisteredStaff.Email;
                var doctorRole = await _roleRepository.GetRoleByNameAsync("Surgeon");
                if (doctorRole != null)
                {
                    var userIdsInDoctorRole = await dbContext.UserRoles
                        .Where(ur => ur.RoleId == doctorRole.Id)
                        .Select(ur => ur.UserId)
                        .ToListAsync();

                    List<SelectListItem> email = await dbContext.Users
                        .Where(n => userIdsInDoctorRole.Contains(n.Id))
                        .Where(n => n.Email != pharmaPrescription.EmailText)
                        .OrderBy(n => n.Name)
                        .Select(n => new SelectListItem
                        {
                            Value = n.Email.ToString(),
                            Text = $"{n.FullName} ({n.Email})"
                        })
                        .ToListAsync();

                    pharmaPrescription.ToEmailAddress = email;
                }
                else
                {
                    List<SelectListItem> email = await dbContext.Users
                       .Where(n => n.Email == pharmaPrescription.EmailText)
                       .OrderBy(n => n.Name)
                       .Select(n => new SelectListItem
                       {
                           Value = n.Email.ToString(),
                           Text = $"{n.FullName} ({n.Email})"
                       })
                       .ToListAsync();

                    pharmaPrescription.ToEmailAddress = email;
                }
               
            }
            catch (Exception) 
            {
                return View(pharmaPrescription);
            }
            return View(pharmaPrescription);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult>Despense(PharmaPrescriptionVM prescriptionVM)
        {
            prescriptionVM.prescription.DateIssued = prescriptionVM.prescription.DateIssued;
            dbContext.Prescriptions.Update(prescriptionVM.prescription);
            dbContext.SaveChanges();
            var dbC = dbContext.Prescriptions
                .Include(x => x.RegisteredStaff)
                .Include(x => x.Patient)
                .FirstOrDefault(x => x.PrescriptionId == prescriptionVM.prescription.PrescriptionId);
   
            List<PrescriptionItem> items = dbContext.PrescriptionItem
                .Where(x => x.PresID == prescriptionVM.prescription.PrescriptionId).Include(x => x.medication)
                .ToList();
            
            var existingMedications = dbContext.AddMedication
                .Where(m => items.Select(i => i.MedID)
                .Contains(m.MedicationId))
                .ToList();
            //Quantity
            for (int x=0; x < items.Count(); x = x + 1)
            {
                var addMedication = existingMedications.FirstOrDefault(m => m.MedicationId == items[x].MedID);
                addMedication.MedicationName = items[x].medication.MedicationName;
                addMedication.scheduleID = items[x].medication.scheduleID;
                addMedication.dosageID = items[x].medication.dosageID;
                addMedication.ReOrder = items[x].medication.ReOrder;
                addMedication.image = items[x].medication.image;
                addMedication.Quantity = items[x].medication.Quantity - items[x].Quantity;
                if (addMedication.Quantity <= addMedication.ReOrder)
                {
                    addMedication.ReOrderStatus = 'y';
                }
                else
                {
                    addMedication.ReOrderStatus = 'n';
                }
                dbContext.AddMedication.Update(addMedication);
            }
            
            dbContext.SaveChanges();

            var receiver =prescriptionVM.EmailText;
            var subject = "Reason For Avoiding Prescription Alerts---Group-14 (Pharmacist)";
            var Message = $"Group-14 (Pharmacist)\n" +
               $"\n" +
               $"Details for the rejected prescription:\n" +
               $"{"Patient Name:".PadRight(20)} {dbC.Patient.Name} {dbC.Patient.Surname}\n" +
               $"{"Priority:".PadRight(26)} {dbC.UrgentStatus}\n" +
               $"{"Date Issued:".PadRight(22)} {dbC.DateIssued.ToString("dd MMMM yyyy")}\n" +
               $"\n" +
               $"Reason: {prescriptionVM.DisperseReason}.\n" +
               $"\n" +
               $"Kind regards,\n" +
               $"Bay Breeze Day Hospital";
            try
            {
                await _emailSender.SendEmailAsync(receiver, subject, Message);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Prescription");
            }
            return RedirectToAction("Prescription");
        }
        //View inactive prescriptions
        public async Task<IActionResult> ViewDespensed()
        {
            IEnumerable<Prescription> list=dbContext.Prescriptions
                .OrderByDescending(x => x.DateIssued)
                .Include(x=>x.Patient)
                .Include(x=>x.RegisteredStaff)
                .Where(x=>x.TrackStatus== "Dispersed"
                || x.TrackStatus== "Rejected").ToList();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!list.Any())
            {
                TempData["error2"] = "No prescriptions found for the search";
            }
            TempData["FullName"] = user.FullName;
            TempData["image"] = user.image;
            return View(list);
        }
        [HttpGet]
        public async Task<IActionResult> ViewDespensed(string recordSearh)
        {
            ViewData["GetPrescriptionRecord"] = recordSearh;
            var record = from x in dbContext.Prescriptions
                         .OrderByDescending(x => x.DateIssued)
                         .Include(x => x.Patient)
                         .Include(x => x.RegisteredStaff)
                         .Where(x => x.TrackStatus != "active") select x;

            if (!String.IsNullOrEmpty(recordSearh))
            {
                record = record.Where(x => x.TrackStatus.Contains(recordSearh)
                ||x.Patient.Name.Contains(recordSearh)
                ||x.Patient.Surname.Contains(recordSearh)
                ||x.RegisteredStaff.Name.Contains(recordSearh)
                ||x.RegisteredStaff.Surname.Contains(recordSearh));
            }
            if (!record.Any())
            {
                TempData["error2"] = "No prescriptions found for the search";
            }
            return View(await record.AsNoTracking().ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectPres(PharmaPrescriptionVM reject)
        {
            string reason = reject.RejectPrescription.RejectReason;
            int PrescriptionID = reject.RejectPrescription.RejectedPresID;
            DateTime rejectiondate = reject.RejectPrescription.RejectDate;
            int ID = reject.prescription.PrescriptionId;
            string status = " ";

            var dbC = dbContext.Prescriptions
                .Include(x => x.RegisteredStaff)
                .Include(x => x.Patient)
                .FirstOrDefault(x => x.PrescriptionId == ID);

            reject.prescription.DateIssued = rejectiondate;
            dbContext.Entry(reject.prescription).State = EntityState.Deleted;
            dbContext.Prescriptions.Update(reject.prescription);
            dbContext.SaveChanges();

            if (dbC.UrgentStatus == 'y'||dbC.UrgentStatus=='Y')
            {
                status = "Urgent";
            }
            else
            {
                status = "Not Urgent";
            }

            var receiver = reject.EmailText;
            var subject = "Rejected Prescription---Group-14 (Pharmacist)";
            var Message = $"Group-14 (Pharmacist)\n" +
               $"\n" +
               $"Details for the rejected prescription:\n" +
               $"{"Patient Name:".PadRight(20)} {dbC.Patient.Name} {dbC.Patient.Surname}\n" +
               $"{"Priority:".PadRight(26)} {status}\n" +
               $"{"Date Issued:".PadRight(22)} {dbC.DateIssued.ToString("dd MMMM yyyy")}\n" +
               $"\n" +
               $"Reason: {reject.RejectPrescription.RejectReason}.\n" +
               $"\n" +
               $"Kind regards,\n" +
               $"Bay Breeze Day Hospital";
            try
            {
                await _emailSender.SendEmailAsync(receiver, subject, Message);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Email server is down, an email can't be sent at this time." +
                    " please send your notification via a professional document";
                return RedirectToAction("Prescription");
            }
            return RedirectToAction("Prescription");
        }
       
        //Adding medication
        public async Task<IActionResult> Medication()
        {
            PharmaMedicationVM medicationViewModel = new PharmaMedicationVM();
            medicationViewModel.AddMedication = new AddMedication();

            List<SelectListItem> dosageform=dbContext.DosageForm
                .OrderBy(n=>n.DosageName)
                .Select(n=>new SelectListItem { Value=n.DosageID.ToString(),Text=n.DosageName})
                .ToList();
            medicationViewModel.dosage=dosageform;

            List<SelectListItem> schedulelist = dbContext.Schedule
                .OrderBy(n => n.ScheduleName)
                .Select(n => new SelectListItem { Value = n.ScheduleId.ToString(), Text ="Schedule " +n.ScheduleName.ToString() })
                .ToList();
            medicationViewModel.schedule = schedulelist;

            return View(medicationViewModel);
        }
        [HttpPost]
        public IActionResult Medication(PharmaMedicationVM medication)
        {
            string uniqueFileName = null;
            IEnumerable<AddMedication> addedMedication= dbContext.AddMedication.ToList();
            foreach(var x in addedMedication)
            {
                if (medication.AddMedication.MedicationName == x.MedicationName)
                {
                    TempData["ErrorName"] = $"{medication.AddMedication.MedicationName} already exists! Use update button to edit medicaction info " +
                        $"or delete the medication";
                    return RedirectToAction("Medication");
                }
           
            }

            if (medication.AddMedication.Quantity <= medication.AddMedication.ReOrder)
            {
                medication.AddMedication.ReOrderStatus = 'y';
            }
            else
            {
                medication.AddMedication.ReOrderStatus = 'n';
            }
            medication.AddMedication.TrackStatus= 1;
            
            if (medication.AddMedication.ProfilePhoto != null)
            {
                // Allowed file types
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(medication.AddMedication.ProfilePhoto.FileName).ToLower();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    
                    TempData["ErrorMessage"] = "Invalid file format. Please upload an image in JPG, JPEG, or PNG format.";
                    return RedirectToAction("Medication");
                }
                
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + medication.AddMedication.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                medication.AddMedication.ProfilePhoto.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            medication.AddMedication.image = uniqueFileName;
            if (medication.AddMedication.ReOrder <=0)
            {
                TempData["Error"] = "Re-Order Level Must Greater Than Zero!";
                return RedirectToAction("Medication");
            }
            dbContext.Add(medication.AddMedication);
            dbContext.SaveChanges();
            return RedirectToAction("MedicineIngredients");
        }

        //Add active ingredients for medication
        public async Task<IActionResult> MedicineIngredients()
        {
            IngredientViewModel ingredientViewModel = new IngredientViewModel();
            IEnumerable<AddMedication> list = dbContext.AddMedication
                .Include(x => x.Schedule)
                .Include(x => x.DosageForm);

            IEnumerable<MedicationIngredient> ingredients = dbContext.MedicationIngredients
                .Include(x => x.ActiveIngredient);

            List<SelectListItem> ingredientname = dbContext.ActiveIngredients
                .OrderBy(n => n.ActiveName)
                .Select(n => new SelectListItem { Value = n.ActiveID
                .ToString(), Text = n.ActiveName }).ToList();

            ingredientViewModel.medicationIngredient = new MedicationIngredient();
            ingredientViewModel.ingredientname = ingredientname;
            ingredientViewModel.record = list;
            ingredientViewModel.Currentingredient = ingredients;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            TempData["FullName"] = user.FullName;
            ingredientViewModel.profile = user.image;
            return View(ingredientViewModel);
        }
        [HttpPost]
        public IActionResult MedicineIngredients(IngredientViewModel ingredient)
        {
            dbContext.Add(ingredient.medicationIngredient);
            dbContext.SaveChanges();
            TempData["MedicineIngredient"] = $"Active ingredient successfully added";
            return RedirectToAction("MedicineIngredients");
        }

        //Delete medicine
        public IActionResult DeleteMed(int? Id)
        {
            var dbC = dbContext.AddMedication.Find(Id);
            List<MedicationIngredient> MyList = dbContext.MedicationIngredients.Where(x => x.medicineid == dbC.MedicationId).ToList();
            if (dbC == null)
            {
                return NotFound();
            }
            dbContext.AddMedication.Remove(dbC);
            foreach (var x in MyList)
            {
                dbContext.MedicationIngredients.Remove(x);
            }
            dbContext.SaveChanges();
            return RedirectToAction("Medication");
        }

        //Deleting active ingredient for added medication
        public IActionResult DeleteMedIngredient(int? Id)
        {
            var dbC = dbContext.MedicationIngredients.Find(Id);
            if (dbC == null)
            {
                return NotFound();
            }
            dbContext.MedicationIngredients.Remove(dbC);
            dbContext.SaveChanges();
            TempData["DeleteIngredient"] = "You have successfully deleted an active ingredient!";
            return RedirectToAction("MedicineIngredients");
        }
        public IActionResult UpdateMedIngredient(int? Id)
        {
            var dbC = dbContext.MedicationIngredients.Find(Id);
            if (dbC == null)
            {
                return NotFound();
            }
            dbContext.MedicationIngredients.Remove(dbC);
            dbContext.SaveChanges();
            TempData["DeleteIngredient"] = "You have successfully deleted an active ingredient!";
            return RedirectToAction("UpdateMed", new { id = dbC.medicineid });
        }
        [HttpPost]
        public IActionResult UpdateIngredients(UpdateMedIngredientVM ingredient)
        {
            dbContext.Add(ingredient.medicationIngredient);
            dbContext.SaveChanges();
            return RedirectToAction("UpdateMed", new { id = ingredient.medicationIngredient.medicineid }); ;
        }
        //View medication
        public async Task<IActionResult> ViewMed()
        {
            GetMedicineViewModel getMedicine=new GetMedicineViewModel();
            IEnumerable<AddMedication> MedList = dbContext.AddMedication
                .Where(x=>x.TrackStatus==1)
                .OrderBy(x=>x.MedicationName)
                .Include(x=>x.DosageForm)
                .Include(x=>x.Schedule).ToList();

            IEnumerable<MedicationIngredient> ActiveList = dbContext.MedicationIngredients
                .OrderBy(x=>x.ActiveIngredient.ActiveName)
                .Include(x=>x.ActiveIngredient)
                .OrderBy(x => x.ActiveIngredient.ActiveName).ToList();

            getMedicine.medicationlist = MedList;
            getMedicine.ingredientlist= ActiveList;

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            TempData["FullName"] = user.FullName;
            getMedicine.profile = user.image;
            return View(getMedicine);
        }

        //Update medication master table
        public async Task <IActionResult> UpdateMed(int? ID)
        {
            UpdateMedIngredientVM med=new UpdateMedIngredientVM();
            med.medicationIngredient = new MedicationIngredient();
            if (ID == null || ID == 0)
            {
                return NotFound();
            }

            var dbC = dbContext.AddMedication.Find(ID);
            if (dbC == null)
            {
                return NotFound();
            }

            var ingredientslist = dbContext.MedicationIngredients
                .Include(x => x.ActiveIngredient)
                .Where(i => i.medicineid == ID).ToList();
            if (ingredientslist.Count() <= 1)
            {
                med.trackmedication = 1;
            }

            IEnumerable<MedicationIngredient> ingredients = dbContext.MedicationIngredients
                .Include(x => x.ActiveIngredient)
                .Where(i => i.medicineid == ID).ToList();
            med.medicationID = dbC.MedicationId;
            med.Name = dbC.MedicationName;
            med.quantity = dbC.Quantity;
            med.reorder = dbC.ReOrder;
            med.dosage = dbC.dosageID;
            med.schedule = dbC.scheduleID;
            med.image = dbC.image;

            List<SelectListItem> dosagelist = dbContext.DosageForm
                .OrderBy(n => n.DosageName)
                .Select(n => new SelectListItem { Value = n.DosageID.ToString(), Text = n.DosageName }).ToList();
            med.dosageform = dosagelist;

            List<SelectListItem> schedulelist = dbContext.Schedule
                .OrderBy(n => n.ScheduleName)
                .Select(n => new SelectListItem { Value = n.ScheduleId.ToString(), Text = "Schedule " + n.ScheduleName.ToString() }).ToList();
            med.Medschedule = schedulelist;

            med.activeid = ingredientslist.Select(i => i.activeid).ToList();
            med.active = ingredientslist;
            med.status = dbC.ReOrderStatus;

            List<SelectListItem> ingredientname = dbContext.ActiveIngredients
                .OrderBy(n => n.ActiveName)
                .Select(n => new SelectListItem
                {
                    Value = n.ActiveID
                .ToString(),
                    Text = n.ActiveName
                }).ToList();
            med.ingredientname = ingredientname;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            TempData["FullName"] = user.FullName;
            med.profile = user.image;
            return View(med);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateMed(UpdateMedIngredientVM record)
        {
            MedicationIngredient ingredient=new MedicationIngredient();
            AddMedication medication = new AddMedication();
            medication.MedicationName = record.Name;
            medication.ReOrder = record.reorder;
            medication.Quantity = record.quantity;
            medication.TrackStatus = 1;
            if (medication.Quantity <= medication.ReOrder)
            {
                medication.ReOrderStatus = 'y';
            }
            else
            {
                medication.ReOrderStatus = 'n';
            }

            medication.scheduleID = record.schedule;
            medication.dosageID = record.dosage;
            medication.MedicationId = record.medicationID;
            medication.image=record.image;
            dbContext.AddMedication.Update(medication);
            dbContext.SaveChanges();
            TempData["AlertMessage"] = medication.MedicationName +" successfully updated!";
            return RedirectToAction("ViewMed");
        }

        //Delete added medication
        public IActionResult DeleteMedication(int? Id)
        {
            var dbC = dbContext.AddMedication.Find(Id);
            List<MedicationIngredient> MyList = dbContext.MedicationIngredients
                .Where(x => x.medicineid==dbC.MedicationId).ToList();

            if (dbC == null)
            {
                return NotFound();
            }
            dbC.TrackStatus = 0;
            dbContext.AddMedication.Update(dbC);
            dbContext.SaveChanges();
            TempData["DeleteMedication"] = $"You have successfully deleted {dbC.MedicationName} ";
            return RedirectToAction("ViewMed");
        }
        
        //Order Medication
        public async Task<IActionResult> OrderMed()
        {
            GetOrderViewModel orderVM= new GetOrderViewModel();
            orderVM.stockuser = new UserStock();
            List<AddMedication> list = dbContext.AddMedication.Where(x=>x.ReOrderStatus=='y').Include(x => x.DosageForm).ToList();
            orderVM.QuickOrder = list;
            List<AddMedication> Allist = dbContext.AddMedication.Include(x => x.DosageForm).ToList();
            orderVM.medicinelist = Allist;

            foreach (var x in Allist)
            {
                orderVM.stockuser.medicationOrders.Add(new MedicationOrder());
            }

            foreach (var x in list)
            {
                orderVM.QuickOrderQuantity.Add(new MedicationOrder());
            }
            return View(orderVM);
        }
        [HttpPost]
        public async Task<IActionResult> OrderMed(GetOrderViewModel record)
        {
            List<AddMedication> list = dbContext.AddMedication.ToList();
            List<DayHospital> users = dbContext.DayHospitals.ToList();
            int userid = 0;
            var message = $"(Group14---Pharmacist)\n\nThe following medication has been ordered on the {record.order.OrderDate.ToString("dd MMMM yyyy")}:\n\n";
            string MedicationName = "";
            string emailaddress = " ";
            string SuccessMessage = " ";
            DateTime HoldDate = record.order.OrderDate;

            foreach (var stockuser in users)
            {
                userid = stockuser.Id;
                emailaddress = stockuser.PurchaseManagerEmail;
            }

            for (int z = 0; z < list.Count(); z = z + 1)
            {
                record.order = new MedicationOrder();
                record.order.OrderQuantity = record.stockuser.medicationOrders[z].OrderQuantity;
                if (record.order.OrderQuantity != 0)
                {
                    record.order.medicineid = list[z].MedicationId;
                    MedicationName = list[z].MedicationName;
                    record.order.OrderStatus = "sent";
                    record.order.OrderDate = HoldDate;
                    record.order.CurrentQuantity = list[z].Quantity;
                    record.order.StockID = userid;
                    message = message +"Medication Name:".PadRight(20)+ MedicationName+"\nQuantity Ordered:".PadRight(22) + record.order.OrderQuantity+ "\n\n";
                    SuccessMessage = SuccessMessage + MedicationName + "\t";
                    dbContext.Add(record.order);
                }
            }
            dbContext.SaveChanges();
            TempData["SuccessMessage"] = $"You have been successfully ordered medication! An email has been sent to {emailaddress} with your order.";
            
            var receiver = emailaddress;
            var subject = "Stock Ordered By Bay Breeze Day Hospital (Group14---Pharmacist)";
            var Message = message+"\nKind regards\nBay Breeze Day Hospital";
            try
            {
                await _emailSender.SendEmailAsync(receiver, subject, Message);
            }
            catch (Exception)
            {

                TempData["ErrorMessage"] = "Error sending email, an email can't be sent at this time. please send your notification via a professional document";
                return RedirectToAction("Stock");
            }
            return RedirectToAction("Stock");
        }
        //Quick Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuickOrder(GetOrderViewModel record)
        {
            List<AddMedication> Quicklist = dbContext.AddMedication.Where(x => x.ReOrderStatus == 'y').ToList();
            List<DayHospital> users = dbContext.DayHospitals.ToList();
            var message = $"(Group14---Pharmacist)\n\nThe following medication has been ordered on the {record.order.OrderDate.ToString("dd MMMM yyyy")}:\n\n";
            string MedicationName = "";
            string emailaddress = " ";
            string SuccessMessage = " ";
            int userid = 0;
            DateTime HoldDate = record.order.OrderDate;

            foreach (var stockuser in users)
            {
                userid = stockuser.Id;
                emailaddress = stockuser.PurchaseManagerEmail;
            }

            for (int z = 0; z < Quicklist.Count(); z = z + 1)
            {
                record.order = new MedicationOrder();
                record.order.OrderQuantity = record.QuickOrderQuantity[z].OrderQuantity;
                if (record.order.OrderQuantity != 0)
                {
                    record.order.medicineid = Quicklist[z].MedicationId;
                    MedicationName = Quicklist[z].MedicationName;
                    record.order.OrderStatus = "sent";
                    record.order.OrderDate = HoldDate;
                    record.order.CurrentQuantity = Quicklist[z].Quantity;
                    record.order.StockID = userid;
                    message = message + "Medication Name:".PadRight(20) + MedicationName + "\nQuantity Ordered:".PadRight(22) + record.order.OrderQuantity + "\n\n";
                    dbContext.Add(record.order);
                }
            }
            dbContext.SaveChanges();
            TempData["SuccessMessage"] = $"You have been successfully ordered medication! An email has been sent to {emailaddress} with your order.";

            var receiver = emailaddress;
            var subject = "Stock Ordered By Bay Breeze Day Hospital (Group14---Pharmacist)";
            var Message = message + "\nKind regards\nBay Breeze Day Hospital";
            try
            {
                await _emailSender.SendEmailAsync(receiver, subject, Message);
            }
            catch (Exception)
            {
                return RedirectToAction("Stock");
            }
            return RedirectToAction("Stock");
        }
        //Viewing  order history
        public async Task <IActionResult> Stock(DateTime? startDate)
        {
            var MyList = dbContext.MedicationOrders
                .Include(x => x.AddMedication)
                .ThenInclude(x => x.DosageForm)
                .Where(x=>x.OrderQuantity!=0)
                .OrderByDescending(x=>x.OrderDate).ToList();

            if (startDate.HasValue)
            {
                MyList = dbContext.MedicationOrders
                    .Where(p => p.OrderDate.Date == startDate.Value.Date)
                    .ToList();
            }
            if (!MyList.Any())
            {
                TempData["error3"] = "No order found for the specified date";
            }
            return View(MyList);
        }

        //Capture Stock Received
        public async Task<IActionResult> CaptureStock()
        {
            CaptureStockVM capturestockvm = new CaptureStockVM();
            capturestockvm.Medication = new AddMedication();
            capturestockvm.receivedStock = new ReceivedStock();

            var MyList = dbContext.AddMedication
                .Include(x => x.DosageForm)
                .ToList();
            foreach (var medication in MyList)
            {
                // Retrieve the most recent order for the current medication
                var latestOrder = dbContext.MedicationOrders
                    .Where(o => o.medicineid == medication.MedicationId)
                    .OrderByDescending(o => o.OrderDate)
                    .FirstOrDefault();
                var receivedStock = new ReceivedStock();
                if (latestOrder != null && latestOrder.OrderStatus == "sent")
                {
                    receivedStock.ReceivedQuantity = latestOrder.OrderQuantity;
                }
                else if (latestOrder != null && latestOrder.OrderStatus == "captured")
                {
                    receivedStock.ReceivedQuantity = 0;
                }
                else
                {
                    receivedStock.ReceivedQuantity = 0;
                }
                capturestockvm.Medication.quantitylist.Add(receivedStock);
            }
            capturestockvm.MedicationList = MyList;
            return View(capturestockvm);
        }
        [HttpPost]
        public async Task<IActionResult> CaptureStock(CaptureStockVM capturevm)
        {
            List<AddMedication> MyList = dbContext.AddMedication.ToList();

            // Stores received quantity
            List<int> medquantity = new List<int>();
            List<int> TrackQuantity = new List<int>();

            // Stores current quantity
            List<int> Currentquantity = new List<int>();
            DateTime HoldDate = capturevm.receivedStock.ReceivedDate;
            string success = "";

            for (int z = 0; z < MyList.Count(); z++)
            {
                Currentquantity.Add(MyList[z].Quantity);
                TrackQuantity.Add(capturevm.Medication.quantitylist[z].ReceivedQuantity);
            }

            List<MedicationOrder> Orders = dbContext.MedicationOrders.ToList();
            for (int z = 0; z < MyList.Count(); z++)
            {
                var medicationId = MyList[z].MedicationId;
                var medicationOrder = Orders
                    .Where(m => m.medicineid == medicationId)
                    .OrderByDescending(m => m.OrderDate) 
                    .FirstOrDefault(); 
                if (medicationOrder != null && capturevm.Medication.quantitylist[z].ReceivedQuantity != 0)
                {
                    medicationOrder.OrderQuantity = medicationOrder.OrderQuantity; 
                    medicationOrder.CurrentQuantity = medicationOrder.CurrentQuantity; 
                    medicationOrder.OrderStatus = "captured"; 
                    medicationOrder.OrderDate = DateTime.Now; 
                    medicationOrder.StockID = medicationOrder.StockID; 
                    dbContext.MedicationOrders.Update(medicationOrder);
                }
            }
            await dbContext.SaveChangesAsync(); // Save Medication Orders

            for (int z = 0; z < MyList.Count(); z++)
            {
                medquantity.Add(capturevm.Medication.quantitylist[z].ReceivedQuantity);
            }

            var existingMedications = dbContext.AddMedication
                .Where(m => MyList.Select(i => i.MedicationId)
                .Contains(m.MedicationId))
                .ToList();
            for (int z = 0; z < MyList.Count(); z++)
            {
                // Updating the quantity on the medication table
                var medication = existingMedications.FirstOrDefault(m => m.MedicationId == MyList[z].MedicationId);
                if (medication != null)
                {
                    medication.MedicationName = MyList[z].MedicationName;
                    medication.Quantity = MyList[z].Quantity + medquantity[z];
                    medication.ReOrder = MyList[z].ReOrder;
                    medication.dosageID = MyList[z].dosageID;
                    medication.scheduleID = MyList[z].scheduleID;
                    medication.image = MyList[z].image;
                    medication.ReOrderStatus = (medication.Quantity <= medication.ReOrder) ? 'y' : 'n';
                    dbContext.AddMedication.Update(medication);
                    if (medquantity[z] != 0)
                    {
                        success += $"{medication.MedicationName}\n";
                    }
                }
            }
            TempData["success1"] = success + "\nsuccessfully captured!";
            await dbContext.SaveChangesAsync();
            return RedirectToAction("ViewMed");
        }

        //Viewing Captured Stock
        public async Task<IActionResult> StockCaptured()
        {
            IEnumerable<ReceivedStock> stocks = dbContext.ReceivedStock
                .Include(x=>x.AddMedication)
                .ToList();
            return View(stocks);
        }
        public IActionResult Orders(DateTime? startDate, DateTime? endDate)
        {
            var viewModel = new OrdersVM
            {
                Prescriptions = dbContext.Prescriptions.Include(p => p.Patient)
                                                       .Include(p => p.RegisteredStaff)
                                                       .Where(x => x.TrackStatus != "active")
                                                       .OrderByDescending(x => x.DateIssued) 
                                                       .ThenBy(x => x.Patient.Surname) 
                                                       .ToList(),
                PrescriptionsItems = dbContext.PrescriptionItem
                .Include(pi => pi.medication)
                .OrderBy(pi => pi.medication.MedicationName).ToList(),
                StartDate = startDate,
                EndDate = endDate,
                TotalRejected = 0,
                TotalDispersed = 0,
                TotalMedcineDispersed = 0
            };

            if (startDate.HasValue && endDate.HasValue)
            {
                if (endDate < startDate) 
                {
                    TempData["error1"]="End date must be greater than or equal to start date.";
                }
                else
                {
                    var endDateOnly = endDate.Value.Date; 
                    viewModel.Prescriptions = viewModel.Prescriptions
                        .Where(p => p.DateIssued.Date >= startDate.Value.Date && p.DateIssued.Date <= endDateOnly)
                        .ToList();
                }

            }
            if (!viewModel.Prescriptions.Any())
            {
                TempData["error1"] = "No prescriptions found for the selected date range.";
            }
            else
            {
                // Create a dictionary to store total quantities per medication
                var medicationTotals = new Dictionary<string, int>();
                foreach (var prescription in viewModel.Prescriptions)
                {
                    var relatedItems = viewModel.PrescriptionsItems.Where(pi => pi.PresID == prescription.PrescriptionId);

                    foreach (var item in relatedItems)
                    {
                        if (medicationTotals.ContainsKey(item.medication.MedicationName))
                        {
                            medicationTotals[item.medication.MedicationName] += item.Quantity;
                        }
                        else
                        {
                            medicationTotals[item.medication.MedicationName] = item.Quantity;
                        }
                    }
                }
                viewModel.MedicationTotals = medicationTotals;
                viewModel.TotalMedcineDispersed = medicationTotals.Values.Sum();
            }

            return View(viewModel);
        }
        
        public async Task<IActionResult> StockCount(DateTime? startDate, DateTime? endDate)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var viewModel = new OrdersVM
            {
                Prescriptions = dbContext.Prescriptions.Include(p => p.Patient)
                                                       .Include(p => p.RegisteredStaff)
                                                       .Where(x => x.TrackStatus != "active")
                                                       .ToList(),
                PrescriptionsItems = dbContext.PrescriptionItem.Include(pi => pi.medication).ToList(),
                TotalDispersed = 0,
                TotalRejected=0,
                TotalMedcineDispersed=0
            };

            if (startDate.HasValue && endDate.HasValue)
            {
                var endDateOnly = endDate.Value.Date; // Get the date without time
                viewModel.Prescriptions = viewModel.Prescriptions
                    .Where(p => p.DateIssued.Date >= startDate.Value.Date && p.DateIssued.Date <= endDateOnly)
                    .ToList();
            }

            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                document.Add(new Paragraph("Bay Breeze Day Hospital")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(16)
                    .SetBold());

                document.Add(new Paragraph("Eastern Cape\r\nGqeberha\r\nSummerstrand (6001)\r\n1 8th Avenue \r\n")
                    .SetTextAlignment(TextAlignment.RIGHT));

                document.Add(new Paragraph("baybreeze@gmail.com")
                    .SetTextAlignment(TextAlignment.RIGHT));

                document.Add(new Paragraph("\n")); 

                document.Add(new Paragraph("DISPENSARY REPORT\n")
                    .SetFontSize(15)
                    .SetBold()
                    .SetTextAlignment(TextAlignment.LEFT));

                document.Add(new Paragraph(user.FullName)
                    .SetBold()
                    .SetTextAlignment(TextAlignment.LEFT));
              
                string currentDate = "Report Generated:\t" + DateTime.Now.ToString("dd MMMM yyyy");
                if (startDate.HasValue && endDate.HasValue)
                {
                    document.Add(new Paragraph("Date Range: " + startDate.Value.Date + " - " + endDate.Value.Date)
                    .SetBold()
                    .SetTextAlignment(TextAlignment.LEFT));
                }
                else
                {
                    document.Add(new Paragraph("Record Not Filtered By Date")
                   .SetBold()
                   .SetTextAlignment(TextAlignment.LEFT));
                }

                document.Add(new Paragraph(currentDate)
                    .SetFontSize(10)
                    .SetBold()
                    .SetTextAlignment(TextAlignment.RIGHT));
                var table = new Table(6);

                table.AddHeaderCell("Patient Name").SetBold();
                table.AddHeaderCell("Sent/Rejected Date").SetBold();
                table.AddHeaderCell("Script By").SetBold();
                table.AddHeaderCell("Prescribed Medication").SetBold();
                table.AddHeaderCell("Priority").SetBold();
                table.AddHeaderCell("Status").SetBold();

                foreach (var prescription in viewModel.Prescriptions)
                {
                    table.AddCell($"{prescription.Patient.Name} {prescription.Patient.Surname}");
                    table.AddCell(prescription.DateIssued.ToString("dd MMMM yyyy"));
                    table.AddCell($"{prescription.RegisteredStaff.Name} {prescription.RegisteredStaff.Surname}");

                    var medications = string.Join(", ", viewModel.PrescriptionsItems
                        .Where(i => i.PresID == prescription.PrescriptionId)
                        .Select(i => $"{i.medication.MedicationName} ({i.Quantity})"));

                    table.AddCell(medications);
                    if (prescription.UrgentStatus == 'N' || prescription.UrgentStatus == 'n')
                    {
                        table.AddCell("Not Urgent");
                    }
                    else
                    {
                        table.AddCell("Urgent");
                    }
                    table.AddCell(prescription.TrackStatus);
                    if (prescription.TrackStatus == "Dispersed")
                    {
                        viewModel.TotalDispersed = viewModel.TotalDispersed + 1;
                    }
                    else
                    {
                        viewModel.TotalRejected = viewModel.TotalRejected + 1;
                    }
                }
                document.Add(table);
                document.Add(new Paragraph("TOTAL SCRIPTS DISPENSED:\t" + viewModel.TotalDispersed)
                   .SetTextAlignment(TextAlignment.LEFT));

                document.Add(new Paragraph("TOTAL SCRIPTS REJECTED:\t" + viewModel.TotalRejected)
                  .SetTextAlignment(TextAlignment.LEFT));
                
                document.Add(new Paragraph("SUMMARY PER MEDICINE:\r\n\n")
                    .SetFontSize(15)
                    .SetBold()
                    .SetTextAlignment(TextAlignment.LEFT));

                var medicationQuantities = new Dictionary<string, int>();
                foreach (var prescription in viewModel.Prescriptions)
                {
                    if (prescription.TrackStatus == "Dispersed")
                    {
                        var relatedItems = viewModel.PrescriptionsItems
                            .Where(i => i.PresID == prescription.PrescriptionId);
                        foreach (var item in relatedItems)
                        {
                            if (medicationQuantities.ContainsKey(item.medication.MedicationName))
                            {
                                medicationQuantities[item.medication.MedicationName] += item.Quantity;
                            }
                            else
                            {
                                medicationQuantities[item.medication.MedicationName] = item.Quantity;
                            }
                        }
                    }
                }

                var medicationSummaryTable = new Table(2);
                medicationSummaryTable.AddHeaderCell("Medication Name").SetBold();
                medicationSummaryTable.AddHeaderCell("Quantity Dispersed").SetBold();
                foreach (var kvp in medicationQuantities)
                {
                    medicationSummaryTable.AddCell(kvp.Key);  // Medication Name
                    medicationSummaryTable.AddCell(kvp.Value.ToString());  // Quantity Dispersed
                }

                document.Add(medicationSummaryTable);
                document.Close();

                using (var outputStream = new MemoryStream())
                {
                    using (var pageNumberWriter = new PdfWriter(outputStream)) 
                    using (var reader = new PdfReader(new MemoryStream(stream.ToArray())))
                    {
                        using (var pdfWithPageNumber = new PdfDocument(reader, pageNumberWriter))
                        {
                            for (int i = 1; i <= pdfWithPageNumber.GetNumberOfPages(); i++)
                            {
                                PdfPage page = pdfWithPageNumber.GetPage(i);
                                PdfCanvas canvas = new PdfCanvas(page);
                                canvas.BeginText();
                                canvas.SetFontAndSize(PdfFontFactory.CreateFont(StandardFonts.HELVETICA), 10);
                                canvas.MoveText(page.GetPageSize().GetWidth() / 2 - 15, 30);
                                canvas.ShowText("Page " + i);
                                canvas.EndText();
                            }
                        }
                    }
                    byte[] finalOutput = outputStream.ToArray();
                    return File(finalOutput, "application/pdf", "prescriptions_report(Group14-Pharmacist).pdf");
                }
            }
        }
        public IActionResult Visual(DateTime? startDate, DateTime? endDate)
        {
            var viewModel = new OrdersVM
            {
                StartDate = startDate,
                EndDate = endDate,
                MedicationTotals = new Dictionary<string, int>()
            };

            var prescriptions = dbContext.Prescriptions.Include(p => p.Patient)
                                                         .Include(p => p.RegisteredStaff)
                                                         .Where(x => x.TrackStatus != "active") // Exclude active prescriptions
                                                         .ToList();
            if (startDate.HasValue && endDate.HasValue)
            {
                if (endDate < startDate)
                {
                    TempData["error1"] = "End date must be greater than or equal to start date.";
                    return View(viewModel);
                }
                else
                {
                    prescriptions = prescriptions.Where(p => p.DateIssued.Date >= startDate.Value.Date && p.DateIssued.Date <= endDate.Value.Date).ToList();
                }
            }

            if (!prescriptions.Any())
            {
                TempData["error1"] = "No prescriptions found for the selected date range.";
            }
            else
            {
                var medicationItems = dbContext.PrescriptionItem
                    .Include(pi => pi.medication)
                    .ToList();
                var medicationTotals = new Dictionary<string, int>();

                foreach (var prescription in prescriptions)
                {
                    if (prescription.TrackStatus == "Dispersed")
                    {
                        var relatedItems = medicationItems.Where(pi => pi.PresID == prescription.PrescriptionId);

                        foreach (var item in relatedItems)
                        {
                            if (medicationTotals.ContainsKey(item.medication.MedicationName))
                            {
                                medicationTotals[item.medication.MedicationName] += item.Quantity;
                            }
                            else
                            {
                                medicationTotals[item.medication.MedicationName] = item.Quantity;
                            }
                        }
                    }
                }
                viewModel.MedicationTotals = medicationTotals;
            }
            return View(viewModel);
        }
        private string GetUploadedFileName(AddMedication addMedication)
        {
            string uniqueFileName = null;
            if (addMedication.ProfilePhoto != null )
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + addMedication.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    addMedication.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
