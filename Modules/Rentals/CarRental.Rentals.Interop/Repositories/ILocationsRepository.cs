using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Rentals.Domain;
using CSharpFunctionalExtensions;

namespace CarRental.Rentals.Interop.Repositories;

public interface ILocationsRepository
{
    Task<IEnumerable<Location>> Get();
    Task<Maybe<Location?>> Get(Guid id);
}