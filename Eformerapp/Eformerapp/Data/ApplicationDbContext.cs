using Eformerapp.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eformerapp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ProductMaster> Products { get; set; }
        public DbSet<UnitMaster> Units { get; set; }
        public DbSet<CategoryMaster> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(u => u.UserRole)
                      .WithMany()
                      .HasForeignKey(u => u.UserRoleId);
            });

            modelBuilder.Entity<ProductMaster>(entity =>
            {
                entity.HasOne(p => p.Unit)
                      .WithMany()
                      .HasForeignKey(p => p.UnitId);

                entity.HasOne(p => p.Category)
                      .WithMany()
                      .HasForeignKey(p => p.CategoryId);
            });
        }
    }
}