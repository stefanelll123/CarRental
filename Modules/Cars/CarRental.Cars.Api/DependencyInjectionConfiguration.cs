using CarRental.Cars.Api.Controllers.Requests;
using CarRental.Cars.Api.Controllers.Requests.Validators;
using CarRental.Cars.Domain;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Extensions.DependencyInjection;

namespace CarRental.Cars.Api;

public static class DependencyInjectionConfiguration
{
    public static IServiceCollection AddMappings(this IServiceCollection service)
    {
        service.AddAutoMapper(config =>
        {
            config.CreateMap<JsonPatchDocument<CarUpdateRequest>, JsonPatchDocument<Car>>();
            config.CreateMap<Operation<CarUpdateRequest>, Operation<Car>>();
        });
        
        return service;
    }
    
    public static IServiceCollection AddValidators(this IServiceCollection service)
    {
        service.AddTransient<IValidator<CarRequest>, CarRequestValidator>();
        service.AddTransient<IValidator<JsonPatchDocument<CarUpdateRequest>>, CarUpdateRequestValidator>();
        
        return service;
    }
}