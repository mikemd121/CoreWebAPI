using CoreWebAPI.Business;
using Microsoft.AspNetCore.Builder;

namespace CoreWebAPI.ServicesExtensions
{
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Configures the custom exception middleware.
        /// </summary>
        /// <param name="app">The application.</param>
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
