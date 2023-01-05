using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRental.Cars.Interop.Repositories;
using CarRental.Cars.Domain;
using CSharpFunctionalExtensions;

namespace CarRental.Cars.DataAccess.Adapters.Repositories;

internal sealed class ModelsRepository : IModelsRepository
{
    private readonly DatabaseContext _databaseContext;

    public ModelsRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<IEnumerable<Model>> Get()
    {
        return await Task.FromResult(_databaseContext.Models);
    }

    public async Task<Maybe<Model?>> Get(Guid id)
    {
        var model = _databaseContext.Models.FirstOrDefault(model => model.Id == id);
        return await Task.FromResult(Maybe.From(model));
    }
}