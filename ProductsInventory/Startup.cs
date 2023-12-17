using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProductsInventory.Data;
using ProductsInventory.Data.Models;
using ProductsInventory.Domain;
using ProductsInventory.Domain.Interfaces;
using ProductsInventory.Domain.IRepositories;
using ProductsInventory.Domain.Middlewares;
using ProductsInventory.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsInventory
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add DbContext
            services.AddDbContext<ProductsInventoryContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Default")));

            // Add Repositories
            services.AddScoped<IProductsRepository, ProductsRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();

            // Add Services
            services.AddScoped<ProductsService>();
            services.AddScoped<IAuditService, AuditDbService>();

            // Add AutoMapper configuration
            //services.AddAutoMapper(typeof(Startup));

            // Add Swagger configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Products Inventory", Version = "v1" });

                // Optional: If you have comments in your code that include API descriptions, you can include those in the Swagger UI
                //var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            // Firebase Realtime Database Configuration
            var firebaseUrl = Configuration["Firebase:DatabaseUrl"];
            var firebaseSecret = Configuration["Firebase:DatabaseSecret"];
            services.AddSingleton<IRealTimeService>(provider =>
        new RealTimeService(firebaseUrl, firebaseSecret));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // use exception handling middleware
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Enable Swagger and Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products Inventory V1");
                // Optionally, specify the Swagger UI route
                // c.RoutePrefix = "api-docs";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
