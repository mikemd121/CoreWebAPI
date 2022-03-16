using CoreWebAPI.Business;
using CoreWebAPI.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CoreWebAPI.ServicesExtensions
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Adds my library services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddMyLibraryServices(this IServiceCollection services)
        {
            services.AddScoped<IPopulationDataService, PopulationDataService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
