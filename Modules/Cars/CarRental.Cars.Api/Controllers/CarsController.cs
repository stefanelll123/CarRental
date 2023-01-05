using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Cars.Api.Controllers.Requests;
using CarRental.Cars.Api.Controllers.Responses;
using CarRental.Cars.Api.Extensions;
using CarRental.Cars.Application.Services;
using CarRental.Cars.Domain;
using CarRental.Infrastructure.FunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Cars.Api.Controllers;

[ApiController]
[Route("cars/v1/cars")]
public sealed class CarsController : ControllerBase
{
    private readonly ICarsService _service;
    private readonly IMapper _mapper;

    public CarsController(ICarsService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CarResponse[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetCars();
        return result.IsSuccess ? Ok(result.Value.ToCarResponses()) : Ok(new List<CarResponse>());
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CarResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _service.GetCar(id);
        return result.IsSuccess ? 
            Ok(result.Value.ToCarResponse())
            : result.Error.ToHttpResponse();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CarRequest car)
    {
        var result = await _service.AddCar(car.ToCreateCarDto());
        return result.IsSuccess
            ? Created($"{Request.Path.Value}/{result.Value}", null)
            : result.Error.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Remove(Guid id)
    {
        var result = await _service.RemoveCar(id);
        return result.IsSuccess
            ? NoContent()
            : result.Error.ToHttpResponse();
    }
    
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(Guid id, [FromBody] JsonPatchDocument<CarUpdateRequest> patchDocument)
    {
        // TODO: implement a custom mechanism for json patch,in order to validate the operations
        // and to ensure that invalid operations as update id are not allowed
        var carPatchDocument = _mapper.Map<JsonPatchDocument<Car>>(patchDocument);
        var result = await _service.UpdateCar(id, carPatchDocument);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.Error.ToHttpResponse();
    }
}