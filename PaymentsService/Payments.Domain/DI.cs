using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace Payments.Domain;

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