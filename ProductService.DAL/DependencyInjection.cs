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
        string connectionStringValues = connectionStringTemplate.Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST")).
        Replace("$MYSQL_DATABASE", Environment.GetEnvironmentVariable("MYSQL_DATABASE")).
        Replace("$MYSQL_USER", Environment.GetEnvironmentVariable("MYSQL_USER")).
        Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD")).
        Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT"));
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(connectionStringValues!, ServerVersion.AutoDetect(connectionStringValues!));
        });
        services.AddScoped<IProductsRepository, ProductsRepository>();
        return services;
  }
}
