using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Rentals.Interop.Repositories;
using CarRental.Rentals.Domain;
using CSharpFunctionalExtensions;

namespace CarRental.Rentals.DataAccess.Adapters.Repositories;

internal sealed class LocationsRepository : ILocationsRepository
{
    private readonly DatabaseContext _databaseContext;

    public LocationsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<IEnumerable<Location>> Get()
    {
        return await Task.FromResult(_databaseContext.Locations);
    }

    public async Task<Maybe<Location?>> Get(Guid id)
    {
        var location = _databaseContext.Locations.FirstOrDefault(location => location.Id == id);
        return await Task.FromResult(Maybe.From(location));
    }
}