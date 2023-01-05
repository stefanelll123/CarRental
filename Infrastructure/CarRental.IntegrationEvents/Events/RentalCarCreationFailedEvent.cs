using System;
using CarRental.Infrastructure.FunctionalExtensions;
using MediatR;

namespace CarRental.IntegrationEvents.Events;

public sealed class RentalCarCreationFailedEvent : INotification
{
    public Guid CarId { get; }
    public Error Error { get; }
    
    public RentalCarCreationFailedEvent(Error error, Guid carId)
    {
        Error = error;
        CarId = carId;
    }
}