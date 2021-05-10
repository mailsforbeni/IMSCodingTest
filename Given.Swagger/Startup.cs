using System.Text;
using Given.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Given.Repositories.Generic;
using Given.Repositories;
using Given.Repositories.Mapping;
using AutoMapper; 
using NLog;
using System.IO;
using Given.DataContext.IMSEntities;

namespace Given.Swagger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(System.String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            //.AllowCredentials();
            #endregion CORS

            #region Database Connections
            services.AddDbContext<InventoryManagementSystemContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            #endregion

            #region AutoMapper
            services.AddAutoMapper(typeof(Startup), typeof(AutoMapperProfile));

            #endregion

            #region Controllers
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Given API",
                    Description = "Given API",
                });
                 
            });
            #endregion
             
                                                               
            #region Repositories                                      
            services.AddScoped<Repositories.Generic.IInventoryRepository, InventoryRepository>();             
            services.AddSingleton<ILog, LogNLog>();
            #endregion

            #region API versioning
            // services.AddApiVersioning(o => o.ApiVersionReader = new HeaderApiVersionReader("api-version")); TODO: use when will be added support for .NET Core 3.0
            #endregion
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");
 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Given API V1");
            });
        }
    }
}
