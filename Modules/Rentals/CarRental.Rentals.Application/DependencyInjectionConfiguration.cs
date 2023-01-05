using CarRental.Rentals.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Rentals.Application;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection RegisterRentalsApplication(this IServiceCollection service)
    {
        service.AddTransient<ICarsService, CarsService>();
        service.AddTransient<ILocationsService, LocationsService>();
        
        return service;
    }
}