using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkiHouse.Web.Data;
using SkiHouse.Web.Data.Models;
using SkiHouse.Web.Data.Repositories;
using SkiHouse.Web.Data.Repositories.Interfaces;
using SkiHouse.Web.Services;

namespace SkiHouse.Web
{
    public class Startup
    {
        private readonly IConfigurationRoot _configurationRoot;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _configurationRoot = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", true)
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();

            services.AddDbContext<SkiHouseDBContext>(options =>
            {
                options.UseSqlServer(_configurationRoot.GetConnectionString("SkiHouse"));
            }, ServiceLifetime.Singleton);

            services.AddDbContext<AppIdentityDBContext>(options =>
            {
                options.UseSqlServer(_configurationRoot.GetConnectionString("SkiHouseIdentity"));
            }, ServiceLifetime.Singleton);
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDBContext>();

            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddScoped(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute("error", "Error", new { controller = "Error", action = "Error" });
                routes.MapRoute("category", "{category}/Page{page:int}", new { Controller = "Product", action = "List" });
                routes.MapRoute("pagination", "Page{page:int}", new { Controller = "Product", action = "List", page = 1 });
                routes.MapRoute(null, "{category}", new { Controller = "Product", action = "List", page = 1 });
                routes.MapRoute("default", "{controller=Product}/{action=List}/{id?}");
            });

            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
