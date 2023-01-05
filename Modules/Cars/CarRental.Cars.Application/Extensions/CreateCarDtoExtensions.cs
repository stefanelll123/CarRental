using System;
using CarRental.Cars.Application.Dtos;
using CarRental.IntegrationEvents.Events;

namespace CarRental.Cars.Application.Extensions;

internal static class CreateCarDtoExtensions
{
    internal static CarCreatedEvent ToCarCreatedEvent(this CreateCarDto createCarDto, Guid id) =>
        new(id, createCarDto.LocationId, createCarDto.RentalPrice,
            createCarDto.RentalCurrency, createCarDto.MinimumDriverAge, createCarDto.CanBeLeftInOtherLocation);
}