using Microsoft.Extensions.DependencyInjection;

namespace ProductService.BLL;

public static class DependencyInjection
{
  public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
  {
    //TO DO: Add Data Access Layer services into the IoC container

    return services;
  }
}
