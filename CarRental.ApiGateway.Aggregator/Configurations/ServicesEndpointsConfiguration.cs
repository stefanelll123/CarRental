using System;

namespace CarRental.ApiGateway.Aggregator.Configurations;

internal sealed class ServicesEndpointsConfiguration : IServicesEndpointsConfiguration
{
    public string? IdentityService { get; set; }
    public string? CarsService { get; set; }
    public string? RentalsService { get; set; }

    string IServicesEndpointsConfiguration.IdentityService => IdentityService ?? throw new ArgumentNullException($"{nameof(IdentityService)} is not defined");

    string IServicesEndpointsConfiguration.CarsService => CarsService ?? throw new ArgumentNullException($"{nameof(CarsService)} is not defined");

    string IServicesEndpointsConfiguration.RentalsService => RentalsService ?? throw new ArgumentNullException($"{nameof(RentalsService)} is not defined");
}