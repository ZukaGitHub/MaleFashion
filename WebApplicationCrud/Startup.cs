using WebApplicationCrud.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using WebApplicationCrud.Data.DbContext;
using WebApplicationCrud.Data.FileManager;

using WebApplicationCrud.Models;
using AutoMapper;
using WebApplicationCrud.Models.Identity;
using Stripe;
using WebApplicationCrud.Models.PaymentI;

namespace WebApplicationCrud
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: MyAllowSpecificOrigins,
            //                      policy =>
            //                      {
            //                          policy.WithOrigins("https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@300;400;600;700;800;900&display=swap",
            //                                              "https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js",
            //                                              "https://unpkg.com/axios/dist/axios.min.js",
            //                                              "https://cdn.jsdelivr.net/npm/vue@2/dist/vue.js");
            //                      });
            //});
            services.AddControllersWithViews();
            services.AddDbContext<CRUDdbcontext>(options => options.UseSqlServer(Configuration.GetConnectionString("Production")));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShoppingCart.GetCart(sp));
          
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                //options.SignIn.RequireConfirmedEmail = true;
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            }
            )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CRUDdbcontext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddGoogle(options => {
                options.ClientId = "728209250486-iqv794df2o311grvum0ls14pg451pg9k.apps.googleusercontent.com";
                options.ClientSecret = "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";


            }).AddFacebook(options =>
            {
                options.AppId = "487829986500552";
                options.AppSecret = "688e5572747e0c333034189752fe0846";
            });
            services.AddRazorPages().AddRazorRuntimeCompilation();

            StripeConfiguration.ApiKey = "sk_test_51MC5xSE9HwHTu6RuZ6A25TMg8Nb3nG5VrYUc7uojjIMVrJtOEMtayHZ4ubQEEecmHauTNzdZwFeM3okyIR84trnb00SUSwBGfq";
            services.AddAutoMapper();
            services.AddTransient<IFileManager, FileManager>();
            services.AddTransient<IRepository, Repository>();
            services.AddMemoryCache();
            services.AddSession(options=> {
                options.Cookie.Name = "Cart";
                options.Cookie.MaxAge = TimeSpan.FromMinutes(20);
            
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
         
            app.UseHttpsRedirection();
          
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();
            //app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                
            });

        
        }
    }
}
