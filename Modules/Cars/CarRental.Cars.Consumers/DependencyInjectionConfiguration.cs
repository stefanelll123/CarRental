using CarRental.Cars.Consumers.Consumers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Cars.Consumers;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddCarsConsumers(this IServiceCollection service)
    {
        service.AddMediatR(typeof(RentalCarCreationFailedEventConsumer).Assembly);
        return service;
    }
}