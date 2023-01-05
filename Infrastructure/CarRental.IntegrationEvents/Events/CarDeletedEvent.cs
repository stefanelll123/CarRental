using System;
using MediatR;

namespace CarRental.IntegrationEvents.Events;

public sealed class CarDeletedEvent : INotification
{
    public Guid CarId { get; }
    
    public CarDeletedEvent(Guid carId)
    {
        CarId = carId;
    }
}