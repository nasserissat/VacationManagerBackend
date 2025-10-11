using Microsoft.EntityFrameworkCore;
using vacation_backend.Domain.Entities;

namespace vacation_backend.Infrastructure
{
    public class VacationDbContext : DbContext
    {
        public VacationDbContext(DbContextOptions<VacationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<ExtraBenefitDay> ExtraBenefitDays { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
                .HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity(j => j.ToTable("RolePermissions"));

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.VacationRequests)
                .WithOne(v => v.Employee)
                .HasForeignKey(v => v.EmployeeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
