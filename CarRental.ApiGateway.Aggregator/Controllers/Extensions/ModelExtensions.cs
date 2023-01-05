using CarRental.ApiGateway.Aggregator.Controllers.Models.Cars;
using CarRental.ApiGateway.Aggregator.Controllers.Responses;

namespace CarRental.ApiGateway.Aggregator.Controllers.Extensions;

internal static class ModelExtensions
{
    internal static ModelResponse ToModelResponse(this Model model) =>
        new(model.Id, model.FriendlyName, model.Brand,
            model.Version, model.MotorCapacity, model.Year,
            model.NumberOfSeats, model.Gearbox);
}