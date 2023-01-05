using CarRental.Cars.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Cars.Application;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection RegisterCarsApplication(this IServiceCollection service)
    {
        service.AddTransient<ICarsService, CarsService>();
        service.AddTransient<IModelsService, ModelsService>();
        
        return service;
    }
}