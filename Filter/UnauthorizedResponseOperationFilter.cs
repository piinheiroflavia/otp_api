using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

public class UnauthorizedResponseOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Responses.ContainsKey("401"))
        {
            operation.Responses["401"].Description = "Unauthorized. The request is missing a valid authentication token.";
        }
        else
        {
            operation.Responses.Add("401", new OpenApiResponse
            {
                Description = "Unauthorized. The request is missing a valid authentication token."
            });
        }
    }
}
