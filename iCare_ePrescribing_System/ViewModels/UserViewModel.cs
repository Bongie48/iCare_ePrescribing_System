using iCare_ePrescribing_System.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace iCare_ePrescribing_System.ViewModels
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        [DisplayName("Roles")]
        public IList<SelectListItem>? Roles { get; set; }
        public string Gender { get; set; }
        public int Number { get; set; }
        public DateTime BirthDate { get; set; }
        public string HealthCouncilRegistrationNumber { get; set; }
        //For editing 
        public string? PasswordOne { get;set; }
        public string? ConfirmPasswordOne { get; set; }
        public string? CurrentPassword { get; set; } = string.Empty;
        public IFormFile ProfilePhoto { get; set; }
        public string? image { get; set; }
        public RegisteredStaff Staff { get; set; }
        public string userID { get; set; }
        public string profile { get; set; }
        //Role by text
        public string roletext { get; set; }
    }
}
