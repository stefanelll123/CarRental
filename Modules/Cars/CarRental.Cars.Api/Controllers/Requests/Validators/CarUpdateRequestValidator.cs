using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;

namespace CarRental.Cars.Api.Controllers.Requests.Validators;

public sealed class CarUpdateRequestValidator : AbstractValidator<JsonPatchDocument<CarUpdateRequest>>
{
    public CarUpdateRequestValidator()
    {
        RuleFor(patchDocument => patchDocument.Operations.FirstOrDefault(x => x.path == "id"))
            .Null()
            .WithMessage("Cannot update the id of a car");
    }
}