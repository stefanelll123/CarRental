using System;
using System.Collections.Generic;
using CarRental.Rentals.Domain;
using CarRental.Rentals.Domain.ValueObjects;

namespace CarRental.Rentals.DataAccess.Adapters;

public sealed class DatabaseContext
{
    public List<Car> Cars { get; }
    public List<Location> Locations { get; }
    
    public DatabaseContext()
    {
        var location1 = new Location("San Julien, Malta", Address.Create("Malta", "San Julien", "1", "683443").Value)
        {
            Id = new Guid("e0e0d3b5-f54d-4add-a690-3dabca7c0a2e")
        };
        var location2 = new Location("Bucharest, Romania", Address.Create("Romania", "Bucharest", "First", "700231").Value)
        {
            Id = new Guid("902a54af-58e7-426d-9195-767d2609e059")
        };
        Locations = new List<Location> {location1, location2};
        
        var car1 = new Car(location1, new Price(100, CurrencyEnum.Euro), 10, true)
        {
            Id = new Guid("ca9a0d82-b1a4-4e8c-a39b-9a0fb5392138")
        };
        var car2 = new Car(location1, new Price(500, CurrencyEnum.Euro), 10, true)
        {
            Id = new Guid("34a37bb9-28b8-48f5-8ff7-bfcf22531535")
        };
        var car3 = new Car(location1, new Price(150, CurrencyEnum.Euro), 10, true)
        {
            Id = new Guid("1bd8ef6c-7bee-4b9b-91c2-3fb478f4d417")
        };
        Cars = new List<Car> {car1, car2, car3};
    }
}