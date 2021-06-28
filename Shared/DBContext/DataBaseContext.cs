using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Shared.DBContext
{
    public class DataBaseContext : IdentityDbContext<User>
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "1", Name = "Admin" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2", Name = "Manager" });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "3", Name = "User" });
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            var admin = new User
            {
                Id = "1",
                UserName = "admin"
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "pwd123");
            modelBuilder.Entity<User>().HasData(admin);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = "1", UserId = "1" });
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Factory> Factory { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<Tank> Tank { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<UpdateModel> UpdateModel { get; set; }
    }

}
