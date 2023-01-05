using System;

namespace CarRental.Cars.Api.Controllers.Requests;

public sealed record CarRequest(Guid ModelId, string Description, 
    int ConditionRate, DateTime AcquisitionDate, Guid LocationId,
    decimal RentalPrice, int RentalCurrency, int MinimumDriverAge,
    bool CanBeLeftInOtherLocation);