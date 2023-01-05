using System;
using CarRental.Rentals.Domain.ValueObjects;

namespace CarRental.Rentals.Application.Dtos;

public sealed record CreateCarDto(Guid Id, Guid LocationId, Price RentalPrice, 
    int MinimumDriverAge, bool CanBeLeftInOtherLocation = false);