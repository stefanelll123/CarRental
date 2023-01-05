using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Infrastructure.FunctionalExtensions;
using CarRental.Rentals.Application.Dtos;
using CarRental.Rentals.Domain;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.JsonPatch;

namespace CarRental.Rentals.Application.Services;

public interface ICarsService
{
    Task<Result<IEnumerable<Car>>> GetCars();
    Task<Result<Car, Error>> GetCar(Guid id);
    Task<Result<Guid, Error>> AddCar(CreateCarDto createCarDto);
    Task<UnitResult<Error>> RemoveCar(Guid id);
    Task<Result<Car, Error>> UpdateCar(Guid id, JsonPatchDocument<Car> patchDocument);
}