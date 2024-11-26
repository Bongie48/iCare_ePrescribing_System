using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iCare_ePrescribing_System.Models;
using iCare_ePrescribing_System.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using iCare_ePrescribing_System.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
//using iCare_ePrescribing_System.ViewModels.BookSurgery;

public class SurgeonController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<StaffMembers> _userManager;
    public SurgeonController(ApplicationDbContext context, UserManager<StaffMembers> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Surgeon/SearchPatient
    public async Task<IActionResult> SearchPatient(string patientIdNo)
    {
        if (string.IsNullOrEmpty(patientIdNo))
        {
            return View("SearchPatient"); //This shows the booking view if Id no is provided
        }
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.IDNo == patientIdNo);
        if (patient == null)
        {
            ModelState.AddModelError("", "Patient not found");
            return View("SearchPatient");
        }

        return View("SearchPatient", patient);// This passes the found patient to the booking view

        //ViewBag.Surgeons = await _context.Surgeon.ToListAsync();
        //ViewBag.Patients = await _context.Patients.ToListAsync();
        //ViewBag.Anaesthesiologists = await _context.Anaesthesiologists.ToListAsync();
        //ViewBag.Theatres = await _context.Theatres.ToListAsync();
        //ViewBag.TreatmentCodes = await _context.TreatmentCodes.ToListAsync();
        //return View();
    }
    //GET:Surgeon/BookSurgery
    public async Task<IActionResult> BookSurgery()
    {
        var model = new BookSurgeryVM();

        var patients = await _context.Patients.Select(p => new SelectListItem
        {
            Value = p.PatientId.ToString(),
            Text = p.Name + " " + p.Surname
        }).ToListAsync();

        var theatres = await _context.Theatres.Select(t => new SelectListItem
        {
            Value = t.TheatreId.ToString(),
            Text = t.TheatreName
        }).ToListAsync();

        var treatmentCodes = await _context.TreatmentCodes.Select(tc => new SelectListItem
        {
            Value = tc.TreatmentCodeId.ToString(),
            Text = tc.Code + " - " + tc.Description
        }).ToListAsync();

        ViewBag.Patients = patients;
        ViewBag.Theatres = theatres;
        ViewBag.TreatmentCodes = treatmentCodes;

        ViewBag.SuccessMessage = TempData["SuccessMessage"];

        return View(model);

    }

    // POST: Surgeon/BookSurgery
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BookSurgery( BookSurgeryVM model)
    {

        if (ModelState.IsValid)
        {
            var booking = new BookSurgery
            {
                PatientId = model.NewPatient.PatientId,
                SurgeryDate = model.SurgeryDate,
                Session = model.Session,
                TheatreId = model.TheatreId,
                TreatmentCode = model.TreatmentCode,
                Status = "Confirmed"
            };

            await _context.Surgeries.AddAsync(booking); // Save to Surgeries (BookSurgery table)
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Surgery successfully booked!";
            return RedirectToAction(nameof(ViewBookedPatients));
        }
        return View(model);

        //var loggedInUser = await _userManager.GetUserAsync(User);
        //var SurgeoRegNumber = loggedInUser?.HealthCouncilRegistrationNumber;

        //if (SurgeoRegNumber == null)
        //{
        //    return NotFound();
        //}
        //var surgeon = await _context.Staff.FirstOrDefaultAsync(n => n.HealthCouncilRegistrationNumber == SurgeoRegNumber);

        //if (surgeon == null)
        //{
        //    return NotFound("Surgeon not found.");
        //}

        //var SurgeonTableSurgeon = new Surgeon
        //{
        //    Name = surgeon.Name,
        //    Surname = surgeon.Surname,
        //    ContactNumber = surgeon.Number.ToString(),
        //    Email = surgeon.Email,
        //    Specialization = "Surgeon",
        //    HealthCounsilRegistrationNumber = SurgeoRegNumber
        //};

        //if(SurgeonTableSurgeon == null)
        //{
        //    return NotFound();
        //}
        //await _context.Surgeon.AddAsync(SurgeonTableSurgeon);

        //var booking = new BookSurgery
        //{
        //    PatientId = model.NewPatient.PatientId,
        //    SurgeryDate = model.SurgeryDate,
        //    Session = model.Session,
        //    TheatreId = model.TheatreId,
        //    TreatmentCode = model.TreatmentCode,
        //    Status = "Confirmed",
        //    SurgeonID = surgeon.StaffId,
        //    Surgeon = SurgeonTableSurgeon
        //};

        //var result = await _context.Surgeries.AddAsync(booking); // Save to Surgeries (BookSurgery table)

        //if (result != null)
        //{
        //    await _context.SaveChangesAsync();
        //    TempData["SuccessMessage"] = "Surgery successfully booked!";
        //    return RedirectToAction(nameof(ViewBookedPatients));
        //}

        //return View(model);

    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> admitedPatientsList()
    {
        var admittedAdmissions = await _context.Admission
            .Where(a => a.AdmissionStatus == "admitted")
            .Include(a => a.Patient)
            .OrderByDescending(a => a.AdmissionDate)
            .ToListAsync();

        return View(admittedAdmissions);
    }

    // GET: Surgeon/LoadPrescription
    public async Task<IActionResult> LoadPrescription()
    {
        ViewBag.Surgeons = await _context.Surgeon.ToListAsync();
        ViewBag.Patients = await _context.Patients.ToListAsync(); //populate with existing patients
        ViewBag.Anaesthesiologists = await _context.Anaesthesiologists.ToListAsync();
        ViewBag.Theatres = await _context.Theatres.ToListAsync();
        ViewBag.TreatmentCodes = await _context.TreatmentCodes.ToListAsync();
        return View();
    }
    
    //GET: Surgeon/AddPatient
    public IActionResult AddPatient()
    {
        return View();
    }

    //POST:Surgeon/ AddPatient
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddPatient(Patient newPatient)
    {
        if (ModelState.IsValid)
        {

            _context.Patients.Add(newPatient);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Patient successfully added! You can now book surgery for them.";

            return RedirectToAction(nameof(BookSurgery)); // redirects to surgery booking
        }
        return View(newPatient);
    }



    // POST: Surgeon/LoadPrescription
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoadPrescription([Bind("SurgeonId,PatientId,DateIssued,MedicationName,DosageForm,Strength,Quantity,Instructions")] Prescription prescription)
    {
        if (ModelState.IsValid)
        {
            // Check for contra-indications, interactions, and allergies
            var contraindications = CheckForContraIndications(prescription);
            //if (contraindications.Any())
            //{
            //    ModelState.AddModelError("", "Contra-indications found: " + string.Join(", ", contraindications));
            //    return View(prescription);
            //}

            _context.Add(prescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(prescription);
    }
    //Add prescription
    public IActionResult AddPrescription() 
    { 
        PrescriptionVM prescriptionVM= new PrescriptionVM();
        prescriptionVM.prescription = new Prescription();
        
        //Select list for patients
        List<SelectListItem> patientlist = _context.Patients
                .OrderBy(n => n.Name)
                .Select(n => new SelectListItem { Value = n.PatientId.ToString(), Text = n.Name+" "+n.Surname })
                .ToList();
        prescriptionVM.Patients = patientlist;

        IEnumerable<RegisteredStaff> staff = _context.Staff;
        prescriptionVM.SurgeonID = staff;
        return View(prescriptionVM); 
    }
    [HttpPost]
    public IActionResult AddPrescription(PrescriptionVM prescriptionVM)
    {
        PrescriptionVM prescription = new PrescriptionVM();
        prescription.patientId = prescription.patientId;
        prescriptionVM.prescription.TrackStatus = "active";
        _context.Add(prescriptionVM.prescription);
        _context.SaveChanges();
        return RedirectToAction("PrescriptionMedication");
    }
    public IActionResult PrescriptionMedication()
    {
        PrescriptionVM prescriptionVM = new PrescriptionVM();
        prescriptionVM.prescriptionItem = new PrescriptionItem();
        prescriptionVM.Vitals = _context.PatientVitals.Where(x => x.PatientVitalsId == prescriptionVM.patientId);

        //Select list for medication
        List<SelectListItem> medicationlist = _context.AddMedication
            .Include(x=>x.DosageForm)
                .OrderBy(n => n.MedicationName)
                .Select(n => new SelectListItem { Value = n.MedicationId.ToString(), Text = n.MedicationName + " (" + n.DosageForm.DosageName + ")" })
                .ToList();
        prescriptionVM.Medication = medicationlist;

        //return all active prescription
        IEnumerable<Prescription> prescriptions=_context.Prescriptions
            .Where(x=>x.TrackStatus=="active")
            .Include(x=>x.Patient);
        prescriptionVM.prescriptionlist = prescriptions;

        prescriptionVM.prescriptionitems = _context.PrescriptionItem.Include(x=>x.medication).ToList();
        return View(prescriptionVM);
    }
    [HttpPost]
    public IActionResult PrescriptionMedication(PrescriptionVM prescriptionVM)
    {
        _context.Add(prescriptionVM.prescriptionItem);
        _context.SaveChanges();
        return RedirectToAction("PrescriptionMedication");
    }
    private async Task<IEnumerable<string>> CheckForContraIndications(Prescription prescription)
    {
        var contraindications = new List<string>();

        // Fetch the patient with related data
        var patient = await _context.Patients
            .Include(p => p.Allergies)
            .Include(p => p.CurrentMedications)
            .FirstOrDefaultAsync(p => p.PatientId == prescription.PatientId);

        if (patient == null)
        {
            contraindications.Add("Patient not found.");
            return contraindications;
        }

        // Check for allergies
        //if (patient.Allergies.Any(a => a.Name.ToLower() == prescription.MedicationName.ToLower()))
        //{
        //    contraindications.Add($"Patient is allergic to {prescription.MedicationName}.");
        //}

        // Check for interactions with current medications
        //foreach (var medication in patient.CurrentMedications)
        //{
        //    if (medication.MedicationName.ToLower() == prescription.MedicationName.ToLower())
        //    {
        //        contraindications.Add($"Interaction with current medication: {medication.MedicationName}.");
        //    }
        //}

        // Implement additional logic for contraindications based on patient diagnosis or other conditions
        // This could involve checking patient diagnosis codes, medical history, etc.

        return contraindications;





    }

    public async Task<IActionResult> ConfirmSurgery(int? SurgeryId)
    {
        var surgery = await _context.Surgeries
                                    .Include(s => s.Patient)
                                    //.Include(s => s.Surgeon)
                                    //.Include(s => s.Anaesthesiologist)
                                    .Include(s => s.Theatre)
                                    //.Include(s => s.TreatmentCode)
                                    .FirstOrDefaultAsync(m => m.SurgeryId == SurgeryId);

        if (surgery == null)
        {
            return NotFound();
        }

        return View(surgery);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ConfirmSurgery(int SurgeryId, bool confirmed)
    {
        var surgery = await _context.Surgeries.FindAsync(SurgeryId);

        if (surgery == null)
        {
            return NotFound();
        }

        if (confirmed)
        {
            surgery.Status = "Confirmed";
            await _context.SaveChangesAsync();
            // Logic to send an email to the patient or any other notification
        }

        return RedirectToAction(nameof(ViewBookedPatients));
    }

    public async Task<IActionResult> ViewBookedPatients()
    {
        var bookedPatients = await _context.Surgeries
                                            .Include(s => s.Patient)
                                            .Include(s => s.Theatre) // Include Theatre details
                                            //.Include(s => s.TreatmentCode)
                                            .Where(s => s.Status == "Confirmed")
                                            .ToListAsync();

        return View(bookedPatients);
    }

}
