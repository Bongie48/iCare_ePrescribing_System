using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iCare_ePrescribing_System.Migrations
{
    public partial class database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Identity");

            migrationBuilder.CreateTable(
                name: "ActiveIngredients",
                schema: "Identity",
                columns: table => new
                {
                    ActiveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveIngredients", x => x.ActiveID);
                });

            migrationBuilder.CreateTable(
                name: "Anaesthesiologists",
                schema: "Identity",
                columns: table => new
                {
                    AnaesthesiologistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anaesthesiologists", x => x.AnaesthesiologistID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HealthCouncilRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Condition",
                schema: "Identity",
                columns: table => new
                {
                    ConditionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.ConditionID);
                });

            migrationBuilder.CreateTable(
                name: "DosageForm",
                schema: "Identity",
                columns: table => new
                {
                    DosageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DosageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DosageForm", x => x.DosageID);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                schema: "Identity",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceId);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                schema: "Identity",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                schema: "Identity",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HealthCouncilRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    roletext = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "Surgeon",
                schema: "Identity",
                columns: table => new
                {
                    SurgeonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthCounsilRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surgeon", x => x.SurgeonId);
                });

            migrationBuilder.CreateTable(
                name: "Theatres",
                schema: "Identity",
                columns: table => new
                {
                    TheatreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TheatreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theatres", x => x.TheatreId);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentCodes",
                schema: "Identity",
                columns: table => new
                {
                    TreatmentCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentCodes", x => x.TreatmentCodeId);
                });

            migrationBuilder.CreateTable(
                name: "UserStocks",
                schema: "Identity",
                columns: table => new
                {
                    StockID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStocks", x => x.StockID);
                });

            migrationBuilder.CreateTable(
                name: "Vitals",
                schema: "Identity",
                columns: table => new
                {
                    VitalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lowlimit = table.Column<double>(type: "float", nullable: false),
                    Highlimit = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vitals", x => x.VitalId);
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                schema: "Identity",
                columns: table => new
                {
                    WardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOfBeds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => x.WardID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "Identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Identity",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "Identity",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "Identity",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalSchema: "Identity",
                        principalTable: "Province",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddMedication",
                schema: "Identity",
                columns: table => new
                {
                    MedicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReOrder = table.Column<int>(type: "int", nullable: false),
                    ReOrderStatus = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    dosageID = table.Column<int>(type: "int", nullable: false),
                    scheduleID = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddMedication", x => x.MedicationId);
                    table.ForeignKey(
                        name: "FK_AddMedication_DosageForm_dosageID",
                        column: x => x.dosageID,
                        principalSchema: "Identity",
                        principalTable: "DosageForm",
                        principalColumn: "DosageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddMedication_Schedule_scheduleID",
                        column: x => x.scheduleID,
                        principalSchema: "Identity",
                        principalTable: "Schedule",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bed",
                schema: "Identity",
                columns: table => new
                {
                    BedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BedNo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bed", x => x.BedID);
                    table.ForeignKey(
                        name: "FK_Bed_Wards_WardID",
                        column: x => x.WardID,
                        principalSchema: "Identity",
                        principalTable: "Wards",
                        principalColumn: "WardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suburb",
                schema: "Identity",
                columns: table => new
                {
                    SuburbId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suburb", x => x.SuburbId);
                    table.ForeignKey(
                        name: "FK_Suburb_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "Identity",
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationIngredients",
                schema: "Identity",
                columns: table => new
                {
                    MedIngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    activeid = table.Column<int>(type: "int", nullable: false),
                    medicineid = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationIngredients", x => x.MedIngredientId);
                    table.ForeignKey(
                        name: "FK_MedicationIngredients_ActiveIngredients_activeid",
                        column: x => x.activeid,
                        principalSchema: "Identity",
                        principalTable: "ActiveIngredients",
                        principalColumn: "ActiveID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationIngredients_AddMedication_medicineid",
                        column: x => x.medicineid,
                        principalSchema: "Identity",
                        principalTable: "AddMedication",
                        principalColumn: "MedicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationOrders",
                schema: "Identity",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderQuantity = table.Column<int>(type: "int", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentQuantity = table.Column<int>(type: "int", nullable: false),
                    medicineid = table.Column<int>(type: "int", nullable: false),
                    StockUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationOrders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_MedicationOrders_AddMedication_medicineid",
                        column: x => x.medicineid,
                        principalSchema: "Identity",
                        principalTable: "AddMedication",
                        principalColumn: "MedicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationOrders_UserStocks_StockUserID",
                        column: x => x.StockUserID,
                        principalSchema: "Identity",
                        principalTable: "UserStocks",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceivedStock",
                schema: "Identity",
                columns: table => new
                {
                    ReceivedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceivedQuantity = table.Column<int>(type: "int", nullable: false),
                    CurrentQuantity = table.Column<int>(type: "int", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReceivedStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivedStock", x => x.ReceivedID);
                    table.ForeignKey(
                        name: "FK_ReceivedStock_AddMedication_MedicationID",
                        column: x => x.MedicationID,
                        principalSchema: "Identity",
                        principalTable: "AddMedication",
                        principalColumn: "MedicationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "Identity",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuburbId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Suburb_SuburbId",
                        column: x => x.SuburbId,
                        principalSchema: "Identity",
                        principalTable: "Suburb",
                        principalColumn: "SuburbId");
                });

            migrationBuilder.CreateTable(
                name: "Admission",
                schema: "Identity",
                columns: table => new
                {
                    AdmisionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DischargeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    BedID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admission", x => x.AdmisionID);
                    table.ForeignKey(
                        name: "FK_Admission_Bed_BedID",
                        column: x => x.BedID,
                        principalSchema: "Identity",
                        principalTable: "Bed",
                        principalColumn: "BedID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Admission_Patients_PatientID",
                        column: x => x.PatientID,
                        principalSchema: "Identity",
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Allergies",
                schema: "Identity",
                columns: table => new
                {
                    AllegyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ActiveIngredientActiveID = table.Column<int>(type: "int", nullable: false),
                    ActiveID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.AllegyId);
                    table.ForeignKey(
                        name: "FK_Allergies_ActiveIngredients_ActiveIngredientActiveID",
                        column: x => x.ActiveIngredientActiveID,
                        principalSchema: "Identity",
                        principalTable: "ActiveIngredients",
                        principalColumn: "ActiveID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allergies_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Identity",
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                schema: "Identity",
                columns: table => new
                {
                    MedicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Strength = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DosageFormDosageID = table.Column<int>(type: "int", nullable: false),
                    DosageID = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.MedicationId);
                    table.ForeignKey(
                        name: "FK_Medications_DosageForm_DosageFormDosageID",
                        column: x => x.DosageFormDosageID,
                        principalSchema: "Identity",
                        principalTable: "DosageForm",
                        principalColumn: "DosageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medications_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Identity",
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "PatientConditions",
                schema: "Identity",
                columns: table => new
                {
                    PatientConditionsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ConditionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientConditions", x => x.PatientConditionsId);
                    table.ForeignKey(
                        name: "FK_PatientConditions_Condition_ConditionID",
                        column: x => x.ConditionID,
                        principalSchema: "Identity",
                        principalTable: "Condition",
                        principalColumn: "ConditionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientConditions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Identity",
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                schema: "Identity",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrgentStatus = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    TrackStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionId);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Identity",
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Staff_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "Identity",
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Surgeries",
                schema: "Identity",
                columns: table => new
                {
                    SurgeryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurgeonId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    SurgeryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Session = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnaesthesiologistId = table.Column<int>(type: "int", nullable: true),
                    TheatreId = table.Column<int>(type: "int", nullable: true),
                    TreatmentCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surgeries", x => x.SurgeryId);
                    table.ForeignKey(
                        name: "FK_Surgeries_Anaesthesiologists_AnaesthesiologistId",
                        column: x => x.AnaesthesiologistId,
                        principalSchema: "Identity",
                        principalTable: "Anaesthesiologists",
                        principalColumn: "AnaesthesiologistID");
                    table.ForeignKey(
                        name: "FK_Surgeries_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Identity",
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surgeries_Surgeon_SurgeonId",
                        column: x => x.SurgeonId,
                        principalSchema: "Identity",
                        principalTable: "Surgeon",
                        principalColumn: "SurgeonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Surgeries_Theatres_TheatreId",
                        column: x => x.TheatreId,
                        principalSchema: "Identity",
                        principalTable: "Theatres",
                        principalColumn: "TheatreId");
                });

            migrationBuilder.CreateTable(
                name: "PatientVitals",
                schema: "Identity",
                columns: table => new
                {
                    PatientVitalsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaptureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Readings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VitalsVitalId = table.Column<int>(type: "int", nullable: false),
                    VitalId = table.Column<int>(type: "int", nullable: false),
                    AdmissionAdmisionID = table.Column<int>(type: "int", nullable: false),
                    AdmisionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVitals", x => x.PatientVitalsId);
                    table.ForeignKey(
                        name: "FK_PatientVitals_Admission_AdmissionAdmisionID",
                        column: x => x.AdmissionAdmisionID,
                        principalSchema: "Identity",
                        principalTable: "Admission",
                        principalColumn: "AdmisionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientVitals_Vitals_VitalsVitalId",
                        column: x => x.VitalsVitalId,
                        principalSchema: "Identity",
                        principalTable: "Vitals",
                        principalColumn: "VitalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientMedication",
                schema: "Identity",
                columns: table => new
                {
                    PatientMedicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientMedication", x => x.PatientMedicationId);
                    table.ForeignKey(
                        name: "FK_PatientMedication_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalSchema: "Identity",
                        principalTable: "Medications",
                        principalColumn: "MedicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientMedication_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Identity",
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionItem",
                schema: "Identity",
                columns: table => new
                {
                    itemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedID = table.Column<int>(type: "int", nullable: false),
                    PresID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    instruction = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionItem", x => x.itemId);
                    table.ForeignKey(
                        name: "FK_PrescriptionItem_AddMedication_MedID",
                        column: x => x.MedID,
                        principalSchema: "Identity",
                        principalTable: "AddMedication",
                        principalColumn: "MedicationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionItem_Prescriptions_PresID",
                        column: x => x.PresID,
                        principalSchema: "Identity",
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RejectPrescription",
                schema: "Identity",
                columns: table => new
                {
                    RejectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RejectReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectedPresID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RejectPrescription", x => x.RejectId);
                    table.ForeignKey(
                        name: "FK_RejectPrescription_Prescriptions_RejectedPresID",
                        column: x => x.RejectedPresID,
                        principalSchema: "Identity",
                        principalTable: "Prescriptions",
                        principalColumn: "PrescriptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddMedication_dosageID",
                schema: "Identity",
                table: "AddMedication",
                column: "dosageID");

            migrationBuilder.CreateIndex(
                name: "IX_AddMedication_scheduleID",
                schema: "Identity",
                table: "AddMedication",
                column: "scheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_Admission_BedID",
                schema: "Identity",
                table: "Admission",
                column: "BedID");

            migrationBuilder.CreateIndex(
                name: "IX_Admission_PatientID",
                schema: "Identity",
                table: "Admission",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_ActiveIngredientActiveID",
                schema: "Identity",
                table: "Allergies",
                column: "ActiveIngredientActiveID");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_PatientId",
                schema: "Identity",
                table: "Allergies",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "Identity",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Identity",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "Identity",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "Identity",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "Identity",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Identity",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Identity",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bed_WardID",
                schema: "Identity",
                table: "Bed",
                column: "WardID");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                schema: "Identity",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationIngredients_activeid",
                schema: "Identity",
                table: "MedicationIngredients",
                column: "activeid");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationIngredients_medicineid",
                schema: "Identity",
                table: "MedicationIngredients",
                column: "medicineid");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationOrders_medicineid",
                schema: "Identity",
                table: "MedicationOrders",
                column: "medicineid");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationOrders_StockUserID",
                schema: "Identity",
                table: "MedicationOrders",
                column: "StockUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DosageFormDosageID",
                schema: "Identity",
                table: "Medications",
                column: "DosageFormDosageID");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_PatientId",
                schema: "Identity",
                table: "Medications",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientConditions_ConditionID",
                schema: "Identity",
                table: "PatientConditions",
                column: "ConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientConditions_PatientId",
                schema: "Identity",
                table: "PatientConditions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedication_MedicationId",
                schema: "Identity",
                table: "PatientMedication",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedication_PatientId",
                schema: "Identity",
                table: "PatientMedication",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_SuburbId",
                schema: "Identity",
                table: "Patients",
                column: "SuburbId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_AdmissionAdmisionID",
                schema: "Identity",
                table: "PatientVitals",
                column: "AdmissionAdmisionID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_VitalsVitalId",
                schema: "Identity",
                table: "PatientVitals",
                column: "VitalsVitalId");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItem_MedID",
                schema: "Identity",
                table: "PrescriptionItem",
                column: "MedID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionItem_PresID",
                schema: "Identity",
                table: "PrescriptionItem",
                column: "PresID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MemberId",
                schema: "Identity",
                table: "Prescriptions",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PatientId",
                schema: "Identity",
                table: "Prescriptions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceivedStock_MedicationID",
                schema: "Identity",
                table: "ReceivedStock",
                column: "MedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_RejectPrescription_RejectedPresID",
                schema: "Identity",
                table: "RejectPrescription",
                column: "RejectedPresID");

            migrationBuilder.CreateIndex(
                name: "IX_Suburb_CityId",
                schema: "Identity",
                table: "Suburb",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeries_AnaesthesiologistId",
                schema: "Identity",
                table: "Surgeries",
                column: "AnaesthesiologistId");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeries_PatientId",
                schema: "Identity",
                table: "Surgeries",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeries_SurgeonId",
                schema: "Identity",
                table: "Surgeries",
                column: "SurgeonId");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeries_TheatreId",
                schema: "Identity",
                table: "Surgeries",
                column: "TheatreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergies",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "MedicationIngredients",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "MedicationOrders",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "PatientConditions",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "PatientMedication",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "PatientVitals",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "PrescriptionItem",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ReceivedStock",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "RejectPrescription",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Surgeries",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "TreatmentCodes",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "ActiveIngredients",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserStocks",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Condition",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Medications",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Admission",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Vitals",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "AddMedication",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Prescriptions",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Anaesthesiologists",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Surgeon",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Theatres",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Bed",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "DosageForm",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Schedule",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Staff",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Wards",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Suburb",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "City",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Province",
                schema: "Identity");
        }
    }
}
