using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Infrastructure.FunctionalExtensions;

public static class UnitResultExtensions
{
    public static ObjectResult ToHttpResponse(this UnitResult<Error> unitResult)
    {
        return new ObjectResult(new {ErrorMessage = unitResult.Error.Message})
        {
            StatusCode = (int) unitResult.Error.HttpStatusCode
        };
    }

    public static ObjectResult ToHttpResponse(this Error error)
    {
        return new ObjectResult(new {ErrorMessage = error.Message})
        {
            StatusCode = (int) error.HttpStatusCode
        };
    }
    
    public static ObjectResult ToRedirectHttpResponse(this Error error)
    {
        return new ObjectResult(error.Message)
        {
            StatusCode = (int) error.HttpStatusCode
        };
    }
}
