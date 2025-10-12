using Microsoft.Extensions.DependencyInjection;
using Presentation.Fitlers;

namespace Presentation
{
    public static class PresentationDependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<LogActionFilter>();
                })
                .AddApplicationPart(typeof(PresentationDependencyInjection).Assembly);
            return services;
        }
    }
}