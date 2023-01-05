using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CarRental.Cars.Application.Dtos;
using CarRental.Cars.Application.Extensions;
using CarRental.Cars.Interop.Repositories;
using CarRental.Cars.Domain;
using CarRental.Infrastructure.FunctionalExtensions;
using CarRental.IntegrationEvents.Events;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

[assembly: InternalsVisibleTo("CarRental.Cars.Tests")]

namespace CarRental.Cars.Application.Services;

internal sealed class CarsService : ICarsService
{
    private readonly ICarsRepository _carsRepository;
    private readonly IModelsRepository _modelsRepository;
    private readonly IMediator _mediator;

    public CarsService(
        ICarsRepository carsRepository, 
        IModelsRepository modelsRepository,
        IMediator mediator)
    {
        _carsRepository = carsRepository;
        _modelsRepository = modelsRepository;
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
        return await Result.Try(() => _modelsRepository.Get(createCarDto.ModelId), error => new Error(error))
            .Ensure(model => model.HasValue,
                _ => new Error(HttpStatusCode.BadRequest, $"Cannot find any model with id {createCarDto.ModelId}"))
            .OnSuccessTry(model => model.Value!)
            .OnSuccessTry(model => new Car(model, createCarDto.Description, createCarDto.ConditionRate,
                createCarDto.AcquisitionDate))
            .OnSuccessTry(car => _carsRepository.Create(car))
            .Ensure(response => response.HasValue,
                _ => new Error(HttpStatusCode.InternalServerError, $"Error creating the new car"))
            .OnSuccessTry(response => response.Value)
            .Tap(carId => _mediator.Publish(createCarDto.ToCarCreatedEvent(carId)));
    }

    public async Task<UnitResult<Error>> RemoveCar(Guid id)
    {
        return await Result.Try(() => _carsRepository.Get(id), error => new Error(error))
            .Ensure(car => car.HasValue, _ => new Error(HttpStatusCode.NotFound, $"Cannot find any car with id {id}"))
            .OnSuccessTry(car => _carsRepository.Delete(car.Value!))
            .OnSuccessTry(_ => _mediator.Publish(new CarDeletedEvent(id)));
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
