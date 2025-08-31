using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductService.BLL.Mappers;
using ProductService.BLL.ServiceContracts;
using ProductService.BLL.Validators;
namespace ProductService.BLL;

public static class DependencyInjection
{
  public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
  {
        //TO DO: Add Data Access Layer services into the IoC container
        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        services.AddScoped<IProductsService, ProductService.BLL.Services.ProductsService>();
        return services;
  }
}
