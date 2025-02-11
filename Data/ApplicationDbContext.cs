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
