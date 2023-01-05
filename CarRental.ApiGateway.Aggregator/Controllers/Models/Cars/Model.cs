using System;

namespace CarRental.ApiGateway.Aggregator.Controllers.Models.Cars;

public sealed record Model(Guid Id, string FriendlyName, string Brand, string Version, 
    string MotorCapacity, int Year, int NumberOfSeats, int Gearbox);