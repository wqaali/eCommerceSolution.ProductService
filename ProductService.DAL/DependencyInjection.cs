using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProductService.DAL.context;
using ProductService.DAL.Repositories;
using ProductService.DAL.RepositoryContracts;

namespace ProductService.DAL;

public static class DependencyInjection
{
  public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
  {
        //TO DO: Add Data Access Layer services into the IoC container
        var connectionStringTemplate=configuration.GetConnectionString("DefaultConnection")!;
        string connectionStringValues = connectionStringTemplate.Replace("$HOST_NAME", Environment.GetEnvironmentVariable("HOST_NAME")).
        Replace("$DB_NAME", Environment.GetEnvironmentVariable("DB_NAME")).
        Replace("$DB_USER", Environment.GetEnvironmentVariable("DB_USER")).
        Replace("$DB_PASSWORD", Environment.GetEnvironmentVariable("DB_PASSWORD"));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionStringValues!);
        });
        services.AddScoped<IProductsRepository, ProductsRepository>();
        return services;
  }
}
