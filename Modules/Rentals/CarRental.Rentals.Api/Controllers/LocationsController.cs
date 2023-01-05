using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Infrastructure.FunctionalExtensions;
using CarRental.Rentals.Api.Controllers.Responses;
using CarRental.Rentals.Api.Extensions;
using CarRental.Rentals.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Rentals.Api.Controllers;

[ApiController]
[Route("rentals/v1/locations")]
public sealed class LocationsController : ControllerBase
{
    private readonly ILocationsService _service;

    public LocationsController(ILocationsService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(LocationResponse[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetLocations();
        return result.IsSuccess ? Ok(result.Value.ToLocationResponses()) : Ok(new List<LocationResponse>());
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(LocationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _service.GetLocation(id);
        return result.IsSuccess ? Ok(result.Value.ToLocationResponse()) : result.Error.ToHttpResponse();
    }
}