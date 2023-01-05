using CarRental.Identity.Domain.ValueObjects;
using CarRental.Infrastructure;

namespace CarRental.Identity.Domain;

public sealed record User(string FirstName, string LastName, Email Email, string Username, string Password) : Entity;