using CarRental.Cars.Api.Controllers.Requests;
using CarRental.Cars.Application.Dtos;

namespace CarRental.Cars.Api.Extensions;

internal static class CarRequestExtensions
{
    internal static CreateCarDto ToCreateCarDto(this CarRequest carRequest) =>
        new(carRequest.ModelId, carRequest.Description, carRequest.ConditionRate,
            carRequest.AcquisitionDate, carRequest.LocationId, carRequest.RentalPrice,
            carRequest.RentalCurrency, carRequest.MinimumDriverAge, carRequest.CanBeLeftInOtherLocation);
}