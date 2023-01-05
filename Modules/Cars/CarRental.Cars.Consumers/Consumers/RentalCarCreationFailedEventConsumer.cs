using System.Threading;
using System.Threading.Tasks;
using CarRental.Cars.Application.Services;
using CarRental.IntegrationEvents.Events;
using MediatR;

namespace CarRental.Cars.Consumers.Consumers;

public sealed class RentalCarCreationFailedEventConsumer : INotificationHandler<RentalCarCreationFailedEvent>
{
    private readonly ICarsService _service;

    public RentalCarCreationFailedEventConsumer(ICarsService service)
    {
        _service = service;
    }
    
    public async Task Handle(RentalCarCreationFailedEvent notification, CancellationToken cancellationToken)
    {
        await _service.RemoveCar(notification.CarId);
    }
}