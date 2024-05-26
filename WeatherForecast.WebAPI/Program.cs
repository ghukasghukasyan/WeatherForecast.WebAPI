
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WeatherForecast.Application.Behaviours;
using WeatherForecast.Application.Middlewares;
using WeatherForecast.Domain.Core;
using WeatherForecast.Domain.Interfaces.Weathers;
using WeatherForecast.Domain.Services.Weathers;
using WeatherForecast.Infrastructure.DataAccess;

namespace WeatherForecast
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var applicationAssembly = Assembly.Load("WeatherForecast.Application");
            
            var infrastructureAssembly = Assembly.Load("WeatherForecast.Infrastructure");


            // Add services to the container.
          
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly, infrastructureAssembly));

            builder.Services.AddDbContext<WeatherContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

            });


            builder.Services.AddScoped<IWeatherContext, WeatherContext>();
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            builder.Services.AddValidatorsFromAssembly(Assembly.Load("WeatherForecast.Application"));
            builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            builder.Services.AddScoped<IWeaterDomainService, WeatherDomainService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            
            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
