using CarRental.Rentals.Api.Controllers.Requests;
using CarRental.Rentals.Domain;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Rentals.Api;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddMappings(this IServiceCollection service)
    {
        service.AddAutoMapper(config =>
        {
            config.CreateMap<JsonPatchDocument<RentalCarUpdateRequest>, JsonPatchDocument<Car>>();
            config.CreateMap<Operation<RentalCarUpdateRequest>, Operation<Car>>();
        });
        
        return service;
    }
    
    public static IServiceCollection AddValidators(this IServiceCollection service)
    {
        return service;
    }
}