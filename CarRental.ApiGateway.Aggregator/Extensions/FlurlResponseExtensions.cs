using System.Net;
using CarRental.Infrastructure.FunctionalExtensions;
using Flurl.Http;

namespace CarRental.ApiGateway.Aggregator.Extensions;

internal static class FlurlResponseExtensions
{
    internal static Error ToError(this IFlurlResponse response)
    {
        return new Error((HttpStatusCode) response.StatusCode, response.GetStringAsync().Result);
    }
}