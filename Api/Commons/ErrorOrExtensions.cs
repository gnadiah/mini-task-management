using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace Api.Commons;

public static class ErrorOrExtensions
{
    public static IActionResult ToActionResult<T>(this ErrorOr<T> result, ControllerBase controller)
    {
        if (result.IsError)
            return ProblemFromErrors(result.Errors, controller);

        return controller.Ok(result.Value);
    }

    public static IActionResult ToActionResult<T, TDto>(
        this ErrorOr<T> result, ControllerBase controller, Func<T, TDto> map, int successStatus = StatusCodes.Status200OK)
    {
        if (result.IsError)
            return ProblemFromErrors(result.Errors, controller);

        return controller.StatusCode(successStatus, map(result.Value));
    }

    private static IActionResult ProblemFromErrors(List<Error> errors, ControllerBase controller)
    {
        var first = errors[0];

        var problemDetails = new ProblemDetails
        {
            Type = first.Code,
            Title = first.Metadata?.GetValueOrDefault("title") as string ?? first.Code,
            Status = first.NumericType,
            Detail = first.Description,
            Instance = $"{controller.Request.Method} {controller.Request.Path}"
        };

        problemDetails.Extensions["traceId"] = controller.HttpContext.TraceIdentifier;
        problemDetails.Extensions["errors"] = errors.Select(e => new
        {
            type = e.Code,
            title = e.Metadata?.GetValueOrDefault("title") as string ?? e.Code,
            status = e.NumericType,
            detail = e.Description
        });

        return controller.StatusCode(first.NumericType, problemDetails);
    }
}
