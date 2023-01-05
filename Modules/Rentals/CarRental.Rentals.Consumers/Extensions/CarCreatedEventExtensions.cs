using CarRental.IntegrationEvents.Events;
using CarRental.Rentals.Application.Dtos;
using CarRental.Rentals.Domain.ValueObjects;

namespace CarRental.Rentals.Consumers.Extensions;

internal static class CarCreatedEventExtensions
{
    internal static CreateCarDto ToCreateCarDto(this CarCreatedEvent carCreatedEvent) =>
        new(carCreatedEvent.Id, carCreatedEvent.LocationId, new Price(carCreatedEvent.RentalPrice, (CurrencyEnum)carCreatedEvent.RentalCurrency),
            carCreatedEvent.MinimumDriverAge, carCreatedEvent.CanBeLeftInOtherLocation);
}