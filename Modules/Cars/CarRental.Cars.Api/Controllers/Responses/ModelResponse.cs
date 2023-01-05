using System;

namespace CarRental.Cars.Api.Controllers.Responses;

public sealed record ModelResponse(Guid Id, string FriendlyName, string Brand, string Version, 
    string MotorCapacity, int Year, int NumberOfSeats, int Gearbox);