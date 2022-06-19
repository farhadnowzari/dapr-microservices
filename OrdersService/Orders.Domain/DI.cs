using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Orders.Domain;

public static class DI
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string cnnstring)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(cnnstring);
        });
        return services;
    }
}