using CarRental.Infrastructure;

namespace CarRental.Rentals.Domain;

public sealed record Agent(Location Location) : Entity;