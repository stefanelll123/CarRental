using System.Collections.Generic;
using System.Linq;
using CarRental.Rentals.Api.Controllers.Responses;
using CarRental.Rentals.Domain;

namespace CarRental.Rentals.Api.Extensions;

internal static class CarExtensions
{
    internal static IEnumerable<RentalCarResponse> ToCarResponses(this IEnumerable<Car> cars) =>
        cars.Select(car => car.ToCarResponse());
    
    internal static RentalCarResponse ToCarResponse(this Car car) =>
        new (car.Id, car.Location.ToLocationResponse(), new PriceResponse(car.RentalPrice.Value, (int)car.RentalPrice.Currency), car.MinimumDriverAge, car.CanBeLeftInOtherLocation);
}