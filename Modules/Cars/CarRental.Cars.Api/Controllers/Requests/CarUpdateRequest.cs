using System;

namespace CarRental.Cars.Api.Controllers.Requests;

public sealed record CarUpdateRequest(Guid? ModelId, string? Description, 
    int? ConditionRate, DateTime? AcquisitionDate);