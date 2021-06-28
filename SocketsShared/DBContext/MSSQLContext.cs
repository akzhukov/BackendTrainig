using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketsShared.DBContext
{
    public class MSSQLContext : IdentityDbContext<User>
    {
        public MSSQLContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "Admin".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2", Name = "Manager", NormalizedName = "Manager".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "3", Name = "User", NormalizedName = "User".ToUpper() });
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            var admin = new User
            {
                Id = "1",
                UserName = "admin"
            };
            var user = new User
            {
                Id = "2",
                UserName = "nik"
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "pwd123");
            user.PasswordHash = passwordHasher.HashPassword(user, "pwd123");

            modelBuilder.Entity<User>().HasData(admin, user);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = "1", UserId = "1" },
                new IdentityUserRole<string> { RoleId = "3", UserId = "2" }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
