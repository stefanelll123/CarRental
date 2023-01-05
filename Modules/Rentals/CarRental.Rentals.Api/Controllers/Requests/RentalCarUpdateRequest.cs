using System;

namespace CarRental.Rentals.Api.Controllers.Requests;

public sealed record RentalCarUpdateRequest(Guid LocationId, decimal Value, int Currency, int MinimumDriverAge, 
    bool CanBeLeftInOtherLocation = false);