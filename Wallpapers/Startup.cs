using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Wallpapers.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wallpapers.Models;
using Wallpapers.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace Wallpapers
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.ConfigureApplicationCookie(options => options.LoginPath = "/login");

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "tag",
                    pattern: "tag/{id}",
                    defaults: new {controller = "Tag", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "tags",
                    pattern: "tags",
                    defaults: new { controller = "Tag", action = "Tags" }
                );

                endpoints.MapControllerRoute(
                    name: "post",
                    pattern: "w/{id}",
                    defaults: new { controller = "Post", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "latest",
                    pattern: "latest",
                    defaults: new { controller = "List", action = "Index"}
                );

                endpoints.MapControllerRoute(
                    name: "random",
                    pattern: "random",
                    defaults: new { controller = "List", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "top",
                    pattern: "top",
                    defaults: new { controller = "List", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "logout",
                    pattern: "logout",
                    defaults: new { controller = "Login", action = "Logout" }
                );

                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "login",
                    defaults: new { controller = "Login", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "register",
                    pattern: "register",
                    defaults: new { controller = "Register", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "favorites",
                    pattern: "favorites",
                    defaults: new { controller = "List", action = "Favorites" }
                );

                endpoints.MapControllerRoute(
                    name: "addFavorites",
                    pattern: "favorites/add",
                    defaults: new { controller = "List", action = "ToggleFavorite" }
                );

                endpoints.MapControllerRoute(
                    name: "upload",
                    pattern: "upload",
                    defaults: new { controller = "Upload", action = "Index" }
                );

                endpoints.MapControllerRoute(
                    name: "delete",
                    pattern: "delete",
                    defaults: new { controller = "Delete", action = "Index" }
                );

                endpoints.MapRazorPages();  
            });
        }
    }
}
