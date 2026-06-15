using Microsoft.EntityFrameworkCore;
using PersonalFinance.API.Models;

namespace PersonalFinance.API.Data
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options) { }

            public DbSet<User> Users { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Transaction> Transactions { get; set; }
            public DbSet<Budget> Budgets { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Table names 
                modelBuilder.Entity<User>().ToTable("Users");
                modelBuilder.Entity<Category>().ToTable("Categories");
                modelBuilder.Entity<Transaction>().ToTable("Transactions");
                modelBuilder.Entity<Budget>().ToTable("Budgets");

                // Unique Email
                modelBuilder.Entity<User>()
                    .HasIndex(u => u.Email)
                    .IsUnique();

                // Transaction rules
                modelBuilder.Entity<Transaction>().HasCheckConstraint("CK_Transaction_Amount", "[Amount] > 0");

            //CategoryType
            modelBuilder.Entity<Category>().HasCheckConstraint("CK_Category_Type", "[Type] IN (0,1)");

                modelBuilder.Entity<Transaction>()
                    .HasOne(t => t.User)
                    .WithMany(u => u.Transactions)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Transaction>()
                    .HasOne(t => t.Category)
                    .WithMany(c => c.Transactions)
                    .HasForeignKey(t => t.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Category → User
                modelBuilder.Entity<Category>()
                    .HasOne(c => c.User)
                    .WithMany(u => u.Categories)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Budget rules
                modelBuilder.Entity<Budget>()
                    .HasIndex(b => new { b.UserId, b.CategoryId, b.Month })
                    .IsUnique();

                modelBuilder.Entity<Budget>()
                    .HasOne(b => b.User)
                    .WithMany(u => u.Budgets)
                    .HasForeignKey(b => b.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Budget>()
                    .HasOne(b => b.Category)
                    .WithMany(c => c.Budgets)
                    .HasForeignKey(b => b.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }



    
}
