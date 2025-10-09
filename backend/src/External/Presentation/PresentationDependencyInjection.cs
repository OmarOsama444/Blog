using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public static class PresentationDependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddApplicationPart(typeof(PresentationDependencyInjection).Assembly);
            return services;
        }
    }
}