using System;

namespace CarRental.ApiGateway.Aggregator.Controllers.Models.Rentals;

public sealed record RentalCarUpdate(Guid LocationId, decimal Value, int Currency, int MinimumDriverAge, 
    bool CanBeLeftInOtherLocation = false);