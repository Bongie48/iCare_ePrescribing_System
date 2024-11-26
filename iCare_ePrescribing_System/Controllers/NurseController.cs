using iCare_ePrescribing_System.Data;
using iCare_ePrescribing_System.Models;
using iCare_ePrescribing_System.Repository;
using iCare_ePrescribing_System.ViewModels.Nurse;
using iText.Commons.Actions.Contexts;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using System;
using System.Reflection.Metadata;
using static iText.IO.Util.IntHashtable;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.IO.Font.Constants;

namespace iCare_ePrescribing_System.Controllers;

[Authorize]
public class NurseController : Controller
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IEmailSender _emailSender;
    private readonly IWebHostEnvironment _webHost;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<StaffMembers> _userManager;
    public NurseController(
        ApplicationDbContext dbContext,
        IEmailSender emailSender,
        IWebHostEnvironment webHost,
        IRoleRepository roleRepository,
        IUserRepository userRepository,
        UserManager<StaffMembers> userManager)
    {
        this._dbContext = dbContext;
        this._emailSender = emailSender;
        _webHost = webHost;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var dailyPatientsRecords = new PatientRecordsViewModel
        {
            SelectedDate = DateTime.Today.Date,
            patients = from p in _dbContext.Surgeries 
                       where p.SurgeryDate.Date == DateTime.Today.Date 
                       select p.Patient
        };

        return View(dailyPatientsRecords);
    }

    [HttpGet]
    public IActionResult filterPatients(DateTime? SelectedDate)
    {
        var dateToFilter = SelectedDate ?? DateTime.Today.Date;

        var filteredPatientsRecords = new PatientRecordsViewModel
        {
            SelectedDate = dateToFilter,
            patients = from p in _dbContext.Surgeries 
                       where p.SurgeryDate.Date == dateToFilter 
                       select p.Patient
        };

        return PartialView("_PatientRecords", filteredPatientsRecords);
    }

    [HttpGet]
    public async Task<IActionResult> admitPatient(int? id)
    {
        if (id == null)
        {
            return RedirectToAction(nameof(Index));
        }
            
        var Patient = await _dbContext.Patients.FindAsync(id);

        if (Patient == null)
        {
            return RedirectToAction(nameof(Index));
        }

        var admissionRecord = await getAddressAndWard(Patient);

        return View(admissionRecord);
    }

    private async Task<AdmissionViewModel> getAddressAndWard(Patient patient)
    {
        var wards = await _dbContext.Wards
            .Select(w => new SelectListItem { Value = w.WardID.ToString(), Text = w.Name })
            .ToListAsync();

        var beds = await _dbContext.Bed
            .Select(b => new SelectListItem { Value = b.BedID.ToString(), Text = b.BedNo.ToString() })
            .ToListAsync();

        var province = await _dbContext.Province
            .Select(b => new SelectListItem { Value = b.ProvinceId.ToString(), Text = b.Name.ToString() })
            .ToListAsync();

        var City = await _dbContext.City
            .Select(b => new SelectListItem { Value = b.CityId.ToString(), Text = b.Name.ToString() })
            .ToListAsync();

        var Suburb = await _dbContext.Suburb
            .Select(b => new SelectListItem { Value = b.SuburbId.ToString(), Text = b.Name.ToString() })
            .ToListAsync();

        return new AdmissionViewModel
        {
            Patient = patient,
            WardList = wards,
            BedList = beds,
            ProvinceList = province,
            CityList = City,
            SuburbList = Suburb,
            Admissions = new Admission()
        };
    }

    [HttpPost]
    public async Task<IActionResult> GetBedsByWard(int wardId)
    {
        var beds = await _dbContext.Bed.Where(b => b.WardID == wardId && b.Status == "Available")
                                 .Select(b => new { Value = b.BedID.ToString(), Text = b.BedNo.ToString() }).ToListAsync();

        return Json(beds);
    }

    [HttpPost]
    public async Task<IActionResult> GetCities(int provinceId)
    {
        var cities = await _dbContext.City
                            .Where(c => c.ProvinceId == provinceId)
                            .Select(c => new { value = c.CityId.ToString(), text = c.Name }).ToListAsync();

        return Json(cities);
    }
    [HttpPost]
    public async Task<IActionResult> GetSuburbs(int cityId)
    {
        var suburbs = await _dbContext.Suburb
                             .Where(s => s.CityId == cityId)
                             .Select(s => new { value = s.SuburbId.ToString(), text = s.Name }).ToListAsync();

        return Json(suburbs);
    }

    [HttpPost]
    public async Task<IActionResult> admitPatient(int id, AdmissionViewModel admission)
    {
        var patient = await updatePatientDetails(id, admission);

        var bed = await _dbContext.Bed.FindAsync(admission.Admissions.BedID);
        bed.Status = "Unavailable";

        var admitPatient = new Admission
        {
            AdmissionDate = admission.Admissions.AdmissionDate,
            AdmissionStatus = "Admitted",
            Height = admission.Admissions.Height,
            Weight = admission.Admissions.Weight,
            Bed = bed,
            Patient = patient,
        };

        _dbContext.Add(admitPatient);
        var result = await _dbContext.SaveChangesAsync();

        if (result > 0)
        {
            TempData["Message"] = "Patient admitted successfully.";
            TempData["MessageType"] = "success";

            return RedirectToAction("PatientProfile", new { id = patient.PatientId });
        }

        TempData["Message"] = "Failed to admit patient. Please try again.";
        TempData["MessageType"] = "error";

        return View(id);

    }

    public async Task<Patient> updatePatientDetails(int id, AdmissionViewModel admission)
    {
        var patient = await _dbContext.Patients.FindAsync(id);

        //patient.IDNo = admission.Patient.IDNo;
        //patient.Name = admission.Patient.Name;
        //patient.DateOfBirth = admission.Patient.DateOfBirth;
        //patient.Surname = admission.Patient.Surname;
        //patient.Gender = "Female";
        patient.Email = admission.Patient.Email;
        patient.ContactNumber = admission.Patient.ContactNumber;
        patient.HomeAddress = admission.Patient.HomeAddress;
        patient.SuburbId = admission.Patient.SuburbId;

        _dbContext.Update(patient);
        _dbContext.SaveChanges();

        return patient;
    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> admitedPatientsList()
    {
        var admittedAdmissions = await _dbContext.Admission
        .Where(a => a.AdmissionStatus == "admitted")
        .Include(a => a.Patient)        // Include the related Patient entity
        .Include(a => a.Bed)            // Include the related Bed entity
            .ThenInclude(b => b.Ward)   // Include the Ward entity related to the Bed
        .OrderByDescending(a => a.AdmissionDate)
        .ToListAsync();

        return View(admittedAdmissions);
    }

    [HttpPost]
    public async Task<IActionResult> discharge(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // Find the admission record with the patient and bed information
        var admission = await _dbContext.Admission
            .Include(a => a.Patient)
            .Include(a => a.Bed) // Include the related Bed entity
            .FirstOrDefaultAsync(a => a.AdmisionID == id);

        if (admission == null)
        {
            TempData["Message"] = "Admission not found.";
            TempData["MessageType"] = "error";
            return View();
        }

        // Ensure the ModelState is valid
        if (ModelState.IsValid)
        {
            // Update the admission status to "Discharged"
            admission.AdmissionStatus = "Discharged";
            admission.DischargeTime = DateTime.UtcNow;

            // Update the bed status to "Available"
            if (admission.Bed != null)
            {
                admission.Bed.Status = "Available";
            }

            try
            {
                // Update the admission and the bed status
                _dbContext.Update(admission);
                var result = await _dbContext.SaveChangesAsync();

                if (result > 0)
                {
                    TempData["Message"] = "Patient discharged successfully.";
                    TempData["MessageType"] = "success";

                    return RedirectToAction("PatientProfile", new { id = admission.Patient.PatientId });
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle the error as necessary
                TempData["Message"] = "An error occurred while discharging the patient.";
                TempData["MessageType"] = "error";
            }
        }

        return View();
    }

    [HttpGet]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> PatientProfile(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var patient = await _dbContext.Patients
            .Include(p => p.Admission)
            .Include(p => p.Allergies)
            .Include(p => p.PatientConditions)
                .ThenInclude(pc => pc.Condition)
            .Include(p => p.PatientMedication)
                .ThenInclude(c => c.Medication)
            .FirstOrDefaultAsync(p => p.PatientId == id);

        if (patient == null)
        {
            return NotFound();
        }

        // Retrieve the relevant admission (e.g., the most recent or admitted one)
        var currentAdmission = patient.Admission
            .FirstOrDefault(a => a.AdmissionStatus == "Admitted");

        if (currentAdmission == null)
        {
            return NotFound("No active admission found for the patient.");
        }

        // Use the dynamically retrieved AdmissionID to query PatientVitals
        var patientVital = await _dbContext.PatientVitals
            .Include(pv => pv.Vitals)
            .Where(pv => pv.AdmisionID == currentAdmission.AdmisionID)
            .ToListAsync();

        // Fetch dispensed medications for the patient from the pharmacist
        var dispensedMedications = _dbContext.PrescriptionItem
            .Include(pi => pi.Prescription)
            .Include(pi => pi.medication)
            .Where(pi => pi.Prescription.PatientId == patient.PatientId
                         && pi.Prescription.TrackStatus == "active")
            .ToList();

        // Fetch latest PrescribedMedication records for each PrescriptionItem and calculate the sum of QuantityAdministered
        var latestPrescribedMedications = await _dbContext.PrescribedMedication
            .Where(pm => dispensedMedications.Select(di => di.itemId).Contains(pm.PrescriptionItem.itemId))
            .GroupBy(pm => pm.PrescriptionItem.itemId)
            .Select(g => new
            {
                LatestPrescribedMedication = g.OrderByDescending(pm => pm.TimeAdministered).FirstOrDefault(), // Latest record
                TotalAdministeredQuantity = g.Sum(pm => pm.QuantityAdministered) // Sum of all administered quantities
            })
            .ToListAsync();

        var patientProfile = new ProfileViewModel
        {
            Patient = patient,
            Admission = currentAdmission,
            Allergies = patient.Allergies,
            PatientConditions = patient.PatientConditions,
            PatientMedication = patient.PatientMedication,
            PatientVitals = patientVital,

            PrescriptionItem = dispensedMedications,
            PrescribedMedication = latestPrescribedMedications
                .Select(lpm => lpm.LatestPrescribedMedication).ToList()
        };

        // You can pass the summed quantities to the View via ViewData or directly modify the ViewModel to include this information
        ViewData["AdministeredQuantities"] = latestPrescribedMedications
            .ToDictionary(lpm => lpm.LatestPrescribedMedication.PrescriptionItem.itemId, lpm => lpm.TotalAdministeredQuantity);

        return View(patientProfile);
    }

    [HttpGet]
    public async Task<IActionResult> AddCondition(int Id)
    {
        var patient = _dbContext.Patients
                          .Include(p => p.PatientConditions)
                          .FirstOrDefault(p => p.PatientId == Id);

        if (patient == null) { return NotFound(); }

        var conditions = await _dbContext.Condition.ToListAsync();

        var viewModel = new ConditionViewModel
        {
            Patient = patient,
            Conditions = conditions,  
            PatientConditions = new List<PatientConditions>()
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> AddCondition(ConditionViewModel conditionVM)
    {
        if (conditionVM == null)
        {
            return NotFound();
        }

        foreach (var patientCondition in conditionVM.PatientConditions)
        {
            if (patientCondition.ConditionID != 0)
            {
                patientCondition.PatientId = conditionVM.Patient.PatientId;
                patientCondition.Date = DateTime.UtcNow;
                var result = await _dbContext.PatientConditions.AddAsync(patientCondition);
            }
        }
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("PatientProfile", new { Id = conditionVM.Patient.PatientId });

        // Unhandle case scenario:
        // - Reload conditions if the model state is invalid
        // - When no condition selected

    }

    [HttpGet]
    public async Task<IActionResult> AddMedication(int Id)
    {
        var patient = await _dbContext.Patients.FindAsync(Id);

        if (patient == null)
        {
            return NotFound();
        }

        var viewModel = new MedicationViewModel
        {
            Patient = patient,
            medications = await _dbContext.Medications.ToListAsync(), // List of all medications
            PatientMedicationList = new List<PatientMedication>()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddMedication(MedicationViewModel medicationViewModel)
    {
        if (medicationViewModel == null)
        {
            return NotFound();
        }

        foreach (var patientMedication in medicationViewModel.PatientMedicationList)
        {
            if (patientMedication.MedicationId != 0 && patientMedication.Quantity > 0)
            {
                patientMedication.PatientId = medicationViewModel.Patient.PatientId;
                await _dbContext.PatientMedication.AddAsync(patientMedication);
            }
        }
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("PatientProfile", new { id = medicationViewModel.Patient.PatientId });

        // Unhandled case scenario:
        // - Reload medication if the model state is invalid
        // - Saving when no medication selected
    }
    [HttpGet]
    public async Task<IActionResult> AddVitals(int id)
    {
        var admission = await _dbContext.Admission.FirstOrDefaultAsync(a => a.AdmisionID == id);

        if (admission == null)
        {
            return NotFound();
        }
        var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.PatientId == admission.PatientID);

        if (patient == null || admission == null)
        {
            return NotFound();
        }

        var viewModel = new VitalsViewModel
        {
            Patient = patient,
            Admission = admission,
            Vitals = await _dbContext.Vitals.ToListAsync(), // Load the list of all available vitals
            PatientVitals = new List<PatientVitals>() // Initial empty list
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Retake(int id)
    {
        // Get the specific patient vital record where retake is needed
        var patientVital = await _dbContext.PatientVitals.FirstOrDefaultAsync(pv => pv.PatientVitalsId == id);

        if (patientVital == null || patientVital.Status != "Retake")
        {
            return NotFound();
        }

        // Get the associated admission and patient details
        var admission = await _dbContext.Admission.FirstOrDefaultAsync(a => a.AdmisionID == patientVital.AdmisionID);
        var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.PatientId == admission.PatientID);
        var vital = await _dbContext.Vitals.FirstOrDefaultAsync(v => v.VitalId == patientVital.VitalId);

        // Prepare the view model with only the selected vital
        var retakenVitals = new VitalsViewModel
        {
            Patient = patient,
            Admission = admission,
            Vitals = new List<Vitals> { vital }, // Only load the vital that needs retaking
            PatientVitals = new List<PatientVitals> { patientVital }
        };

        return View(retakenVitals);
    }

    [HttpPost]
    public async Task<IActionResult> AddVitals(VitalsViewModel viewModel)
    {
        var count = 0;
        var admission = await _dbContext.Admission.FirstOrDefaultAsync(a => a.AdmisionID == viewModel.Admission.AdmisionID);

        if (admission == null)
        {
            return NotFound();
        }

        var patientVitalRetake = viewModel.PatientVitals.FirstOrDefault();

        if(patientVitalRetake == null) 
        { 
            return NotFound(); 
        }

        if (patientVitalRetake.PatientVitalsId != 0) // Retake scenario
        {
            var existingVital = await _dbContext.PatientVitals.FirstOrDefaultAsync(pv => pv.PatientVitalsId == patientVitalRetake.PatientVitalsId);

            if (existingVital == null || existingVital.Status != "Retake")
            {
                return NotFound();
            }

            var vital = await _dbContext.Vitals.FirstOrDefaultAsync(v => v.VitalId == existingVital.VitalId);

            if (vital == null)
            {
                return NotFound();
            }

            // Update existing vital with new readings and status
            var newReading = double.Parse(patientVitalRetake.Readings);

            if (newReading < vital.Lowlimit || newReading > vital.Highlimit)
            {
                existingVital.Status = "Retaken-History";
                patientVitalRetake.Status = "Retake";
                _dbContext.PatientVitals.Update(existingVital);
            }
            else
            {
                patientVitalRetake.Status = "Retaken";
                existingVital.Status = "Retaken-History";
                _dbContext.PatientVitals.Update(existingVital);
                //await _dbContext.SaveChangesAsync(); // Save to database
            }
            var newVitalReading = new PatientVitals
            {
                CaptureTime = patientVitalRetake.CaptureTime,
                Readings = patientVitalRetake.Readings,
                Status = patientVitalRetake.Status,
                Vitals = vital,
                Admission = admission,
                AdmisionID = admission.AdmisionID,
                VitalId = vital.VitalId            
            };

            if(newVitalReading == null)
            {
                return NotFound();
            }

            //existingVital.Readings = patientVitalRetake.Readings;
            //existingVital.CaptureTime = patientVitalRetake.CaptureTime;

            _dbContext.PatientVitals.Add(newVitalReading);
        }
        else
        {
            foreach (var patientVital in viewModel.PatientVitals)
            {
                var vital = await _dbContext.Vitals.FirstOrDefaultAsync(a => a.VitalId == patientVital.VitalId);

                if (vital == null)
                {
                    return NotFound();
                }

                if (patientVital.VitalId != 0 && !string.IsNullOrWhiteSpace(patientVital.Readings) && patientVital.CaptureTime != default)
                {
                    if (double.Parse(patientVital.Readings) < vital.Lowlimit
                        || double.Parse(patientVital.Readings) > vital.Highlimit)
                    {
                        patientVital.Status = "Retake";
                    }
                    else
                    {
                        patientVital.Status = "Normal";
                    }

                    patientVital.AdmisionID = viewModel.Admission.AdmisionID; // Link the admission
                    patientVital.Admission = admission;
                    patientVital.Vitals = vital;


                    _dbContext.PatientVitals.Add(patientVital);
                    count++;
                }
            }
        }

        await _dbContext.SaveChangesAsync(); // Save to database
        return RedirectToAction("PatientProfile", new { id = viewModel.Patient.PatientId });


        // Reload the vitals in case of validation failure
        //viewModel.Vitals = _dbContext.Vitals.ToList();
        //return View(viewModel);
    }
    [HttpGet]
    public async Task<IActionResult> PrescribedMedication(int id)
    {
        var prescriptionItem = await _dbContext.PrescriptionItem
            .Include(pi => pi.medication)
            .Include(pi => pi.Prescription)
            .ThenInclude(p => p.Patient)
            .FirstOrDefaultAsync(pi => pi.itemId == id);

        if (prescriptionItem == null)
        {
            return NotFound("Prescription item not found.");
        }

        var loggedInUser = await _userManager.GetUserAsync(User);
        var nurseRegNumber = loggedInUser?.HealthCouncilRegistrationNumber;

        if (nurseRegNumber == null)
        {
            return NotFound();
        }
        var nurse = await _dbContext.Staff.FirstOrDefaultAsync(n => n.HealthCouncilRegistrationNumber == nurseRegNumber);

        if (nurse == null)
        {
            return NotFound("Nurse not found.");
        }

        var model = new PrescribedMedicationViewModel
        {
            PrescriptionItem = prescriptionItem,
            PrescribedMedication = new PrescribedMedication
            {
                PatientId = prescriptionItem.Prescription.PatientId,
                Patient = prescriptionItem.Prescription.Patient,
                PrescriptionItem = prescriptionItem,
                Nurse = nurse 
            }
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PrescribedMedication(PrescribedMedicationViewModel model)
    {
        var prescriptionItem = await _dbContext.PrescriptionItem
            .FirstOrDefaultAsync(pi => pi.itemId == model.PrescriptionItem.itemId);

        if (prescriptionItem == null)
        {
            return NotFound("Prescription item not found.");
        }

        if (model.PrescribedMedication.QuantityAdministered > prescriptionItem.Quantity)
        {
            ModelState.AddModelError("", "Administered quantity cannot exceed prescribed quantity.");
            return View(model);
        }

        if (prescriptionItem.RemaininingQuantity.HasValue)
        {
            prescriptionItem.RemaininingQuantity -= model.PrescribedMedication.QuantityAdministered;
        }
        else
        {
            prescriptionItem.RemaininingQuantity = prescriptionItem.Quantity - model.PrescribedMedication.QuantityAdministered;
        }

        _dbContext.Update(prescriptionItem);

        var loggedInUser = await _userManager.GetUserAsync(User);
        var nurseRegNumber = loggedInUser?.HealthCouncilRegistrationNumber;

        if (nurseRegNumber == null)
        {
            return NotFound();
        }
        var nurse = await _dbContext.Staff.FirstOrDefaultAsync(n => n.HealthCouncilRegistrationNumber == nurseRegNumber);

        if (nurse == null)
        {
            return NotFound("Nurse not found.");
        }

        var prescribedMedication = new PrescribedMedication
        {
            PatientId = model.PrescribedMedication.PatientId,
            PrescriptionItem = prescriptionItem,
            TimeAdministered = model.PrescribedMedication.TimeAdministered,
            QuantityAdministered = model.PrescribedMedication.QuantityAdministered,
            Notes = model.PrescribedMedication.Notes,
            Nurse = nurse
        };

        await _dbContext.PrescribedMedication.AddAsync(prescribedMedication);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("PatientProfile", new { id = model.PrescribedMedication.PatientId });
    }


    [HttpGet]
    public async Task<IActionResult> Report()
    {
        return View(new ReportsViewModel());
    }


    public async Task<IActionResult> Report(DateTime StartDate, DateTime EndDate)
    {
        // Adjust EndDate to include the entire day by adding 1 day and subtracting 1 tick(smallest unit of time)
        var adjustedEndDate = EndDate.AddDays(1).AddTicks(-1);

        // Fetch Prescribed Medication records within the selected date range
        var prescribedMedications = await _dbContext.PrescribedMedication
            .Include(pm => pm.Patient)
            .Include(pm => pm.PrescriptionItem)
                .ThenInclude(pi => pi.medication)
            .Where(pm => pm.TimeAdministered >= StartDate && pm.TimeAdministered <= adjustedEndDate)
            .OrderBy(pm => pm.Patient.Name + pm.Patient.Surname)  // Sort by patient name
            .ToListAsync();

        // Total number of distinct patients
        var totalPatients = prescribedMedications.Select(pm => pm.PatientId).Distinct().Count();

        // Summary of medication quantity administered
        var medicationSummary = prescribedMedications
            .GroupBy(pm => pm.PrescriptionItem.medication.MedicationName)
            .Select(group => new MedicationSummaryViewModel
            {
                MedicationName = group.Key,
                QuantityAdministered = group.Sum(pm => pm.QuantityAdministered)
            })
            .ToList();

        // Get the current nurse details (assuming user is logged in and represents the nurse)
        //var nurseName = User.Identity.Name; // Use this to get the logged-in user's name
        var loggedInUser = await _userManager.GetUserAsync(User);
        var nurseRegNumber = loggedInUser?.HealthCouncilRegistrationNumber;

        if (nurseRegNumber == null)
        {
            return NotFound();
        }
        var nurse = await _dbContext.Staff.FirstOrDefaultAsync(n => n.HealthCouncilRegistrationNumber == nurseRegNumber);

        if (nurse == null)
        {
            return NotFound("Nurse not found.");
        }

        // Prepare the ViewModel
        var reportViewModel = new ReportsViewModel
        {
            StartDate = StartDate,
            EndDate = EndDate,
            PrescribedMedications = prescribedMedications,
            TotalPatients = totalPatients,
            MedicationSummary = medicationSummary,
            ReportGenerated = true,
            NurseName = nurse.Name + " " + nurse.Surname // Pass the nurse's name to the ViewModel
        };

        return View(reportViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> GenerateMedicationReport(DateTime? startDate, DateTime? endDate)
    {
        // Fetch the user generating the report (nurse/authorized staff)
        var user = await _userManager.FindByNameAsync(User.Identity.Name);

        // Adjust EndDate to include the full day if provided
        var adjustedEndDate = endDate?.AddDays(1).AddTicks(-1);

        // Fetch prescribed medications within the selected date range
        var prescribedMedications = await _dbContext.PrescribedMedication
            .Include(pm => pm.Patient)
            .Include(pm => pm.PrescriptionItem)
                .ThenInclude(pi => pi.medication)
            .Where(pm => pm.TimeAdministered >= startDate && pm.TimeAdministered <= adjustedEndDate)
            .OrderBy(pm => pm.Patient.Name + " " + pm.Patient.Surname)
            .ToListAsync();

        // Create a MemoryStream to hold the generated PDF
        using (var stream = new MemoryStream())
        {
            var writer = new PdfWriter(stream);
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf);

            // Add Header: Hospital Information
            document.Add(new Paragraph("Bay Breeze Day Hospital")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16)
                .SetBold());

            document.Add(new Paragraph("Eastern Cape\r\nGqeberha\r\nSummerstrand (6001)\r\n1 8th Avenue \r\n")
                .SetTextAlignment(TextAlignment.RIGHT));

            document.Add(new Paragraph("baybreeze@gmail.com")
                .SetTextAlignment(TextAlignment.RIGHT));

            document.Add(new Paragraph("\n")); // Empty line for spacing

            // Title of the Report
            document.Add(new Paragraph("MEDICATION REPORT\n")
                .SetFontSize(15)
                .SetBold()
                .SetTextAlignment(TextAlignment.LEFT));

            // User (Nurse or Pharmacist generating the report)
            document.Add(new Paragraph("Report Generated By: " + user.FullName)
                .SetBold()
                .SetTextAlignment(TextAlignment.LEFT));

            // Display Date Range or indicate that no filtering by date
            if (startDate.HasValue && endDate.HasValue)
            {
                document.Add(new Paragraph($"Date Range: {startDate.Value:dd MMM yyyy} - {endDate.Value:dd MMM yyyy}")
                    .SetBold()
                    .SetTextAlignment(TextAlignment.LEFT));
            }
            else
            {
                document.Add(new Paragraph("Record Not Filtered By Date")
                    .SetBold()
                    .SetTextAlignment(TextAlignment.LEFT));
            }

            // Current Date when the report is generated
            document.Add(new Paragraph("Report Generated on: " + DateTime.Now.ToString("dd MMMM yyyy"))
                .SetTextAlignment(TextAlignment.RIGHT));

            // Add a Table for detailed prescription information
            var table = new Table(5);
            table.AddHeaderCell("Patient Name").SetBold();
            table.AddHeaderCell("Medication").SetBold();
            table.AddHeaderCell("Quantity").SetBold();
            table.AddHeaderCell("Date Administered").SetBold();
            table.AddHeaderCell("Administered By").SetBold();


            // Populate table with data from prescribed medications
            foreach (var medication in prescribedMedications)
            {
                table.AddCell($"{medication.Patient.Name + " " + medication.Patient.Surname}");
                table.AddCell(medication.PrescriptionItem.medication.MedicationName);
                table.AddCell(medication.QuantityAdministered.ToString());
                table.AddCell(medication.TimeAdministered.ToString("dd MMM yyyy HH:mm"));
                table.AddCell(user.FullName);  // Assuming the user generating the report is the same as the one administering
            }

            document.Add(table);  // Add table to the document

            // Summary of total number of medications administered
            var medicationSummary = prescribedMedications
                .GroupBy(pm => pm.PrescriptionItem.medication.MedicationName)
                .Select(group => new
                {
                    MedicationName = group.Key,
                    TotalQuantity = group.Sum(pm => pm.QuantityAdministered)
                })
                .ToList();

            // Add a new section for summary per medication
            document.Add(new Paragraph("\nSUMMARY PER MEDICATION:")
                .SetFontSize(15)
                .SetBold());

            // Add a new table for summary
            var summaryTable = new Table(2);
            summaryTable.AddHeaderCell("Medication Name").SetBold();
            summaryTable.AddHeaderCell("Total Quantity Administered").SetBold();

            // Populate the summary table
            foreach (var summary in medicationSummary)
            {
                summaryTable.AddCell(summary.MedicationName);
                summaryTable.AddCell(summary.TotalQuantity.ToString());
            }

            document.Add(summaryTable);

            // Close the document
            document.Close();

            // Generate the PDF and return as a file response
            byte[] pdfOutput = stream.ToArray();
            return File(pdfOutput, "application/pdf", "Medication_Report.pdf");
        }
    }

}