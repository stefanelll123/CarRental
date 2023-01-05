using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CarRental.Infrastructure.FunctionalExtensions;
using CarRental.Rentals.Interop.Repositories;
using CarRental.Rentals.Domain;
using CSharpFunctionalExtensions;

namespace CarRental.Rentals.Application.Services;

public sealed class LocationsService : ILocationsService
{
    private readonly ILocationsRepository _repository;

    public LocationsService(ILocationsRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<IEnumerable<Location>>> GetLocations()
    {
        return Result.Success(await _repository.Get());
    }
    
    public async Task<Result<Location, Error>> GetLocation(Guid id)
    {
        return await Result.Try(() => _repository.Get(id),
                error => new Error(error))
            .Ensure(model => model.HasValue, 
                new Error(HttpStatusCode.NotFound, $"Cannot find any model with id {id}"))
            .OnSuccessTry(model => model.Value!);
    }
}