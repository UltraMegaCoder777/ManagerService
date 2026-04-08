using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManagerService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:document_check_status", "не_проверен,отклонён,утверждён")
                .Annotation("Npgsql:Enum:employee_role", "supervisor,manager")
                .Annotation("Npgsql:Enum:manager_slot_status", "отменен,черновик,подтвержден,опубликован,занят")
                .Annotation("Npgsql:Enum:practice_format", "очная,дистанционная,гибридная")
                .Annotation("Npgsql:Enum:signing_document_status", "не_подписан,подписан_университетом,подписан_ламой,подписан_всеми");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    IdDepartment = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.IdDepartment);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    IdDocumentType = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DocumentName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.IdDocumentType);
                });

            migrationBuilder.CreateTable(
                name: "PracticeTypes",
                columns: table => new
                {
                    IdPracticeType = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeTypes", x => x.IdPracticeType);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    IdSpecialization = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.IdSpecialization);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    IdAddress = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdDepartment = table.Column<int>(type: "integer", nullable: false),
                    FullAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.IdAddress);
                    table.ForeignKey(
                        name: "FK_Addresses_Departments_IdDepartment",
                        column: x => x.IdDepartment,
                        principalTable: "Departments",
                        principalColumn: "IdDepartment",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    IdEmployee = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Patronymic = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PersonnelNumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Position = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IdDepartment = table.Column<int>(type: "integer", nullable: true),
                    Role = table.Column<int>(type: "employee_role", nullable: false),
                    Phone = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Login = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.IdEmployee);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_IdDepartment",
                        column: x => x.IdDepartment,
                        principalTable: "Departments",
                        principalColumn: "IdDepartment",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsForSpecialization",
                columns: table => new
                {
                    IdDocumentType = table.Column<int>(type: "integer", nullable: false),
                    IdSpecialization = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsForSpecialization", x => new { x.IdDocumentType, x.IdSpecialization });
                    table.ForeignKey(
                        name: "FK_DocumentsForSpecialization_DocumentTypes_IdDocumentType",
                        column: x => x.IdDocumentType,
                        principalTable: "DocumentTypes",
                        principalColumn: "IdDocumentType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentsForSpecialization_Specializations_IdSpecialization",
                        column: x => x.IdSpecialization,
                        principalTable: "Specializations",
                        principalColumn: "IdSpecialization",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledPractices",
                columns: table => new
                {
                    IdScheduledPractice = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdSpecialization = table.Column<int>(type: "integer", nullable: false),
                    IdPracticeType = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledPractices", x => x.IdScheduledPractice);
                    table.ForeignKey(
                        name: "FK_ScheduledPractices_PracticeTypes_IdPracticeType",
                        column: x => x.IdPracticeType,
                        principalTable: "PracticeTypes",
                        principalColumn: "IdPracticeType",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduledPractices_Specializations_IdSpecialization",
                        column: x => x.IdSpecialization,
                        principalTable: "Specializations",
                        principalColumn: "IdSpecialization",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ManagerIntervals",
                columns: table => new
                {
                    IdManagerInterval = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEmployee = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaxCount = table.Column<int>(type: "integer", nullable: false),
                    BreakDuration = table.Column<TimeSpan>(type: "interval", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    managerIdEmployee = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerIntervals", x => x.IdManagerInterval);
                    table.ForeignKey(
                        name: "FK_ManagerIntervals_Employees_managerIdEmployee",
                        column: x => x.managerIdEmployee,
                        principalTable: "Employees",
                        principalColumn: "IdEmployee",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ManagerSlots",
                columns: table => new
                {
                    IdManagerSlot = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEmployee = table.Column<int>(type: "integer", nullable: false),
                    IdManagerInterval = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "manager_slot_status", nullable: false),
                    MeetingPlace = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerSlots", x => x.IdManagerSlot);
                    table.ForeignKey(
                        name: "FK_ManagerSlots_ManagerIntervals_IdManagerInterval",
                        column: x => x.IdManagerInterval,
                        principalTable: "ManagerIntervals",
                        principalColumn: "IdManagerInterval",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ManagerInterviews",
                columns: table => new
                {
                    IdManagerSlot = table.Column<int>(type: "integer", nullable: false),
                    IdStudent = table.Column<int>(type: "integer", nullable: false),
                    IdStudentApplication = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<bool>(type: "boolean", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerInterviews", x => x.IdManagerSlot);
                    table.ForeignKey(
                        name: "FK_ManagerInterviews_ManagerSlots_IdManagerSlot",
                        column: x => x.IdManagerSlot,
                        principalTable: "ManagerSlots",
                        principalColumn: "IdManagerSlot",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_IdDepartment",
                table: "Addresses",
                column: "IdDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsForSpecialization_IdSpecialization",
                table: "DocumentsForSpecialization",
                column: "IdSpecialization");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IdDepartment",
                table: "Employees",
                column: "IdDepartment");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Login",
                table: "Employees",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonnelNumber",
                table: "Employees",
                column: "PersonnelNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ManagerIntervals_managerIdEmployee",
                table: "ManagerIntervals",
                column: "managerIdEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerSlots_IdManagerInterval",
                table: "ManagerSlots",
                column: "IdManagerInterval");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerSlots_StartTime",
                table: "ManagerSlots",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerSlots_Status",
                table: "ManagerSlots",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledPractices_IdPracticeType",
                table: "ScheduledPractices",
                column: "IdPracticeType");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledPractices_IdSpecialization",
                table: "ScheduledPractices",
                column: "IdSpecialization");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "DocumentsForSpecialization");

            migrationBuilder.DropTable(
                name: "ManagerInterviews");

            migrationBuilder.DropTable(
                name: "ScheduledPractices");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "ManagerSlots");

            migrationBuilder.DropTable(
                name: "PracticeTypes");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "ManagerIntervals");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
