using System;
using CarRental.Infrastructure;

namespace CarRental.Rentals.Domain;

public sealed record Rental(string ClientFullName, Car RentedCar, Agent RentalAgent, 
    string ClientIdentificationNumber, int DriverAge, Location RentalLocation, 
    DateTime StartRentingDate, DateTime EndRentingDate, bool IsClosed) : Entity;