using iCare_ePrescribing_System.Data;
using iCare_ePrescribing_System.Models;
using iCare_ePrescribing_System.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;

namespace iCare_ePrescribing_System.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<StaffMembers> _userManager;
        public AdminController(ApplicationDbContext dbContext,
            UserManager<StaffMembers> userManager)
        {
            this.dbContext = dbContext;
            _userManager = userManager; 
        }
        
        //Add Dosage form
        public async Task<IActionResult> MedDosageForm()
        {
            DosageViewModel dosageView = new DosageViewModel();
            dosageView.dosage = new DosageForm();
            IEnumerable<DosageForm> list = dbContext.DosageForm.OrderBy(x=>x.DosageName);
            dosageView.dosagelist = list;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            TempData["FullName"] = user.FullName;
            TempData["image"] = user.image;
            return View(dosageView);
        }
        [HttpPost]
        public IActionResult MedDosageForm(DosageViewModel dosageForm)
        {
            dbContext.DosageForm.Add(dosageForm.dosage);
            dbContext.SaveChanges();
            TempData["Message1"] = $"{dosageForm.dosage.DosageName} successfully added.";
            return RedirectToAction("MedDosageForm");
        }
        //Add Schedule
        public async Task<IActionResult> MedSchedules()
        {
            ScheduleViewModel scheduleView = new ScheduleViewModel();  
            scheduleView.Schedule=new Schedule();
            IEnumerable<Schedule> list = dbContext.Schedule.OrderBy(x=>x.ScheduleName);
            scheduleView.schedulelist = list;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            TempData["FullName"] = user.FullName;
            TempData["image"] = user.image;
            return View(scheduleView);
        }
        [HttpPost]
        public IActionResult MedSchedules(ScheduleViewModel record)
        {
            dbContext.Schedule.Add(record.Schedule);
            dbContext.SaveChanges();
            TempData["Message2"] = $"Schedule {record.Schedule.ScheduleName} successfully added.";
            return RedirectToAction("MedSchedules");
        }
        //Add medicine ingredients
        public IActionResult MedIngedients()
        {
            activeViewModel activeView=new activeViewModel();
            activeView.ActiveIngredient=new ActiveIngredient();
            IEnumerable<ActiveIngredient> list = dbContext.ActiveIngredients.OrderBy(x=>x.ActiveName);
            activeView.ingredientslist = list;
            return View(activeView);
        }
        [HttpPost]
        public async Task<IActionResult> MedIngedients(activeViewModel active)
        {
            dbContext.ActiveIngredients.Add(active.ActiveIngredient);
            dbContext.SaveChanges();
            TempData["Message3"] = $"{active.ActiveIngredient.ActiveName} successfully added.";
            return RedirectToAction("MedIngedients");
        }
        //Update dosage form
        public IActionResult UpdateDosage(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var dbC = dbContext.DosageForm.Find(ID);
            if (dbC == null)
            {
                return NotFound();
            }
            return View(dbC);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateDosage(DosageForm record)
        {
            dbContext.DosageForm.Update(record);
            dbContext.SaveChanges();
            TempData["Message11"] = $"{record.DosageName} Successfully Update!";
            return RedirectToAction("MedDosageForm");
        }
        //Deleting Dosage Form
        public IActionResult DosageDelete(int? Id)
        {
            var dbC = dbContext.DosageForm.Find(Id);
            if (dbC == null)
            {
                return NotFound();
            }
            dbContext.DosageForm.Remove(dbC);
            dbContext.SaveChanges();
            TempData["Message111"] = $"You have successfully deleted {dbC.DosageName}!";
            return RedirectToAction("MedDosageForm");
        }
        //Update schedule
        public IActionResult UpdateSchedule(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var dbC = dbContext.Schedule.Find(ID);
            if (dbC == null)
            {
                return NotFound();
            }
            return View(dbC);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSchedule(Schedule record)
        {
            dbContext.Schedule.Update(record);
            dbContext.SaveChanges();
            TempData["Message222"] = $"Schedule {record.ScheduleName} Successfully Updated!";
            return RedirectToAction("MedSchedules");
        }
        //Deleting Schedule
        public IActionResult ScheduleDelete(int? Id)
        {
            var dbC = dbContext.Schedule.Find(Id);
            if (dbC == null)
            {
                return NotFound();
            }
            dbContext.Schedule.Remove(dbC);
            dbContext.SaveChanges();
            TempData["Message2222"] = $"You have successfully deleted Schedule {dbC.ScheduleName}!";
            return RedirectToAction("MedSchedules");
        }
        //Update active ingredients
        public IActionResult UpdateActive(int? ID)
        {
            if (ID == null || ID == 0)
            {
                return NotFound();
            }
            var dbC = dbContext.ActiveIngredients.Find(ID);
            if (dbC == null)
            {
                return NotFound();
            }
            return View(dbC);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateActive(ActiveIngredient record)
        {
            dbContext.ActiveIngredients.Update(record);
            dbContext.SaveChanges();
            TempData["Message22"] = $"{record.ActiveName} Successfully Updated!";
            return RedirectToAction("MedIngedients");
        }
        //Deleting active
        public IActionResult ActiveDelete(int? Id)
        {
            var dbC = dbContext.ActiveIngredients.Find(Id);
            if (dbC == null)
            {
                return NotFound();
            }
            dbContext.ActiveIngredients.Remove(dbC);
            dbContext.SaveChanges();
            TempData["Message333"] = $"You have successfully deleted {dbC.ActiveName}!";
            return RedirectToAction("MedIngedients");
        }


        // GET: DayHospital
        public IActionResult Index()
        {
            return View(dbContext.DayHospitals.ToList());
        }

        // GET: DayHospital/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DayHospital/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Address,ContactNumber,EmailAddress,PurchaseManagerEmail")] DayHospital dayHospital)
        {
            if (ModelState.IsValid)
            {
                dbContext.DayHospitals.Add(dayHospital);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(dayHospital);
        }

        // GET: DayHospital/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayHospital = dbContext.DayHospitals.Find(id);
            if (dayHospital == null)
            {
                return NotFound();
            }
            return View(dayHospital);
        }

        // POST: DayHospital/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Address,ContactNumber,EmailAddress,PurchaseManagerEmail")] DayHospital dayHospital)
        {
            if (id != dayHospital.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbContext.Update(dayHospital);
                    dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayHospitalExists(dayHospital.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dayHospital);
        }

        // GET: DayHospital/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayHospital = dbContext.DayHospitals
                .FirstOrDefault(m => m.Id == id);
            if (dayHospital == null)
            {
                return NotFound();
            }

            return View(dayHospital);
        }

        // POST: DayHospital/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var dayHospital = dbContext.DayHospitals.Find(id);
            dbContext.DayHospitals.Remove(dayHospital);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool DayHospitalExists(int id)
        {
            return dbContext.DayHospitals.Any(e => e.Id == id);
        }
        
        // - Wards and beds management
        public async Task<IActionResult> Ward()
        {
            var ward = await dbContext.Wards.ToListAsync();

            return View(ward);
        }

        public IActionResult CreateWard()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateWard([Bind("WardID, Name, NoOfBeds")] Ward ward)
        {
            dbContext.Wards.Add(ward);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Ward));
        }

        public async Task<IActionResult> ViewBeds(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bed = await dbContext.Bed.Where(b => b.WardID == id)
                .OrderBy(b => b.WardID) 
                .ToListAsync();

            if (bed == null)
            {
                return NotFound();
            }

            ViewData["WardID"] = id;
            return View(bed);
        }
        [HttpGet]
        public IActionResult CreateBed(int? id)
        {
            ViewData["WardID"] = id;
            return View();
        }
            
        [HttpPost]
        public async Task<IActionResult> CreateBed(Bed bed)
        {
            if(bed == null)
            {
                return NotFound();
            }

            var existingBedsCount = await dbContext.Bed.CountAsync(b => b.WardID == bed.WardID);

            var maxBeds = await dbContext.Wards
                .Where(w => w.WardID == bed.WardID)
                .Select(w => w.NoOfBeds)
                .FirstOrDefaultAsync();

            if (existingBedsCount < maxBeds)
            {
                var beds = new Bed
                {
                    BedNo = bed.BedNo,
                    Status = bed.Status,
                    WardID = bed.WardID
                };

                dbContext.Bed.Add(bed);
                dbContext.SaveChanges();
                return RedirectToAction(nameof(ViewBeds), new { id = bed.WardID });
            }

            return View(bed);
        }

        // - Condition diagnosis Records
        public async Task<IActionResult> ConditionDiagnosis()
        {   
            var conditionList = new ConditionsDiagnosisVM
            {
                ConditionList = await dbContext.Condition.ToListAsync()
            };
            return View(conditionList);
        }
        [HttpPost]
        public async Task<IActionResult> ConditionDiagnosis(ConditionsDiagnosisVM conditionVM)
        {
            if(conditionVM == null)
            {
                return NotFound();
            }

            var conditions = new Condition
            {
                ConditionCode = conditionVM.Condition.ConditionCode,
                ConditionName = conditionVM.Condition.ConditionName
            };

            dbContext.Condition.Add(conditions);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(ConditionDiagnosis));
        }

        // - Vital range management
        public async Task<IActionResult> VitalRange()
        {
            var VitalRange = new VitalRangeVM
            {
                VitalsList = await dbContext.Vitals.ToListAsync()
            };
            return View(VitalRange);
        }
        [HttpPost]
        public async Task<IActionResult> VitalRange(VitalRangeVM vitalRange)
        {
            if (vitalRange == null)
                return NotFound();

            var range = new Vitals()
            {
                Name = vitalRange.Vitals.Name,
                Lowlimit = vitalRange.Vitals.Lowlimit,
                Highlimit = vitalRange.Vitals.Highlimit
            };

            dbContext.Vitals.Add(range);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(VitalRange));
        }
        public IActionResult MedContra()
        {
            var contraIndicationVM = new ContraIndicationVM
            {
                ConditionName = dbContext.Condition.OrderBy(n => n.ConditionName)
             .Select(n => new SelectListItem
             {
                 Value = n.ConditionID.ToString(),
                 Text = n.ConditionName
             }).ToList(),

                Active = dbContext.ActiveIngredients
                .OrderBy(n=> n.ActiveName)
                .Select(n => new SelectListItem
                {
                    Value = n.ActiveID.ToString(),
                    Text = n.ActiveName
                }).ToList(), // Fetch available active ingredients

                MedInteractionsList = dbContext.MedInteraction
                .OrderBy(n=>n.Condition.ConditionCode)
                .Include(x => x.Condition)
                .Include(x=>x.ActiveIngredient)
                .ToList()
            };

            return View(contraIndicationVM);

        }
        [HttpPost]
        public IActionResult MedContra(ContraIndicationVM contraIndication)
        {
            foreach (var activeId in contraIndication.SelectedActiveIngredientIDs)
            {
                var newInteraction = new MedInteraction
                {
                    MedicalConditionID = contraIndication.MedInteraction.MedicalConditionID,
                    InteractionActiveID = activeId
                };
                dbContext.MedInteraction.Add(newInteraction);
            }
            dbContext.SaveChanges();
            TempData["Message101"] = "Contra-Indications added successfully!";
            return RedirectToAction("MedContra");
        }
        public IActionResult MedInteraction()
        {
            var medicationInteractionVM = new MedicationInteractionVM
            {
              ActiveName = dbContext.ActiveIngredients
              .OrderBy(n => n.ActiveName)
             .Select(n => new SelectListItem
             {
                 Value = n.ActiveID.ToString(),
                 Text = n.ActiveName
             }).ToList(),

                Active = dbContext.ActiveIngredients
                .OrderBy(n => n.ActiveName)
                .Select(n => new SelectListItem
                {
                    Value = n.ActiveID.ToString(),
                    Text = n.ActiveName
                }).ToList(),

                MedInteractionsList = dbContext.MedContra
                .OrderBy(n => n.ActiveIngredient.ActiveName)
                .Include(x=>x.ActiveInteraction)
                .Include(x => x.ActiveIngredient)
                .ToList()
            };

            return View(medicationInteractionVM);

        }
        [HttpPost]
        public IActionResult MedInteraction(MedicationInteractionVM MedicationInteraction)
        {
            foreach (var activeId in MedicationInteraction.SelectedActiveIngredientIDs)
            {
                var newInteraction = new MedContra
                {
                    CurrentActiveID = MedicationInteraction.MedContra.CurrentActiveID,
                    InteratingActiveID = activeId
                };
                dbContext.MedContra.Add(newInteraction);
            }
            dbContext.SaveChanges();
            TempData["Message102"] = "Medication Interaction added successfully!";
            return RedirectToAction("MedInteraction");
        }
        // - Provinces, Cities and Surbub Records
    }
}
