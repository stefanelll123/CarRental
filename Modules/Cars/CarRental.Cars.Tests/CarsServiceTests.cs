using System;
using System.Net;
using System.Threading.Tasks;
using CarRental.Cars.Application.Services;
using CarRental.Cars.Domain;
using CarRental.Cars.Interop.Repositories;
using CSharpFunctionalExtensions;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace CarRental.Cars.Tests;

public sealed class CarsServiceTests
{
    private CarsService _sut;
    private readonly ICarsRepository _carsRepository = Substitute.For<ICarsRepository>();
    private readonly IModelsRepository _modelsRepository = Substitute.For<IModelsRepository>();
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    
    public CarsServiceTests()
    {
        _sut = new CarsService(_carsRepository, _modelsRepository, _mediator);
    }

    [Fact]
    public async Task Should_ReturnError_When_GetCarByIdAndTheIdDoesNotExists()
    {
        // Arrange
        _carsRepository.Get(Arg.Any<Guid>()).Returns(Maybe<Car?>.None);

        // Act
        var response = await _sut.GetCar(new Guid("ce251288-5063-48a7-9b24-73c9475a0ef3"));

        // Assert
        response.IsFailure.Should().BeTrue();
        response.Error.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Fact]
    public async Task Should_ReturnCar_When_GetCarById()
    {
        // Arrange
        var id = new Guid("ce251288-5063-48a7-9b24-73c9475a0ef3");
        var model = new Model("Volvo S60", "Volvo", "V60", "2.4", 
            2012, 5, GearboxType.Automatic);
        var car = new Car(model, "A family car", 5, new DateTime(2020, 10, 10))
        {
            Id = id
        };
        
        _carsRepository.Get(Arg.Any<Guid>()).Returns(Maybe<Car?>.From(car));

        // Act
        var response = await _sut.GetCar(id);

        // Assert
        response.IsSuccess.Should().BeTrue();
        response.Value.Id.Should().Be(id);
    }
}