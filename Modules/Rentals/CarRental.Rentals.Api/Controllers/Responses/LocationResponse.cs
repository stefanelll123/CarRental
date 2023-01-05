using System;

namespace CarRental.Rentals.Api.Controllers.Responses;

public sealed record LocationResponse(Guid Id, string Name, AddressResponse Address);