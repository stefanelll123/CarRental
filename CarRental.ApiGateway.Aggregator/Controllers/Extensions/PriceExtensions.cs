using CarRental.ApiGateway.Aggregator.Controllers.Models.Rentals;
using CarRental.ApiGateway.Aggregator.Controllers.Responses;

namespace CarRental.ApiGateway.Aggregator.Controllers.Extensions;

internal static class PriceExtensions
{
    internal static PriceResponse ToPriceResponse(this Price price) =>
        new PriceResponse(price.Value, price.Currency);
}