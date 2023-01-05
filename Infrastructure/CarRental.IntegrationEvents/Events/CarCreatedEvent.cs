using System;
using MediatR;

namespace CarRental.IntegrationEvents.Events;

public sealed class CarCreatedEvent : INotification
{
    public Guid Id { get; }
    public Guid LocationId { get; }
    public decimal RentalPrice { get; }
    public int RentalCurrency { get; }
    public int MinimumDriverAge { get; }
    public bool CanBeLeftInOtherLocation { get; }
    
    public CarCreatedEvent(Guid id, Guid locationId, decimal rentalPrice,
        int rentalCurrency, int minimumDriverAge, bool canBeLeftInOtherLocation = false)
    {
        Id = id;
        LocationId = locationId;
        RentalPrice = rentalPrice;
        RentalCurrency = rentalCurrency;
        MinimumDriverAge = minimumDriverAge;
        CanBeLeftInOtherLocation = canBeLeftInOtherLocation;
    }
}