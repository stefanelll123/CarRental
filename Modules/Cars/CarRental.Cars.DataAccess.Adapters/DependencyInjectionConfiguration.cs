using CarRental.Cars.DataAccess.Adapters.Repositories;
using CarRental.Cars.Interop.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Cars.DataAccess.Adapters;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection RegisterCarsDataAccess(this IServiceCollection service)
    {
        service.AddSingleton<DatabaseContext>();
        service.AddTransient<ICarsRepository, CarsRepository>();
        service.AddTransient<IModelsRepository, ModelsRepository>();
        
        return service;
    }
}