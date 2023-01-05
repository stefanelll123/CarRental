using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.ApiGateway.Aggregator.Configurations;
using CarRental.ApiGateway.Aggregator.Controllers.Extensions;
using CarRental.ApiGateway.Aggregator.Controllers.Models.Cars;
using CarRental.ApiGateway.Aggregator.Controllers.Models.Rentals;
using CarRental.ApiGateway.Aggregator.Controllers.Requests;
using CarRental.ApiGateway.Aggregator.Controllers.Responses;
using CarRental.ApiGateway.Aggregator.Extensions;
using CarRental.Infrastructure.FunctionalExtensions;
using CSharpFunctionalExtensions;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

namespace CarRental.ApiGateway.Aggregator.Controllers;

[ApiController]
[Route("api/v1/cars")]
public sealed class CarsController : ControllerBase
{
    private readonly IServicesEndpointsConfiguration _endpoints;
    private readonly IMapper _mapper;

    public CarsController(IServicesEndpointsConfiguration endpoints, IMapper mapper)
    {
        _endpoints = endpoints;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(CarResponse[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var cars = await _endpoints.CarsService
            .AppendPathSegment("cars/v1/cars")
            .GetJsonAsync<Car[]>();
        
        var rentalCars = await _endpoints.RentalsService
            .AppendPathSegment("rentals/v1/cars")
            .GetJsonAsync<RentalCar[]>();

        if (cars == null)
        {
            return Ok(new List<CarResponse>());
        }

        var carResponses = cars.Select(car =>
        {
            var rentalCar = rentalCars.FirstOrDefault();
            return rentalCar != null ? car.ToCarResponse(rentalCar) : null;
        }).Where(response => response != null);

        return Ok(carResponses);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CarResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await Result.Try(() =>
                    _endpoints.CarsService.AppendPathSegment($"cars/v1/cars/{id}")
                        .AllowAnyHttpStatus().GetAsync(),
                error => new Error(error))
            .Ensure(response => response.StatusCode == (int) HttpStatusCode.OK,
                response => response.ToError())
            .OnSuccessTry(response => response.GetJsonAsync<Car>())
            .OnSuccessTry(async car => (car,
                rentalCarResponse: await _endpoints.CarsService.AppendPathSegment($"rentals/v1/cars/{id}")
                                            .AllowAnyHttpStatus().GetAsync()))
            .Ensure(input => input.rentalCarResponse.StatusCode == (int) HttpStatusCode.OK,
                input => input.rentalCarResponse.ToError())
            .OnSuccessTry(async input =>
                input.car.ToCarResponse(await input.rentalCarResponse.GetJsonAsync<RentalCar>()));

        return result.IsSuccess ? Ok(result.Value) : result.Error.ToRedirectHttpResponse();
    }
    
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(Guid id, [FromBody] JsonPatchDocument<CarUpdateRequest> patchDocument)
    {
        var carPatchDocument = FilterOperationsForContract(_mapper.Map<JsonPatchDocument<CarUpdate>>(patchDocument));
        var rentalCarPatchDocument = FilterOperationsForContract(_mapper.Map<JsonPatchDocument<RentalCarUpdate>>(patchDocument));

        var result = await Result.Try(() =>
                    _endpoints.CarsService.AppendPathSegment($"cars/v1/cars/{id}")
                        .AllowAnyHttpStatus().PatchJsonAsync(carPatchDocument),
                error => new Error(error))
            .Ensure(response => response.StatusCode == (int) HttpStatusCode.OK,
                response => response.ToError())
            .OnSuccessTry(response => response.GetJsonAsync<Car>())
            .OnSuccessTry(async car => (car,
                rentalCarResponse: await _endpoints.CarsService.AppendPathSegment($"rentals/v1/cars/{id}")
                    .AllowAnyHttpStatus().PatchJsonAsync(rentalCarPatchDocument)))
            .Ensure(input => input.rentalCarResponse.StatusCode == (int) HttpStatusCode.OK,
                input => input.rentalCarResponse.ToError())
            .OnSuccessTry(async input =>
                input.car.ToCarResponse(await input.rentalCarResponse.GetJsonAsync<RentalCar>()));

        return result.IsSuccess ? Ok(result.Value) : result.Error.ToRedirectHttpResponse();
    }

    private static JsonPatchDocument<T> FilterOperationsForContract<T>(JsonPatchDocument<T> patchDocument)
        where T : class
    {
        var members = typeof(T).GetMembers().Select(x => x.Name.ToLower());
        return new JsonPatchDocument<T>(
            patchDocument.Operations.Where(x => members.Contains(x.path.ToLower())).ToList(),
            new DefaultContractResolver());
    }
}