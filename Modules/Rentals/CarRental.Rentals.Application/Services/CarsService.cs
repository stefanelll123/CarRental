using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CarRental.Infrastructure.FunctionalExtensions;
using CarRental.IntegrationEvents.Events;
using CarRental.Rentals.Application.Dtos;
using CarRental.Rentals.Interop.Repositories;
using CarRental.Rentals.Domain;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace CarRental.Rentals.Application.Services;

internal sealed class CarsService : ICarsService
{
    private readonly ICarsRepository _carsRepository;
    private readonly ILocationsRepository _locationsRepository;
    private readonly IMediator _mediator;

    public CarsService(ICarsRepository carsRepository, ILocationsRepository locationsRepository, IMediator mediator)
    {
        _carsRepository = carsRepository;
        _locationsRepository = locationsRepository;
        _mediator = mediator;
    }
    
    public async Task<Result<IEnumerable<Car>>> GetCars()
    {
        return await Result.Try(() => _carsRepository.Get());
    }

    public async Task<Result<Car, Error>> GetCar(Guid id)
    {
        return await Result.Try(() => _carsRepository.Get(id), error => new Error(error))
            .Ensure(car => car.HasValue, _ => new Error(HttpStatusCode.NotFound, $"Cannot find any car with id {id}"))
            .OnSuccessTry(car => car.Value!);
    }

    public async Task<Result<Guid, Error>> AddCar(CreateCarDto createCarDto)
    {
        return await Result.Try(() => _locationsRepository.Get(createCarDto.LocationId), error => new Error(error))
            .Ensure(location => location.HasValue,
                _ => new Error(HttpStatusCode.BadRequest, $"Cannot find any location with id {createCarDto.LocationId}"))
            .OnSuccessTry(location =>
                new Car(location.Value!, createCarDto.RentalPrice, createCarDto.MinimumDriverAge,
                    createCarDto.CanBeLeftInOtherLocation)
                {
                    Id = createCarDto.Id
                })
            .OnSuccessTry(car => _carsRepository.Create(car), error => new Error(error))
            .Ensure(response => response.HasValue,
                _ => new Error(HttpStatusCode.InternalServerError, "Error creating the new car"))
            .OnSuccessTry(response => response.Value)
            .OnFailure(error => _mediator.Publish(new RentalCarCreationFailedEvent(error, createCarDto.Id)));
    }

    public async Task<UnitResult<Error>> RemoveCar(Guid id)
    {
        return await Result.Try(() => _carsRepository.Get(id), error => new Error(error))
            .Ensure(car => car.HasValue, _ => new Error(HttpStatusCode.NotFound, $"Cannot find any car with id {id}"))
            .OnSuccessTry(car => _carsRepository.Delete(car.Value!));
    }

    public async Task<Result<Car, Error>> UpdateCar(Guid id, JsonPatchDocument<Car> patchDocument)
    {
        return await Result.Try(() => _carsRepository.Get(id), error => new Error(error))
            .Ensure(car => car.HasValue, _ => new Error(HttpStatusCode.NotFound, $"Cannot find any car with id {id}"))
            .OnSuccessTry(car => car.Value!)
            .Tap(car => patchDocument.ApplyTo(car, _ => {}))
            .Tap(car => _carsRepository.Update(id, car));
    }
}