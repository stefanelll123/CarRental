using CarRental.ApiGateway.Aggregator.Controllers.Models.Cars;
using CarRental.ApiGateway.Aggregator.Controllers.Models.Rentals;
using CarRental.ApiGateway.Aggregator.Controllers.Responses;

namespace CarRental.ApiGateway.Aggregator.Controllers.Extensions;

internal static class CarExtensions
{
    internal static CarResponse ToCarResponse(this Car car, RentalCar rentalCar) =>
        new CarResponse(car.Id, car.Model.ToModelResponse(), rentalCar.Location.ToLocationResponse(),
            car.Description, car.ConditionRate, car.AcquisitionDate,
            rentalCar.RentalPrice.ToPriceResponse(), rentalCar.MinimumDriverAge, rentalCar.CanBeLeftInOtherLocation);
}