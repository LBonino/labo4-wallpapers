using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wallpapers.Data;
using Wallpapers.Models;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;

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
                NormalizedEmail = "LISANDROBONINO4@PROTONMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var catLover = new ApplicationUser
            {
                UserName = "catLover",
                NormalizedUserName = "CATLOVER",
                Email = "wallpapers.catlover@protonmail.com",
                NormalizedEmail = "WALLPAPERS.CATLOVER@PROTONMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var doggoFan = new ApplicationUser
            {
                UserName = "doggofan",
                NormalizedUserName = "DOGGOFAN",
                Email = "wallpapers.doggofan@protonmail.com",
                NormalizedEmail = "WALLPAPERS.DOGGOFAN@PROTONMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var birbEnthusiast = new ApplicationUser
            {
                UserName = "birb_enthusiast",
                NormalizedUserName = "BIRB_ENTHUSIAST",
                Email = "wallpapers.birbenthusiast@protonmail.COM",
                NormalizedEmail = "WALLPAPERS.BIRBENTHUSIAST@PROTONMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var aenami = new ApplicationUser
            {
                UserName = "aenami",
                NormalizedUserName = "AENAMI",
                Email = "aenami@art.com",
                NormalizedEmail = "AENAMI@ART.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var uploaders = new List<ApplicationUser> { doggoFan, catLover, birbEnthusiast, aenami, admin };
            foreach (var user in uploaders)
            {
                if (!userManager.Users.Any(r => r.UserName == user.UserName))
                {
                    var result = await userManager.CreateAsync(user, "mala-contraseña-123");
                    await userManager.AddToRoleAsync(user, "Uploader");
                }
            }

            await userManager.AddToRoleAsync(admin, "Admin");

            if (!userManager.Users.Any(u => u.NormalizedUserName.Contains("NORMIE")))
            {
                var normies = new List<ApplicationUser>();
                for (int i = 0; i < 100; i++)
                {
                    normies.Add(new ApplicationUser
                    {
                        UserName = $"normie{i}",
                        NormalizedUserName = $"NORMIE{i}",
                        Email = $"normie{i}@normie.com",
                        NormalizedEmail = $"NORMIE{i}@NORMIE.COM",
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    });
                }

                foreach (var user in normies)
                {
                    if (!userManager.Users.Any(r => r.UserName == user.UserName))
                    {
                        var result = await userManager.CreateAsync(user, "mala-contraseña-123");
                        await userManager.AddToRoleAsync(user, "Standard");
                    }
                }
            }
            
            var birdTag = new Tag
            {
                Name = "Birds",
                AdditionDate = DateTime.Now.AddDays(-15),
                UserId = userManager.Users
                        .Where(user => user.UserName == "birb_enthusiast")
                        .First().Id
            };

            var dogTag = new Tag
            {
                Name = "Dogs",
                AdditionDate = DateTime.Now.AddDays(-14),
                UserId = userManager.Users
                    .Where(user => user.UserName == "doggofan")
                    .First().Id
            };

            var catTag = new Tag
            {
                Name = "Cats",
                AdditionDate = DateTime.Now.AddDays(-13),
                UserId = userManager.Users
                    .Where(user => user.UserName == "catLover")
                    .First().Id
            };

            var digitalArtTag = new Tag
            {
                Name = "Digital Art",
                AdditionDate = DateTime.Now.AddDays(-13),
                UserId = userManager.Users
                    .Where(user => user.UserName == "aenami")
                    .First().Id
            };

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                var hostEnvironment = serviceProvider.GetService<IWebHostEnvironment>();

                if (!context.Tags.Any())
                {
                    context.Tags.AddRange(catTag, dogTag, birdTag, digitalArtTag);
                    await context.SaveChangesAsync();
                }


                if (!context.Images.Any())
                {
                    var wallpapersToSeedPath = Path.Combine(Directory.GetCurrentDirectory(), "WallpapersToSeed");

                    var aenamiFullSizeToSeedPath = Path.Combine(wallpapersToSeedPath, "Aenami\\fullsize");
                    var dogsFullSizeToSeedPath = Path.Combine(wallpapersToSeedPath, "Dogs\\fullsize");
                    var catsFullSizeToSeedPath = Path.Combine(wallpapersToSeedPath, "Cats\\fullsize");
                    var birdsFullSizeToSeedPath = Path.Combine(wallpapersToSeedPath, "Birds\\fullsize");

                    var doggoFanId = userManager.Users
                        .Where(user => user.UserName == "doggoFan")
                        .First().Id;

                    var catLoverId = userManager.Users
                        .Where(user => user.UserName == "catLover")
                        .First().Id;

                    var birbEnthusiastId = userManager.Users
                        .Where(user => user.UserName == "birb_enthusiast")
                        .First().Id;

                    var aenamiId = userManager.Users
                        .Where(user => user.UserName == "aenami")
                        .First().Id;


                    var dogsTag = context.Tags
                        .Where(tag => tag.Name == "Dogs")
                        .First();

                    var catsTag = context.Tags
                        .Where(tag => tag.Name == "Cats")
                        .First();

                    var birdsTag = context.Tags
                        .Where(tag => tag.Name == "Birds")
                        .First();

                    var digitalArtsTag = context.Tags
                        .Where(tag => tag.Name == "Digital Art")
                        .First();

                    var folderToSaveWallpaperPath = Path.Combine(
                        hostEnvironment.WebRootPath,
                        "wallpapers"
                    );

                    await SeedWallpaperBatch(
                        dogsFullSizeToSeedPath,
                        folderToSaveWallpaperPath,
                        doggoFanId,
                        dogsTag,
                        context
                    );

                    await SeedWallpaperBatch(
                        catsFullSizeToSeedPath,
                        folderToSaveWallpaperPath,
                        catLoverId,
                        catsTag,
                        context
                    );

                    await SeedWallpaperBatch(
                        birdsFullSizeToSeedPath,
                        folderToSaveWallpaperPath,
                        birbEnthusiastId,
                        birdsTag,
                        context
                    );

                    await SeedWallpaperBatch(
                        aenamiFullSizeToSeedPath,
                        folderToSaveWallpaperPath,
                        aenamiId,
                        digitalArtsTag,
                        context
                    );
                }

                var normie = userManager.Users.Where(user => user.UserName == "normie1").FirstOrDefault();
                
                if (
                    normie != null
                    && !context.Favorite.Any(f => f.UserId == normie.Id)
                )
                {
                    // Hacer que usuarios seedeados agreguen a favs algunos posts al azar
                    var favorites = new List<Favorite>();
                    var firstPostId = context.Posts.First().PostId;
                    var upperBoundPostId = firstPostId + context.Posts.Count();
                    var normies = context.Users
                        .Where(user => user.NormalizedUserName.Contains("NORMIE"))
                        .ToList();

                    foreach (var user in normies)
                    {
                        for (int i = 0; i < new Random().Next(1, 10); i++)
                        {
                            favorites.Add(new Favorite
                            {
                                UserId = user.Id,
                                PostId = new Random().Next(firstPostId, upperBoundPostId)
                            });
                        }
                    }

                    context.Favorite.AddRange(favorites);
                    context.SaveChanges();
                }
            }
        }

        // "Postea" wallpapers a nombre de un usuario y tag ya registrados
        // Asume que el usuario y tag ya están en la base de datos, así que puede fallar dijo Tusam
        private static async Task SeedWallpaperBatch(
            string fullSizeToSeedPath,
            string folderToSaveWallpaperPath,
            string userId,
            Tag tag,
            ApplicationDbContext context
        )
        {
            if (
                context.Posts
                    .Where(post => post.UserId == userId)
                    .Any()
            )
            {
                return;
            }

            var fullSizePath = Path.Combine(folderToSaveWallpaperPath, "fullsize");
            var thumbnailPath = Path.Combine(folderToSaveWallpaperPath, "thumbnail");

            Directory.CreateDirectory(fullSizePath);
            Directory.CreateDirectory(thumbnailPath);

            var fullSizeFiles = Directory.GetFiles(fullSizeToSeedPath);

            var posts = new List<Post>();

            foreach (var file in fullSizeFiles)
            {
                posts.Add(new Post
                {
                    SubmissionDate = tag.AdditionDate,
                    SubmissionStatus = SubmissionStatus.Approved,
                    UserId = userId
                });
            }

            // Se guardan de esta manera porque tienen una relación 1:1
            // en la que una Image es la entidad dependiente
            context.Posts.AddRange(posts);
            await context.SaveChangesAsync();
            
            var postTags = new List<PostTag>();
            var favorites = new List<Favorite>();
            var images = new List<Image>();
            var currentPostId = context.Posts
                .ToList()
                .ElementAt(context.Posts.Count() - fullSizeFiles.Length)
                .PostId;

            foreach (var filePath in fullSizeFiles)
            {
                var wallpaperImage = System.Drawing.Image.FromFile(filePath);

                var fileInfo = new FileInfo(filePath);
                string extension = fileInfo.Extension.ToString();

                var image = new Image
                {
                    Width = wallpaperImage.Width,
                    Height = wallpaperImage.Height,
                    Extension = extension,
                    SizeInBytes = fileInfo.Length,
                    Filename = $"{currentPostId}{extension}",
                    PostId = currentPostId
                };

                images.Add(image);

                File.Copy(
                    fileInfo.FullName,
                    Path.Combine(new string[] { folderToSaveWallpaperPath, "fullsize", image.Filename }),
                    overwrite: true
                );

                File.Copy(
                    Path.Combine(new string[] { fileInfo.Directory.Parent.FullName, "thumbnail", fileInfo.Name }),
                    Path.Combine(new string[] { folderToSaveWallpaperPath, "thumbnail", image.Filename }),
                    overwrite: true
                );

                postTags.Add(new PostTag
                {
                    TagId = tag.TagId,
                    PostId = currentPostId
                });

                favorites.Add(new Favorite
                {
                    UserId = userId,
                    PostId = currentPostId
                });

                currentPostId++;
            }

            context.Images.AddRange(images);
            context.PostTags.AddRange(postTags);
            context.Favorite.AddRange(favorites);
            await context.SaveChangesAsync();
        }
    }
}