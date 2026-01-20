using FiapCloudGames.Notifications.Application.Services.v1;
using FiapCloudGames.Notifications.Domain.Services.v1;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Notifications.Application;

[ExcludeFromCodeCoverage]
public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}