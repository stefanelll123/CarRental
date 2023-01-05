namespace CarRental.ApiGateway.Aggregator.Configurations;

public interface IServicesEndpointsConfiguration
{
    public string IdentityService { get; }
    public string CarsService { get; }
    public string RentalsService { get; }
}