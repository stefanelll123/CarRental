using System.Threading;
using System.Threading.Tasks;
using CarRental.IntegrationEvents.Events;
using CarRental.Rentals.Application.Services;
using CarRental.Rentals.Consumers.Extensions;
using MediatR;

namespace CarRental.Rentals.Consumers.Consumers;

public sealed class CarCreatedEventConsumer : INotificationHandler<CarCreatedEvent>
{
    private readonly ICarsService _service;

    public CarCreatedEventConsumer(ICarsService service)
    {
        _service = service;
    }
    
    public async Task Handle(CarCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _service.AddCar(notification.ToCreateCarDto());
    }
}