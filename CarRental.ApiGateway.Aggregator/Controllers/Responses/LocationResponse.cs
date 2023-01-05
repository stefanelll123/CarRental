using System;

namespace CarRental.ApiGateway.Aggregator.Controllers.Responses;

public sealed record LocationResponse(Guid Id, string Name, AddressResponse Address);