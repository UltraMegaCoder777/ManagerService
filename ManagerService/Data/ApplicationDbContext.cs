using ManagerService.Enums;
using ManagerService.Models.manager;
using ManagerService.Models.shared;
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
        public DbSet<ManagerInterval> ManagerIntervals { get; set; }
        public DbSet<ManagerSlot> ManagerSlots { get; set; }
        public DbSet<ManagerInterview> ManagerInterviews { get; set; }

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

            // добавление Enum-ов
            modelBuilder.HasPostgresEnum<DocumentCheckStatus>();
            modelBuilder.HasPostgresEnum<EmployeeRole>();            
            modelBuilder.HasPostgresEnum<PracticeFormat>();
            modelBuilder.HasPostgresEnum<SigningDocumentStatus>();
            modelBuilder.HasPostgresEnum<ManagerSlotStatus>();
                        
            // from manager

            // Interview - 1:1 связь с InterviewSlot
            modelBuilder.Entity<ManagerInterview>()
                .HasOne(i => i.ManagerSlot)
                .WithOne(islot => islot.ManagerInterview)
                .HasForeignKey<ManagerInterview>(i => i.IdManagerSlot)
                .OnDelete(DeleteBehavior.Cascade);

            // InterviewSlot -> TimeInterval
            modelBuilder.Entity<ManagerSlot>()
                .HasOne(islot => islot.ManagerInterval)
                .WithMany(ti => ti.ManagerSlots)
                .HasForeignKey(islot => islot.IdManagerInterval)
                .OnDelete(DeleteBehavior.Restrict);

            // Индексы для поиска
            modelBuilder.Entity<ManagerSlot>()
                .HasIndex(islot => islot.Status);

            modelBuilder.Entity<ManagerSlot>()
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
        }
    }
}