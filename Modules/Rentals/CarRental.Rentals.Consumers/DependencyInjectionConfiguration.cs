using CarRental.Rentals.Consumers.Consumers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Rentals.Consumers;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddRentalsConsumers(this IServiceCollection service)
    {
        service.AddMediatR(typeof(CarCreatedEventConsumer).Assembly);
        return service;
    }
}