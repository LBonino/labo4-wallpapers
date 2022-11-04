using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallpapers.Data;

namespace Wallpapers.Models
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService
                <RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "Uploader", "Standard" };

            foreach (var role in roles)
            {
                if (!(await roleManager.RoleExistsAsync(role)))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            using UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService
                <UserManager<ApplicationUser>>();

            var admin = new ApplicationUser
            {
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "lisandrobonino4@protonmail.com",
                NormalizedEmail = "LISANDROBONINO4@PROTONMAIL.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var catLover = new ApplicationUser
            {
                UserName = "catLover",
                NormalizedUserName = "CATLOVER",
                Email = "wallpapers.catlover@protonmail.com",
                NormalizedEmail = "WALLPAPERS.CATLOVER@PROTONMAIL.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var doggoFan = new ApplicationUser
            {
                UserName = "doggofan",
                NormalizedUserName = "DOGGOFAN",
                Email = "wallpapers.doggofan@protonmail.com",
                NormalizedEmail = "WALLPAPERS.DOGGOFAN@PROTONMAIL.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var birbEnthusiast = new ApplicationUser
            {
                UserName = "birb_enthusiast",
                NormalizedUserName = "BIRB_ENTHUSIAST",
                Email = "wallpapers.birbenthusiast@protonmail.com",
                NormalizedEmail = "WALLPAPERS.BIRBENTHUSIAST@PROTONMAIL.com",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            ApplicationUser[] userList = { doggoFan, catLover, birbEnthusiast, admin };

            foreach (var user in userList)
            {
                if (!userManager.Users.Any(r => r.UserName == user.UserName))
                {
                    var result = await userManager.CreateAsync(user, "mala-contraseña-123");
                }
            }
        }

        // PENDIENTE: Seedear imágenes, tags, posts, favoritos
    }
}