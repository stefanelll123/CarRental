using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarRental.Cars.Api.Controllers.Responses;
using CarRental.Cars.Api.Extensions;
using CarRental.Cars.Application.Services;
using CarRental.Infrastructure.FunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Cars.Api.Controllers;

[ApiController]
[Route("cars/v1/models")]
public sealed class ModelsController : ControllerBase
{
    private readonly IModelsService _service;

    public ModelsController(IModelsService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ModelResponse[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var result = await _service.GetModels();
        return result.IsSuccess ? Ok(result.Value.ToModelResponses()) : Ok(new List<ModelResponse>());
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ModelResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _service.GetModel(id);
        return result.IsSuccess ? Ok(result.Value.ToModelResponse()) : result.Error.ToHttpResponse();
    }
}