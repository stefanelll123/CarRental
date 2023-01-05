using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Infrastructure.FunctionalExtensions;
using CarRental.Rentals.Domain;
using CSharpFunctionalExtensions;

namespace CarRental.Rentals.Application.Services;

public interface ILocationsService
{
    Task<Result<IEnumerable<Location>>> GetLocations();
    Task<Result<Location, Error>> GetLocation(Guid id);
}