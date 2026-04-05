using InternshipManager.Api.Models.Supervisor;
using ManagerService.Enums;
using ManagerService.Models.manager;
using ManagerService.Models.shared;
using ManagerService.Models.Supervisor;
using Microsoft.EntityFrameworkCore;

namespace ManagerService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // from manager
        public DbSet<DocumentForSpecialization> DocumentsForSpecialization { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<StudentApplication> StudentApplications { get; set; }
        public DbSet<StudentDocument> StudentDocuments { get; set; }

        // from supervisor
        public DbSet<SupervisorApplication> SupervisorApplications { get; set; }
        public DbSet<StudentSupervisorApplication> StudentSupervisorApplications { get; set; }
        public DbSet<TimeInterval> TimeIntervals { get; set; }
        public DbSet<InterviewSlot> InterviewSlots { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<SupervisorReview> SupervisorReviews { get; set; }

        // from shared
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PracticeType> PracticeTypes { get; set; }
        public DbSet<ScheduledPractice> ScheduledPractices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //from manager
            // Настройка составного внешнего ключа для StudentDocument
            modelBuilder.Entity<StudentDocument>(entity =>
            {
                entity.HasOne(sd => sd.DocumentForSpecialization)
                      .WithMany(dfs => dfs.StudentDocument)
                      .HasForeignKey(sd => new { sd.IdDocumentType, sd.IdSpecialization });
            });

            // supervisor
            // StudentSupervisorApplication - составной ключ уже задан атрибутом [PrimaryKey]
            // SupervisorReview - составной ключ уже задан атрибутом [PrimaryKey]

            // Interview - 1:1 связь с InterviewSlot
            modelBuilder.Entity<Interview>()
                .HasOne(i => i.InterviewSlot)
                .WithOne(islot => islot.Interview)
                .HasForeignKey<Interview>(i => i.IdInterviewSlot)
                .OnDelete(DeleteBehavior.Cascade);

            // InterviewSlot -> TimeInterval
            modelBuilder.Entity<InterviewSlot>()
                .HasOne(islot => islot.TimeInterval)
                .WithMany(ti => ti.InterviewSlots)
                .HasForeignKey(islot => islot.IdInterval)
                .OnDelete(DeleteBehavior.Restrict);

            // Индексы для поиска
            modelBuilder.Entity<SupervisorApplication>()
                .HasIndex(sa => sa.Status);

            modelBuilder.Entity<SupervisorApplication>()
                .HasIndex(sa => sa.IdEmployee);

            modelBuilder.Entity<InterviewSlot>()
                .HasIndex(islot => islot.Status);

            modelBuilder.Entity<InterviewSlot>()
                .HasIndex(islot => islot.StartTime);

            // from shared
            // Уникальные индексы
            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.Login)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.PersonnelNumber)
                .IsUnique();

            // Связи
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Department)
                .WithMany(d => d.Addresses)
                .HasForeignKey(a => a.IdDepartment)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.IdDepartment)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ScheduledPractice>()
                .HasOne(sp => sp.Specialization)
                .WithMany(s => s.ScheduledPractices)
                .HasForeignKey(sp => sp.IdSpecialization)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ScheduledPractice>()
                .HasOne(sp => sp.PracticeType)
                .WithMany(pt => pt.ScheduledPractices)
                .HasForeignKey(sp => sp.IdPracticeType)
                .OnDelete(DeleteBehavior.Restrict);

            // добавление Enum-ов
            modelBuilder.HasPostgresEnum<DocumentCheckStatus>();
            modelBuilder.HasPostgresEnum<EmployeeRole>();
            modelBuilder.HasPostgresEnum<InterviewSlotStatus>();
            modelBuilder.HasPostgresEnum<InterviewType>();
            modelBuilder.HasPostgresEnum<PracticeFormat>();
            modelBuilder.HasPostgresEnum<SigningDocumentStatus>();
            modelBuilder.HasPostgresEnum<StudentApplicationStatus>();
            modelBuilder.HasPostgresEnum<SupervisorApplicationStatus>();
        }
    }
}