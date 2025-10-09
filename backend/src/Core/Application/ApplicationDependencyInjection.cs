using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Application.Events;
using Application.Interfaces;
using Application.Services;
using Application.Abstractions;
using Domain.Abstractions;

namespace Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(AssemblyRefrence.Assembly);
        // Register the domain event dispatcher
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IAiService, AiService>();
        // Register domain event handlers
        services.Scan(scan => scan
            .FromAssemblies(AssemblyRefrence.Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
        return services;
    }
}
