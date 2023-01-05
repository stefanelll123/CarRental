using CarRental.Infrastructure;
using CarRental.Rentals.Domain.ValueObjects;

namespace CarRental.Rentals.Domain;

public sealed record Location(string Name, Address Address) : Entity;