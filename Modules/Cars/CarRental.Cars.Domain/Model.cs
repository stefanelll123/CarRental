using CarRental.Infrastructure;

namespace CarRental.Cars.Domain;

public sealed record Model(string FriendlyName, string Brand, string Version, string MotorCapacity, 
    int Year, int NumberOfSeats, GearboxType Gearbox) : Entity;