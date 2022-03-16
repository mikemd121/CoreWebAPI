using CoreWebAPI.Business;
using CoreWebAPI.Core;
using CoreWebAPI.Data;
using CoreWebAPI.ServicesExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CoreWebAPI
{
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
                });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CoreWebAPI", Version = "v1" });
            });

            services.AddControllers();
            services.AddDbContext<CoreAPIDbContext>();
            services.AddAutoMapper(typeof(PopulationProfile));
            services.AddMyLibraryServices();
            services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new CustomBinderProvider());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                    var context = serviceScope.ServiceProvider.GetRequiredService<CoreAPIDbContext>();
                    context.Database.EnsureDeleted();
                    context.Database.Migrate();
                    context.Database.EnsureCreated();
                    RelationalDatabaseCreator databaseCreator =
                     (RelationalDatabaseCreator)context.Database.GetService<IDatabaseCreator>();
                    databaseCreator.CreateTables();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreWebAPI v1"));
            }
            app.ConfigureCustomExceptionMiddleware();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("EnableCORS");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
