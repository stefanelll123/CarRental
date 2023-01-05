using System;

namespace CarRental.Cars.Application.Dtos;

public sealed record CreateCarDto(Guid ModelId, string Description, 
    int ConditionRate, DateTime AcquisitionDate, Guid LocationId,
    decimal RentalPrice, int RentalCurrency, int MinimumDriverAge,
    bool CanBeLeftInOtherLocation);