using System.Collections.Generic;
using System.Linq;
using CarRental.Cars.Api.Controllers.Responses;
using CarRental.Cars.Domain;

namespace CarRental.Cars.Api.Extensions;

internal static class ModelExtensions
{
    internal static IEnumerable<ModelResponse> ToModelResponses(this IEnumerable<Model> models) =>
        models.Select(model => model.ToModelResponse());
    
    internal static ModelResponse ToModelResponse(this Model model) =>
        new(model.Id, model.FriendlyName, model.Brand, model.Version,
            model.MotorCapacity, model.Year, model.NumberOfSeats,
            (int)model.Gearbox);
}