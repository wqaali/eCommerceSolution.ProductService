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
            //Add Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyOrigin().AllowAnyHeader();
                });
            });
            builder.Services.AddControllers();

            //FluentValidations
            builder.Services.AddFluentValidationAutoValidation();
            var app = builder.Build();
          
            app.UseExceptionHandlingMiddleware();
            app.UseRouting();
            //Auth
            app.UseAuthentication();
            app.UseAuthorization();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT API token");
                });
            };
            app.UseCors();
            app.UseHttpsRedirection();
            app.MapControllers();
            app.MapProductAPIEndpoints();
            app.Run();
        }
    }
}
