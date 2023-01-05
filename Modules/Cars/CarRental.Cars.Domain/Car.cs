using System;
using CarRental.Infrastructure;

namespace CarRental.Cars.Domain;

public sealed record Car(Model Model, string Description, 
    int ConditionRate, DateTime AcquisitionDate, bool IsDeleted = false) : Entity;