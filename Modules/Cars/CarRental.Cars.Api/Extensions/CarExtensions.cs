using System.Collections.Generic;
using System.Linq;
using CarRental.Cars.Api.Controllers.Responses;
using CarRental.Cars.Domain;

namespace CarRental.Cars.Api.Extensions;

internal static class CarExtensions
{
    internal static IEnumerable<CarResponse> ToCarResponses(this IEnumerable<Car> cars) =>
        cars.Select(car => car.ToCarResponse());
    
    internal static CarResponse ToCarResponse(this Car car) =>
        new (car.Id, car.Model.ToModelResponse(),  
            car.Description, car.ConditionRate, car.AcquisitionDate);
}