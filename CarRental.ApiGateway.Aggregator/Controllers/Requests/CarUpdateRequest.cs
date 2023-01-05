using System;

namespace CarRental.ApiGateway.Aggregator.Controllers.Requests;

public record CarUpdateRequest(Guid? ModelId, string? Description, int? ConditionRate,
    DateTime? AcquisitionDate, Guid LocationId, decimal Value, 
    int Currency, int MinimumDriverAge, bool CanBeLeftInOtherLocation = false);
