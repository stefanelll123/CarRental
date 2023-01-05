using System;

namespace CarRental.Rentals.Api.Controllers.Responses;

public sealed record RentalCarResponse(Guid Id, LocationResponse Location, PriceResponse RentalPrice, int MinimumDriverAge, 
    bool CanBeLeftInOtherLocation);
    