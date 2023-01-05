using System.Reflection;
using CarRental.Rentals.Api;
using CarRental.Rentals.Application;
using CarRental.Rentals.Consumers;
using CarRental.Rentals.DataAccess.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Rentals.Bootstrap;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection RegisterRentalsModule(this IServiceCollection service)
    {
        return service.RegisterRentalsApplication()
            .RegisterRentalsDataAccess()
            .AddRentalsConsumers()
            .AddMappings()
            .AddValidators();
    }
    
    public static IMvcBuilder RegisterRentalsModuleControllers(this IMvcBuilder mvcBuilder)
    {
        return mvcBuilder
            .AddApplicationPart(Assembly.GetAssembly(typeof(RentalsAssembly))!);
    }
}