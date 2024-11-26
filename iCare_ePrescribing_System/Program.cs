using iCare_ePrescribing_System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using iCare_ePrescribing_System.Models;
using iCare_ePrescribing_System.Repository;
//using Microsoft.AspNetCore.Identity.UI.Services;
//using NuGet.Protocol;
using iCare_ePrescribing_System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<StaffMembers>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<UserManager<StaffMembers>>();
builder.Services.AddScoped<SignInManager<StaffMembers>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//builder.Services.AddTransient<iCare_ePrescribing_System.EmailSender.IEmailSender, EmailSender>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Pharmacist", "Surgeon", "Nurse", "Admin", "Patient" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<StaffMembers>>();
    string email = "admin@gmail.com";
    string password = "Password123!";
    string name = "Bongi";
    string surname = "Gxekiwe";
    string gender = "Female";
    int number = 098776667;
    string image = null;
    DateTime date = new DateTime();
    string healthcouncilregistrationnumber = "HC8787889";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new StaffMembers();
        user.Email = email;
        user.UserName = email;
        user.Name = name;
        user.Surname = surname;
        user.Gender = gender;
        user.Number = number;
        user.HealthCouncilRegistrationNumber= healthcouncilregistrationnumber;
        user.BirthDate = date;
        user.image= image;
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}
app.Run();
