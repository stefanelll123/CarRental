using System.Collections.Generic;
using System.Linq;
using CarRental.Rentals.Api.Controllers.Responses;
using CarRental.Rentals.Domain;

namespace CarRental.Rentals.Api.Extensions;

internal static class LocationExtensions
{
    internal static IEnumerable<LocationResponse> ToLocationResponses(this IEnumerable<Location> locations) =>
        locations.Select(location => location.ToLocationResponse());

    internal static LocationResponse ToLocationResponse(this Location location) =>
        new(location.Id, location.Name,
            new AddressResponse(location.Address.Country, location.Address.City, location.Address.Street,
                location.Address.ZipCode));
}