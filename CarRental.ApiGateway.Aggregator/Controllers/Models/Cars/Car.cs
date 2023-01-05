using System;

namespace CarRental.ApiGateway.Aggregator.Controllers.Models.Cars;

public sealed record Car(Guid Id, Model Model, string Description,
    int ConditionRate, DateTime AcquisitionDate);
    