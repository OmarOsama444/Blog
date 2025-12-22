using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Fitlers;

namespace Presentation
{
    public static class PresentationDependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddApplicationPart(typeof(PresentationDependencyInjection).Assembly);
            services.AddHttpLogging(options =>
            {
                options.LoggingFields = HttpLoggingFields.Request | HttpLoggingFields.Response;
            });

            return services;
        }
    }
}