using System.Threading;
using System.Threading.Tasks;
using CarRental.IntegrationEvents.Events;
using CarRental.Rentals.Application.Services;
using MediatR;

namespace CarRental.Rentals.Consumers.Consumers;

public sealed class CarDeletedEventConsumer : INotificationHandler<CarDeletedEvent>
{
    private readonly ICarsService _service;

    public CarDeletedEventConsumer(ICarsService service)
    {
        _service = service;
    }
    
    public async Task Handle(CarDeletedEvent notification, CancellationToken cancellationToken)
    {
        await _service.RemoveCar(notification.CarId);
    }
}