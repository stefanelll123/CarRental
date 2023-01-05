using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Cars.Domain;
using CSharpFunctionalExtensions;

namespace CarRental.Cars.Interop.Repositories;

public interface IModelsRepository
{
    Task<IEnumerable<Model>> Get();
    
    Task<Maybe<Model?>> Get(Guid id);
}