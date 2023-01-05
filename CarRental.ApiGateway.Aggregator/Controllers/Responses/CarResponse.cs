using System;

namespace CarRental.ApiGateway.Aggregator.Controllers.Responses;

public sealed record CarResponse(Guid Id, ModelResponse Model, LocationResponse Location, 
    string Description, int ConditionRate, DateTime AcquisitionDate, 
    PriceResponse RentalPrice, int MinimumDriverAge, bool CanBeLeftInOtherLocation);
    