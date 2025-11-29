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
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<VacationRequest> VacationRequests { get; set; }
        public DbSet<ExtraBenefitDay> ExtraBenefitDays { get; set; }
        public DbSet<EmployeeExtraBenefitDay> EmployeeExtraBenefitDays { get; set; }
        public DbSet<Holiday> Holidays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            modelBuilder.Entity<Role>()
                .Property(r => r.Position)
                .IsRequired();

            modelBuilder.Entity<Permission>()
                .Property(p => p.Key)
                .IsRequired();

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.VacationRequests)
                .WithOne(v => v.Employee)
                .HasForeignKey(v => v.EmployeeId);

            // Un empleado no debe tener dos asignaciones del mismo beneficio en el mismo año:
            modelBuilder.Entity<EmployeeExtraBenefitDay>()
                .HasIndex(x => new { x.EmployeeId, x.ExtraBenefitDayId, x.Year })
                .IsUnique();


            base.OnModelCreating(modelBuilder);
        }
    }
}
