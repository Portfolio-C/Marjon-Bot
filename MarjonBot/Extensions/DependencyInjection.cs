using MarjonBot.Application.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace MarjonBot.Extensions;
internal static class DependencyInjection
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddApplication();

        return services;
    }
}
