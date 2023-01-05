using System.Reflection;
using CarRental.Cars.Api;
using CarRental.Cars.Application;
using CarRental.Cars.Consumers;
using CarRental.Cars.DataAccess.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Cars.Bootstrap;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection RegisterCarsModule(this IServiceCollection service)
    {
        return service.RegisterCarsApplication()
            .RegisterCarsDataAccess()
            .AddCarsConsumers()
            .AddMappings()
            .AddValidators();
    }
    
    public static IMvcBuilder RegisterCarsModuleControllers(this IMvcBuilder mvcBuilder)
    {
        return mvcBuilder
            .AddApplicationPart(Assembly.GetAssembly(typeof(CarsAssembly))!);
    }
}