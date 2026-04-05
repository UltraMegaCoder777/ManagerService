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
                .Annotation("Npgsql:Enum:interview_slot_status", "отменен,черновик,предложен_руководителю,подтвержден,опубликован,занят")
                .Annotation("Npgsql:Enum:interview_type", "менеджер,руководитель")
                .Annotation("Npgsql:Enum:practice_format", "очная,дистанционная,гибридная")
                .Annotation("Npgsql:Enum:signing_document_status", "не_подписан,подписан_университетом,подписан_ламой,подписан_всеми")
                .Annotation("Npgsql:Enum:student_application_status", "на_рассмотрении_руководителем,отказано,собеседование,оформление_документов,принят")
                .Annotation("Npgsql:Enum:supervisor_application_status", "отменена,шаблон,отправлена,удовлетворена,неудовлетворена,закрыта");

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
                name: "StudentApplications",
                columns: table => new
                {
                    IdStudentApplication = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPracticeType = table.Column<int>(type: "integer", nullable: false),
                    IdScheduledPractice = table.Column<int>(type: "integer", nullable: false),
                    IdSpecialization = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentApplications", x => x.IdStudentApplication);
                    table.ForeignKey(
                        name: "FK_StudentApplications_StudentApplications_IdPracticeType",
                        column: x => x.IdPracticeType,
                        principalTable: "StudentApplications",
                        principalColumn: "IdStudentApplication",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentApplications_StudentApplications_IdScheduledPractice",
                        column: x => x.IdScheduledPractice,
                        principalTable: "StudentApplications",
                        principalColumn: "IdStudentApplication",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentApplications_StudentApplications_IdSpecialization",
                        column: x => x.IdSpecialization,
                        principalTable: "StudentApplications",
                        principalColumn: "IdStudentApplication",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupervisorApplications",
                columns: table => new
                {
                    IdSupervisorApplication = table.Column<Guid>(type: "uuid", nullable: false),
                    IdEmployee = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IdSpecialization = table.Column<int>(type: "integer", nullable: false),
                    IdDepartment = table.Column<int>(type: "integer", nullable: false),
                    IdAddress = table.Column<int>(type: "integer", nullable: false),
                    IdScheduledPractice = table.Column<int>(type: "integer", nullable: true),
                    RequestedStudentsCount = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PracticeFormat = table.Column<int>(type: "integer", nullable: false),
                    IsPaid = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorApplications", x => x.IdSupervisorApplication);
                });

            migrationBuilder.CreateTable(
                name: "SupervisorReviews",
                columns: table => new
                {
                    IdEmployee = table.Column<Guid>(type: "uuid", nullable: false),
                    IdStudentApplication = table.Column<Guid>(type: "uuid", nullable: false),
                    RecommendedForEmployment = table.Column<bool>(type: "boolean", nullable: false),
                    PvScore = table.Column<int>(type: "integer", nullable: false),
                    SkillsScore = table.Column<int>(type: "integer", nullable: false),
                    IndependenceScore = table.Column<int>(type: "integer", nullable: false),
                    TeamworkScore = table.Column<int>(type: "integer", nullable: false),
                    OverallScore = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisorReviews", x => new { x.IdEmployee, x.IdStudentApplication });
                });

            migrationBuilder.CreateTable(
                name: "TimeIntervals",
                columns: table => new
                {
                    IdInterval = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEmployee = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCreator = table.Column<Guid>(type: "uuid", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MaxCount = table.Column<int>(type: "integer", nullable: false),
                    BreakDuration = table.Column<TimeSpan>(type: "interval", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeIntervals", x => x.IdInterval);
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
                    IdEmployee = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Patronymic = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PersonnelNumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Position = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IdDepartment = table.Column<int>(type: "integer", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
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
                name: "StudentSupervisorApplications",
                columns: table => new
                {
                    IdSupervisorApplication = table.Column<Guid>(type: "uuid", nullable: false),
                    IdStudentApplication = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSupervisorApplications", x => new { x.IdSupervisorApplication, x.IdStudentApplication });
                    table.ForeignKey(
                        name: "FK_StudentSupervisorApplications_SupervisorApplications_IdSupe~",
                        column: x => x.IdSupervisorApplication,
                        principalTable: "SupervisorApplications",
                        principalColumn: "IdSupervisorApplication",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterviewSlots",
                columns: table => new
                {
                    IdInterviewSlot = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdEmployee = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCreator = table.Column<Guid>(type: "uuid", nullable: true),
                    IdInterval = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    MeetingPlace = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewSlots", x => x.IdInterviewSlot);
                    table.ForeignKey(
                        name: "FK_InterviewSlots_TimeIntervals_IdInterval",
                        column: x => x.IdInterval,
                        principalTable: "TimeIntervals",
                        principalColumn: "IdInterval",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentDocuments",
                columns: table => new
                {
                    IdDocument = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdStudentApplication = table.Column<int>(type: "integer", nullable: false),
                    StudentApplication = table.Column<int>(type: "integer", nullable: false),
                    IdDocumentType = table.Column<int>(type: "integer", nullable: false),
                    IdSpecialization = table.Column<int>(type: "integer", nullable: false),
                    isLoaded = table.Column<bool>(type: "boolean", nullable: false),
                    DocumentCheckStatus = table.Column<int>(type: "integer", nullable: false),
                    LoadDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PathToFile = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    SigningDocumentStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentDocuments", x => x.IdDocument);
                    table.ForeignKey(
                        name: "FK_StudentDocuments_DocumentsForSpecialization_IdDocumentType_~",
                        columns: x => new { x.IdDocumentType, x.IdSpecialization },
                        principalTable: "DocumentsForSpecialization",
                        principalColumns: new[] { "IdDocumentType", "IdSpecialization" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentDocuments_StudentApplications_StudentApplication",
                        column: x => x.StudentApplication,
                        principalTable: "StudentApplications",
                        principalColumn: "IdStudentApplication",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    IdInterviewSlot = table.Column<int>(type: "integer", nullable: false),
                    InterviewType = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<bool>(type: "boolean", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.IdInterviewSlot);
                    table.ForeignKey(
                        name: "FK_Interviews_InterviewSlots_IdInterviewSlot",
                        column: x => x.IdInterviewSlot,
                        principalTable: "InterviewSlots",
                        principalColumn: "IdInterviewSlot",
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
                name: "IX_InterviewSlots_IdInterval",
                table: "InterviewSlots",
                column: "IdInterval");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSlots_StartTime",
                table: "InterviewSlots",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSlots_Status",
                table: "InterviewSlots",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledPractices_IdPracticeType",
                table: "ScheduledPractices",
                column: "IdPracticeType");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledPractices_IdSpecialization",
                table: "ScheduledPractices",
                column: "IdSpecialization");

            migrationBuilder.CreateIndex(
                name: "IX_StudentApplications_IdPracticeType",
                table: "StudentApplications",
                column: "IdPracticeType");

            migrationBuilder.CreateIndex(
                name: "IX_StudentApplications_IdScheduledPractice",
                table: "StudentApplications",
                column: "IdScheduledPractice");

            migrationBuilder.CreateIndex(
                name: "IX_StudentApplications_IdSpecialization",
                table: "StudentApplications",
                column: "IdSpecialization");

            migrationBuilder.CreateIndex(
                name: "IX_StudentDocuments_IdDocumentType_IdSpecialization",
                table: "StudentDocuments",
                columns: new[] { "IdDocumentType", "IdSpecialization" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentDocuments_StudentApplication",
                table: "StudentDocuments",
                column: "StudentApplication");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorApplications_IdEmployee",
                table: "SupervisorApplications",
                column: "IdEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisorApplications_Status",
                table: "SupervisorApplications",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "ScheduledPractices");

            migrationBuilder.DropTable(
                name: "StudentDocuments");

            migrationBuilder.DropTable(
                name: "StudentSupervisorApplications");

            migrationBuilder.DropTable(
                name: "SupervisorReviews");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "InterviewSlots");

            migrationBuilder.DropTable(
                name: "PracticeTypes");

            migrationBuilder.DropTable(
                name: "DocumentsForSpecialization");

            migrationBuilder.DropTable(
                name: "StudentApplications");

            migrationBuilder.DropTable(
                name: "SupervisorApplications");

            migrationBuilder.DropTable(
                name: "TimeIntervals");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
