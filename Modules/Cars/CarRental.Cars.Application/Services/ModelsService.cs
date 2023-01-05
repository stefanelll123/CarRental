using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using CarRental.Cars.Interop.Repositories;
using CarRental.Cars.Domain;
using CarRental.Infrastructure.FunctionalExtensions;
using CSharpFunctionalExtensions;

namespace CarRental.Cars.Application.Services;

internal sealed class ModelsService : IModelsService
{
    private readonly IModelsRepository _repository;

    public ModelsService(IModelsRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<IEnumerable<Model>>> GetModels()
    {
        return Result.Success(await _repository.Get());
    }
    
    public async Task<Result<Model, Error>> GetModel(Guid id)
    {
        return await Result.Try(() => _repository.Get(id),
                error => new Error(error))
            .Ensure(model => model.HasValue, 
                new Error(HttpStatusCode.NotFound, $"Cannot find any model with id {id}"))
            .OnSuccessTry(model => model.Value!);
    }
}