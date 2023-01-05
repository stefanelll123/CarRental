using System;
using System.Collections.Generic;
using CarRental.Cars.Domain;

namespace CarRental.Cars.DataAccess.Adapters;

public sealed class DatabaseContext
{
    public List<Model> Models { get; }
    public List<Car> Cars { get; }
    
    public DatabaseContext()
    {
        var model1 = new Model("Volvo S60", "Volvo", "V60", "2.4", 2012, 5, GearboxType.Automatic)
        {
            Id = new Guid("b01991fc-3b78-49da-b54e-61ca727f5f6d")
        };
        var model2 = new Model("Volvo S60", "Volvo", "V40", "2.4", 2014, 5, GearboxType.Manual)        {
            Id = new Guid("e05d8047-96f4-4db4-abb3-5a53ccec4fd1")
        };
        Models = new List<Model> {model1, model2};

        var car1 = new Car(model1, "A family car", 5, new DateTime(2020, 10, 10))
        {
            Id = new Guid("ca9a0d82-b1a4-4e8c-a39b-9a0fb5392138")
        };
        var car2 = new Car(model1, "A family car", 5, new DateTime(2020, 10, 10))
        {
            Id = new Guid("34a37bb9-28b8-48f5-8ff7-bfcf22531535")
        };
        var car3 = new Car(model2, "A smaller car", 4, new DateTime(2020, 10, 10))
        {
            Id = new Guid("1bd8ef6c-7bee-4b9b-91c2-3fb478f4d417")
        };
        Cars = new List<Car> {car1, car2, car3};
    }
}