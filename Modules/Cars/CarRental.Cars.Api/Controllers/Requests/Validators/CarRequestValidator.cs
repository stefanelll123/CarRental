using FluentValidation;

namespace CarRental.Cars.Api.Controllers.Requests.Validators;

internal sealed class CarRequestValidator : AbstractValidator<CarRequest>
{
    public CarRequestValidator()
    {
        RuleFor(car => car.ModelId).NotNull();
        RuleFor(car => car.Description).NotEmpty();
        RuleFor(car => car.ConditionRate)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(5);
        RuleFor(car => car.AcquisitionDate).NotNull();
    }
}