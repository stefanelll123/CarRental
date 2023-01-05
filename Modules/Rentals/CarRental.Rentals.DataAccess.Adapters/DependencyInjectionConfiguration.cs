using CarRental.Rentals.DataAccess.Adapters.Repositories;
using CarRental.Rentals.Interop.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Rentals.DataAccess.Adapters;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection RegisterRentalsDataAccess(this IServiceCollection service)
    {
        service.AddSingleton<DatabaseContext>();
        service.AddTransient<ICarsRepository, CarsRepository>();
        service.AddTransient<ILocationsRepository, LocationsRepository>();
        
        return service;
    }
}