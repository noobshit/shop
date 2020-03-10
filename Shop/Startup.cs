using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Data;
using Shop.Models;
using Shop.Services;

namespace Shop
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure youzr application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddIdentity<ShopUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ShopContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/SignIn";
                options.AccessDeniedPath = "/Error/403";
            });

            services.AddDbContextPool<ShopContext>(options =>
                {
                    options.UseLazyLoadingProxies().UseSqlServer(_config.GetConnectionString("ShopDB"));
                }
            );

            services.AddHttpContextAccessor();

            services.AddScoped<ICartManager, CartManager>();
            services.AddScoped<IImageManager, ImageManager>();
            services.AddTransient<ICartSummary, CartSummary>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) 
        {
            if( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            UpdateDatabase(app);
        }


        private static void UpdateDatabase(IApplicationBuilder app)
        {
            try
            {
                using( var serviceScope = app.ApplicationServices
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope() )
                {
                    var serviceProvider = serviceScope.ServiceProvider;
                    using( var context = serviceProvider.GetService<ShopContext>() )
                    {
                        context.Database.Migrate();
                    }
                    SeedData.Roles(serviceProvider.GetRequiredService<RoleManager<IdentityRole>>());
                }
            }
            catch
            {

            }
        }
    }
}
