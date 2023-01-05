using CarRental.ApiGateway.Aggregator.Controllers.Models.Rentals;
using CarRental.ApiGateway.Aggregator.Controllers.Responses;

namespace CarRental.ApiGateway.Aggregator.Controllers.Extensions;

internal static class LocationExtensions
{
    internal static LocationResponse ToLocationResponse(this Location location) =>
        new LocationResponse(location.Id, location.Name, location.Address.ToAddressResponse());

    internal static AddressResponse ToAddressResponse(this Address address) =>
        new AddressResponse(address.Country, address.City, address.Street, address.ZipCode);
}