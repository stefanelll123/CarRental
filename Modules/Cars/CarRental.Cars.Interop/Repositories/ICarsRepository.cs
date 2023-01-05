using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Cars.Domain;
using CSharpFunctionalExtensions;

namespace CarRental.Cars.Interop.Repositories;

public interface ICarsRepository
{
    public Task<IEnumerable<Car>> Get();
    public Task<Maybe<Car?>> Get(Guid id);
    public Task<Maybe<Guid>> Create(Car car);
    public Task Delete(Car car);
    public Task Update(Guid id, Car car);
}