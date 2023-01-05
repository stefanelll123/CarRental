using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Rentals.Interop.Repositories;
using CarRental.Rentals.Domain;
using CSharpFunctionalExtensions;

namespace CarRental.Rentals.DataAccess.Adapters.Repositories;

internal sealed class CarsRepository : ICarsRepository
{
    private readonly DatabaseContext _databaseContext;
    
    public CarsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<IEnumerable<Car>> Get()
    {
        return await Task.FromResult(_databaseContext.Cars);
    }

    public Task<Maybe<Car?>> Get(Guid id)
    {
        var car = _databaseContext.Cars.FirstOrDefault(car => car.Id == id);
        return Task.FromResult(Maybe.From(car));
    }

    public Task<Maybe<Guid>> Create(Car car)
    {
        _databaseContext.Cars.Add(car);

        return Task.FromResult(Maybe.From(car.Id));
    }

    public Task Delete(Car car)
    {
        _databaseContext.Cars.Remove(car);
        return Task.CompletedTask;
    }
    
    public Task Update(Guid id, Car car)
    {
        var oldCar = _databaseContext.Cars.FirstOrDefault(x => x.Id == id);
        if (oldCar != null)
        {
            _databaseContext.Cars.Remove(oldCar);
            _databaseContext.Cars.Add(car);
        }
        
        return Task.CompletedTask;;
    }
}