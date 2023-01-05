using CarRental.Infrastructure;
using CarRental.Rentals.Domain.ValueObjects;

namespace CarRental.Rentals.Domain;

public sealed record Car(Location Location, Price RentalPrice, int MinimumDriverAge, 
    bool CanBeLeftInOtherLocation = false) : Entity;