using Microsoft.EntityFrameworkCore;
using gg_test.Models;

namespace gg_test.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmailHistory> EmailHistories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<KPI> KPIs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmailHistory>(entity =>
            {
                entity.Property(e => e.SentAt)
                    .HasColumnType("timestamp")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<User>().HasData(
                new User 
                { 
                    Id = 1, 
                    Name = "Admin User", 
                    Email = "admin@example.com", 
                    Department = "IT", 
                    Role = Role.Admin, 
                    Password = BCrypt.Net.BCrypt.HashPassword("admin123")
                },
                new User 
                { 
                    Id = 2, 
                    Name = "Manager User", 
                    Email = "manager@example.com", 
                    Department = "Sales", 
                    Role = Role.Manager, 
                    Password = BCrypt.Net.BCrypt.HashPassword("manager123")
                },
                new User 
                { 
                    Id = 3, 
                    Name = "Employee1", 
                    Email = "emp1@example.com", 
                    Department = "Sales", 
                    Role = Role.Employee, 
                    Password = BCrypt.Net.BCrypt.HashPassword("emp123")
                },
                new User 
                { 
                    Id = 4, 
                    Name = "Employee2", 
                    Email = "emp2@example.com", 
                    Department = "Marketing", 
                    Role = Role.Employee, 
                    Password = BCrypt.Net.BCrypt.HashPassword("emp123")
                }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "Company A" },
                new Company { Id = 2, Name = "Company B" }
            );

            modelBuilder.Entity<KPI>().HasData(
                new KPI { Id = 1, CompanyId = 1, MonthlyRevenue = 100000, NetProfit = 20000, ProfitMargin = 20 },
                new KPI { Id = 2, CompanyId = 1, MonthlyRevenue = 120000, NetProfit = 25000, ProfitMargin = 21 },
                new KPI { Id = 3, CompanyId = 2, MonthlyRevenue = 150000, NetProfit = 30000, ProfitMargin = 20 },
                new KPI { Id = 4, CompanyId = 2, MonthlyRevenue = 160000, NetProfit = 32000, ProfitMargin = 20 }
            );
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is EmailHistory && 
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (EmailHistory)entry.Entity;
                
                entity.SentAt = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
