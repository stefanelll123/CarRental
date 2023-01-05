using System;

namespace CarRental.Cars.Api.Controllers.Responses;

public sealed record CarResponse(Guid Id, ModelResponse Model, string Description,
    int ConditionRate, DateTime AcquisitionDate);
    