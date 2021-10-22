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
using ShopBridgeBLL.Services;
using ShopBridgeBLL.Services.EFRepoContracts;
using ShopBridgeDAL.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClientServiceApp.Infrastructure.Filters;

namespace ShopBridgeWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ShopDbContext>(options =>
                                                 {
                                                  options.UseSqlServer(Configuration["ConnectionStrings:ShopBridgeDbConn"],
                                                                       x => x.MigrationsAssembly(@"ShopBridgeDAL"));
                                                  options.EnableSensitiveDataLogging();
                                                 });

            services.AddTransient<IProductCatalogService, ProductCatalogService>();
            services.AddTransient<IProductCatalogRepository, ProductCatalogRepository>();
      
            services.AddAutoMapper(typeof(MappingProfile));

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilters));
                options.Filters.Add(typeof(ActionGlobalLogFilter));
            });

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ShopBridge Product Catalog API",
                    Description = "ShopBridge Product Catalog Web API service"
                });
            });
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                SeedData.InitializeDb(app.ApplicationServices);

                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "ShopBridge Product Catalog API"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
