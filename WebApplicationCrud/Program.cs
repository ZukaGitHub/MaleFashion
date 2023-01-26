using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Models;
using WebApplicationCrud.Models.Identity;

namespace WebApplicationCrud
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();


            try
            {
                var scope = host.Services.CreateScope();
                var ctx = scope.ServiceProvider.GetRequiredService<CRUDdbcontext>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();




                var adminRole = new IdentityRole("Admin");
                if (!ctx.Roles.Any())
                {
                    roleMgr.CreateAsync(adminRole).GetAwaiter().GetResult();

                }
                if (!ctx.Users.Any(u => u.UserName == "admin"))
                {
                    var adminUser = new ApplicationUser
                    {
                        UserName = "admin",
                        Email = "admin@test.com",


                    };


                    userMgr.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                    userMgr.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            host.Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).ConfigureKestrel(webBuilder =>
            {
                webBuilder.AddServerHeader = false;
            })
       
                .UseStartup<Startup>();
    }
}
