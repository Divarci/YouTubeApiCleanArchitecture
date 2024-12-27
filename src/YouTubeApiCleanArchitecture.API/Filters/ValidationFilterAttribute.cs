using Microsoft.AspNetCore.Mvc.Filters;
using YouTubeApiCleanArchitecture.Domain.Exceptions;

namespace YouTubeApiCleanArchitecture.API.Filters;

public class ValidationFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage)
                .ToList();

            if (errors.Any(x => x.Contains("request")))            
                throw new PayloadFormatException(errors);
            
            else            
                throw new RequestValidationException(errors);
            
        }
    }
}