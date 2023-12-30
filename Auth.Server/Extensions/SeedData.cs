using Auth.Server.Data.Entities.Custom;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Server.Extensions
{
    public static class SeedData
    {
        public static void SeedUserData(ModelBuilder modelBuilder)
        {
            AspnetUser user = new()
            {
                Id = 1,
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "myemail@mymail.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            PasswordHasher<AspnetUser> passwordHasher = new();
            user.PasswordHash = passwordHasher.HashPassword(user, "123456");
            modelBuilder.Entity<AspnetUser>().HasData(user);
        }
    }
}
