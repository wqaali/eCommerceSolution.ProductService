using FluentValidation.AspNetCore;
using ProductMicroService.API.Middleware;
using ProductService.DAL;  // for AddDataAccessLayer
using ProductService.BLL;
using ProductsMicroService.API.APIEndpoints;  // for AddBusinessLogicLayer

namespace ProductMicroService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Add DAL and BLL services
            builder.Services.AddDataAccessLayer(builder.Configuration);
            builder.Services.AddBusinessLogicLayer();

            builder.Services.AddControllers();

            //FluentValidations
            builder.Services.AddFluentValidationAutoValidation();
            var app = builder.Build();
          
            app.UseExceptionHandlingMiddleware();
            app.UseRouting();

            //Auth
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapProductAPIEndpoints();
            app.Run();
        }
    }
}
