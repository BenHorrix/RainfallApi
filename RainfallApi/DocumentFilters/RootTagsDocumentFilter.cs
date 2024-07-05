using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RainfallApi.DocumentFilters
{
    public class RootTagsDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var rainfallTagName = "RainfallController";
            var hasRainfallTag = swaggerDoc.Tags.Any(t => t.Name == rainfallTagName);
            if (!hasRainfallTag)
            {
                swaggerDoc.Tags.Add(new OpenApiTag() { Name = rainfallTagName, Description = "Operations relating to rainfall" });
            }
        }
    }
}
