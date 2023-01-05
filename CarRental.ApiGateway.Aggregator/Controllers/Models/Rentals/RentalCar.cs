using System;

namespace CarRental.ApiGateway.Aggregator.Controllers.Models.Rentals;

public sealed record RentalCar(Guid Id, Location Location, Price RentalPrice, int MinimumDriverAge, 
    bool CanBeLeftInOtherLocation);
    