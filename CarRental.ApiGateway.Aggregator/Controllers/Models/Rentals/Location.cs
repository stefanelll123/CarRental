using System;

namespace CarRental.ApiGateway.Aggregator.Controllers.Models.Rentals;

public sealed record Location(Guid Id, string Name, Address Address);