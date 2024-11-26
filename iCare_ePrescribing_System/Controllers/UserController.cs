using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using iCare_ePrescribing_System.Repository;
using System.Net.Mail;
using System.Text;
using iCare_ePrescribing_System.Data;
using iCare_ePrescribing_System.ViewModels;
using iCare_ePrescribing_System.Models;
using System.Linq;

namespace iCare_ePrescribing_System.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<StaffMembers> _signInManager;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserStore<StaffMembers> _userStore;
        private readonly IUserEmailStore<StaffMembers> _emailStore;
        private readonly UserManager<StaffMembers> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHost;
        public UserController(
            SignInManager<StaffMembers> signInManager,
            IRoleRepository roleRepository,
            IWebHostEnvironment webHost,
            IUserStore<StaffMembers> userStore,
            UserManager<StaffMembers> userManager,
            IUserRepository userRepository,
            IEmailSender emailSender,
            ApplicationDbContext dbContext
            )
        {
            _roleRepository = roleRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _userRepository = userRepository;
            _webHost = webHost;
            _emailStore = GetEmailStore();
            _userStore = userStore;
            this._emailSender = emailSender;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _userRepository.GetUsers());
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetUser(id);
            var roles = await _roleRepository.GetRoles();
            if (user == null) return NotFound();
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
            var rolesItems = roles.Select(r => new SelectListItem(r.Name, r.Id, userRoles.Any(s => s.Contains(r.Name)))).ToList();
            var res = new UserViewModel
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Roles = rolesItems,
                Gender = user.Gender,
                Number = user.Number,
                BirthDate = user.BirthDate,
                image=user.image,
                HealthCouncilRegistrationNumber= user.HealthCouncilRegistrationNumber,
                ProfilePhoto=user.ProfilePhoto,
            };
            return View(res);
        }
        [HttpGet]
        private async Task<UserViewModel> LoadView(string id)
        {
            var user = await _userRepository.GetUser(id);
            var roles = await _roleRepository.GetRoles();
            if (user == null) return new UserViewModel();
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
            var rolesItems = roles.Select(r => new SelectListItem(r.Name, r.Id, userRoles.Any(s => s.Contains(r.Name)))).ToList();
            return new UserViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Gender= user.Gender,
                Number= user.Number,
                BirthDate= user.BirthDate,
                Id = user.Id,
                Roles = rolesItems,
                image= user.image,
                HealthCouncilRegistrationNumber = user.HealthCouncilRegistrationNumber,
                ProfilePhoto=user.ProfilePhoto,
            };
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var rolesCount = model.Roles.Count(s => s.Selected);
            string uniqueFileName = null;
            if (model.ConfirmPasswordOne!=model.PasswordOne || string.IsNullOrEmpty(model.Surname) | string.IsNullOrEmpty(model.Email) || rolesCount < 1)
            {
                if (model.ConfirmPasswordOne != model.PasswordOne)
                {
                    TempData["Error"] = "Your new password does not match confirmed password! New password must be the same as Confirmed password.";
                }
                return View(model);
            }
            if (model.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.ProfilePhoto.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            var user = await _userRepository.GetUser(model.Id);
            if (user == null) return NotFound();
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.Gender = model.Gender;
            user.Number = model.Number;
            user.BirthDate = model.BirthDate;
            if (model.image != null)
            {
                user.image = model.image;
            }
            else
            {
                user.image = uniqueFileName;
            }
            user.HealthCouncilRegistrationNumber= model.HealthCouncilRegistrationNumber;
            // Check for password change
            if (!string.IsNullOrWhiteSpace(model.PasswordOne))
            {
                var resetPassResult = await _signInManager.UserManager.ChangePasswordAsync(user, model.CurrentPassword,model.PasswordOne);
                if (!resetPassResult.Succeeded)
                {
                    foreach (var error in resetPassResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        TempData["ErrorPassword1"] = error.Description;
                    }
                    
                    return View(model);
                }
                if (resetPassResult.Succeeded)
                {
                    TempData["SuccessPassword"] = "Password Successfully changed!";
                    var receiver = user.Email;
                    var subject = "Password Reset";
                    var message = "Group 14 (Pharmacist) \n\nYour password has been successfully changed. If these changes were not made by you contact the hosptal administrator:\n\nKind regards\nBay Breeze Day Hospital\nbaybreeze@gmail.com\n041 583 2121";
                    try
                    {
                        await _emailSender.SendEmailAsync(receiver, subject, message);
                    }
                    catch (Exception ex)
                    {

                        TempData["ErrorMessage"] = "Email server is down, an email can't be sent at this time. please send your notification via a professional document";
                       
                    }
                }
            }

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                var assignedRole = userRoles.FirstOrDefault(s => s == role.Text);
                if (role.Selected && assignedRole == null)
                    await _signInManager.UserManager.AddToRoleAsync(user, role.Text);
                else if (assignedRole != null && !role.Selected)
                    await _signInManager.UserManager.RemoveFromRoleAsync(user, role.Text);
            }
            var updateUser = await _userRepository.UpdateUser(user);
            return RedirectToAction("Pharmacist", "Dashboard", new { area = "" });
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile(string id)
        {
            var user = await _userRepository.GetUser(id);
            var roles = await _roleRepository.GetRoles();
            if (user == null) return NotFound();
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
            var rolesItems = roles.Select(r => new SelectListItem(r.Name, r.Id, userRoles.Any(s => s.Contains(r.Name)))).ToList();
            var res = new UserViewModel
            {
                Email = user.Email,
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Roles = rolesItems,
                Gender = user.Gender,
                Number = user.Number,
                BirthDate = user.BirthDate,
                image = user.image,
                HealthCouncilRegistrationNumber = user.HealthCouncilRegistrationNumber,
                ProfilePhoto = user.ProfilePhoto,
            };
            return View(res);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(UserViewModel model)
        {
            var rolesCount = model.Roles.Count(s => s.Selected);
            string uniqueFileName = null;
            if (string.IsNullOrEmpty(model.Surname) | string.IsNullOrEmpty(model.Email) || rolesCount < 1)
            {
                TempData["Error"] = "Enter data to all required fields!";
                return View(model);
            }
            // Check if the file is uploaded
            if (model.ProfilePhoto != null)
            {
                // Allowed file types
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var fileExtension = Path.GetExtension(model.ProfilePhoto.FileName).ToLower();

                // Validate file format
                if (!allowedExtensions.Contains(fileExtension))
                {
                    
                    TempData["ErrorMessage"] = "Invalid file format. Please upload an image in JPG, JPEG, or PNG format.";
                    return View(model); 
                }
                
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.ProfilePhoto.CopyTo(new FileStream(filePath, FileMode.Create));
                
                TempData["SuccessMessage"] = "Profile photo uploaded successfully!";
            }

            var user = await _userRepository.GetUser(model.Id);
            if (user == null) return NotFound();
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.Gender = model.Gender;
            user.Number = model.Number;
            user.BirthDate = model.BirthDate;
            user.image = uniqueFileName;
            user.HealthCouncilRegistrationNumber = model.HealthCouncilRegistrationNumber;
            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                var assignedRole = userRoles.FirstOrDefault(s => s == role.Text);
                if (role.Selected && assignedRole == null)
                    await _signInManager.UserManager.AddToRoleAsync(user, role.Text);
                else if (assignedRole != null && !role.Selected)
                    await _signInManager.UserManager.RemoveFromRoleAsync(user, role.Text);
            }
            var updateUser = await _userRepository.UpdateUser(user);
            return RedirectToAction("Pharmacist", "Dashboard", new { area = "" });
        }
        public async Task<IActionResult> Delete(UserViewModel model)
        {
            if (model.Id == null) // Check for valid ID
            {
                TempData["Error"] = "User ID is not valid.";
                return RedirectToAction("Index", "User", new { area = "" });
            }

            var user = await _userRepository.GetUser(model.Id);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return NotFound();
            }

            bool deleteResult = await _userRepository.DeleteUser(user);
            if (!deleteResult)
            {
                TempData["Error"] = "Error deleting user. Please try again.";
                return View(model);
            }

            TempData["SuccessMessage"] = user.FullName+ " successfully Deregistered.";
            return RedirectToAction("Index", "User", new { area = "" });
        }
        private async Task<UserViewModel> LoadView()
        {
            var roles = await _roleRepository.GetRoles();
            var roleItems = roles.Select(r => new SelectListItem(r.Name, r.Id, false)).ToList();
            return new UserViewModel
            {
                Roles = roleItems,
            };
        }
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            return View(await LoadView());
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(UserViewModel model)
        {
            model.Staff=new Models.RegisteredStaff();
            string uniqueFileName = null;
            var rolesCount = model.Roles.Count(s => s.Selected);
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Surname) | string.IsNullOrEmpty(model.Email) || rolesCount < 1|| rolesCount > 1)
            {
                if (rolesCount < 1)
                {
                    TempData["ErrorRole"] = "Please select one role for the user!";
                }
                else if (rolesCount > 1)
                {
                    TempData["ErrorRole"] = "Please select one role for the user, not more than one!";
                }
                return View(model);
            }
            if (model.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.ProfilePhoto.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            var user = CreateUser();
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.Gender = model.Gender;
            user.HealthCouncilRegistrationNumber = model.HealthCouncilRegistrationNumber;
            user.Number = model.Number;
            user.BirthDate = model.BirthDate;
            user.image = model.image;
            model.Staff.Name = model.Name;
            model.Staff.Surname=model.Surname;
            model.Staff.Email= model.Email;
            model.Staff.Gender = model.Gender;
            model.Staff.HealthCouncilRegistrationNumber= model.HealthCouncilRegistrationNumber;
            model.Staff.Number = model.Number;
            model.Staff.BirthDate = model.BirthDate;
            model.image = uniqueFileName;
            string returnUrl = Url.Content("~/");
            var randomPassword = GenerateRandomPassword();
            string password = "Password123!";
            await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
            //await _emailStore.SetEmailAsync(user, model.Email,CancellationToken.None);
            var result = await _userManager.CreateAsync(user, password);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/User/ConfirmEmail",
                pageHandler: null,
                values: new { /*area = "Identity", */UserId = userId, token = code, returnUrl = returnUrl },
                protocol: Request.Scheme);
            if (result.Succeeded)
            {
                foreach (var role in model.Roles)
                {
                    if (role.Selected)
                    {
                        await _signInManager.UserManager.AddToRoleAsync(user, role.Text);
                        model.Staff.roletext = role.Text;
                        break;
                    }
                    
                }
                TempData["UserRegistered"] = $"{user.FullName}, succefully registered as a {model.Staff.roletext}";
                var receiver = user.Email;
                var subject = "Registration Password";
                var message = $"Group 14\n\nYou have been succefully registered as a {model.Staff.roletext},\n\nUsername:\n\n{user.Email}\n Please use the following password to access your Bay Breeze Day Hospital account:\n\n" + password+"\n\nKind regards\nBay Breeze Day Hospital";
                try
                {
                    await _emailSender.SendEmailAsync(receiver, subject, message);
                }
                catch (Exception ex)
                {

                    TempData["ErrorMessage"] = "Email server is down, an email can't be sent at this time. please send your notification via a professional document";
                }
                _dbContext.Add(model.Staff);
                _dbContext.SaveChanges();
                ViewBag.success = "User successfully registered as a staff member!";
                return RedirectToAction("Index", "User", new { area = "" });
            }
            else if (!result.Succeeded)
            {
                TempData["Errorusername"] = "The email has already been registered in the system, use a different email!";
            }
            return View(model);
        }
        //Code for generating random password
        private string GenerateRandomPassword()
        {
            const string chars = "ABClZvwxz01234!&@#$%/";
            var random = new Random();
            var password = new string(Enumerable.Repeat(chars, 12)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            return password;
        }
        //Return a picture
        private string GetUploadedFileName(StaffMembers staffMembers)
        {
            string uniqueFileName = null;

            if (staffMembers.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + staffMembers.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    staffMembers.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        [HttpGet]
        public IActionResult Succeeded()
        {
            if (TempData["Succeeded"] != null)
            {
                ViewBag.success = TempData["Succeeded"];
                TempData.Clear();
            }
            return View();
        }
        [HttpGet]
        public IActionResult AccessDenied()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
                TempData.Clear();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Error()
        {
            if (TempData["Error"] != null)
            {
                ViewBag.Error = TempData["Error"];
                TempData.Clear();
            }
            return View();
        }
        private StaffMembers CreateUser()
        {
            try
            {
                return Activator.CreateInstance<StaffMembers>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(StaffMembers)}'. " +
                    $"Ensure that '{nameof(StaffMembers)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<StaffMembers> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<StaffMembers>)_userStore;
        }

    }
}
