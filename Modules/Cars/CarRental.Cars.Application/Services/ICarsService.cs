using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Cars.Application.Dtos;
using CarRental.Cars.Domain;
using CarRental.Infrastructure.FunctionalExtensions;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.JsonPatch;

namespace CarRental.Cars.Application.Services;

public interface ICarsService
{
    Task<Result<IEnumerable<Car>>> GetCars();
    Task<Result<Car, Error>> GetCar(Guid id);
    Task<Result<Guid, Error>> AddCar(CreateCarDto createCarDto);
    Task<UnitResult<Error>> RemoveCar(Guid id);
    Task<Result<Car, Error>> UpdateCar(Guid id, JsonPatchDocument<Car> patchDocument);
}