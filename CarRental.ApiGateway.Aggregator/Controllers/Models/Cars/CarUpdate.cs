using System;

namespace CarRental.ApiGateway.Aggregator.Controllers.Models.Cars;

public sealed record CarUpdate(Guid? ModelId, string? Description, 
    int? ConditionRate, DateTime? AcquisitionDate);