using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CarRental.Infrastructure.FunctionalExtensions;
using CarRental.Rentals.Api.Controllers.Requests;
using CarRental.Rentals.Api.Controllers.Responses;
using CarRental.Rentals.Api.Extensions;
using CarRental.Rentals.Application.Services;
using CarRental.Rentals.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Rentals.Api.Controllers;

[ApiController]
[Route("rentals/v1/cars")]
public sealed class RentalCarsController : ControllerBase
{
    private readonly ICarsService _service;
    private readonly IMapper _mapper;

    public RentalCarsController(ICarsService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(RentalCarResponse[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetCars();
        return result.IsSuccess ? Ok(result.Value.ToCarResponses()) : Ok(new List<RentalCarResponse>());
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RentalCarResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _service.GetCar(id);
        return result.IsSuccess ? 
            Ok(result.Value.ToCarResponse())
            : result.Error.ToHttpResponse();
    }
    
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(Guid id, [FromBody] JsonPatchDocument<RentalCarUpdateRequest> patchDocument)
    {
        var carPatchDocument = _mapper.Map<JsonPatchDocument<Car>>(patchDocument);
        var result = await _service.UpdateCar(id, carPatchDocument);

        return result.IsSuccess
            ? Ok(result.Value)
            : result.Error.ToHttpResponse();
    }
}