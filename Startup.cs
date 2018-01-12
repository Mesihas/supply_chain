using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EFDC.Services;
using EFDC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace EFDC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // add Identity services
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
           {
               config.User.RequireUniqueEmail = false;
               config.Password.RequireDigit = false;
               config.Password.RequiredLength = 4;
               config.Password.RequireLowercase = false;
               config.Password.RequireUppercase = false;
               config.Password.RequireNonAlphanumeric = false;
           }).AddEntityFrameworkStores<MyDbContext>();

            services.AddDbContext<MyDbContext>();

            // add My services
            services.AddScoped<IDataService<Category>, DataService<Category>>();
            services.AddScoped<IDataService<Product>, DataService<Product>>();
            services.AddScoped<IDataService<Order>, DataService<Order>>();
            services.AddScoped<IDataService<Category>, DataService<Category>>();
            services.AddScoped<IDataService<Branch>, DataService<Branch>>();
            services.AddScoped<IDataService<SubCategory>, DataService<SubCategory>>();
            services.AddScoped<IDataService<OrderDetail>, DataService<OrderDetail>>();
            services.AddScoped<IDataService<Profile>, DataService<Profile>>();
            services.AddScoped<IDataService<Puesto>, DataService<Puesto>>();
            services.AddScoped<IDataService<ProductAvalilability>, DataService<ProductAvalilability>>();
            services.AddScoped<IDataService<Mail>, DataService<Mail>>();


            // Configure session services
            // 1- add a default in-memory cache
            services.AddDistributedMemoryCache();
            // 2- add session with options
            services.AddSession();
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(10);
            //    options.CookieHttpOnly = true;
            //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseIdentity();
            app.UseMvcWithDefaultRoute();
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
