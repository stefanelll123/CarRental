using CSharpFunctionalExtensions;

namespace CarRental.Rentals.Domain.ValueObjects;

public sealed record Address
{
    public string Country { get; }
    public string City { get; }
    public string Street { get; }
    public string ZipCode { get; }
    
    private Address(string country, string city, string street, string zipCode)
    {
        Country = country;
        City = city;
        Street = street;
        ZipCode = zipCode;
    }

    public static Result<Address> Create(string country, string city, string street, string zipCode)
    {
        if (string.IsNullOrEmpty(country)
            || string.IsNullOrEmpty(city)
            || string.IsNullOrEmpty(street)
            || string.IsNullOrEmpty(zipCode))
        {
            return Result.Failure<Address>("Invalid data to create a new address");
        }

        return Result.Success(new Address(country, city, street, zipCode));
    }
}