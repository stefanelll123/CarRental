using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Cars.Domain;
using CarRental.Infrastructure.FunctionalExtensions;
using CSharpFunctionalExtensions;

namespace CarRental.Cars.Application.Services;

public interface IModelsService
{
    Task<Result<IEnumerable<Model>>> GetModels();
    Task<Result<Model, Error>> GetModel(Guid id);
}