using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Swashbuckle.FlattenAllOf.Extension
{
    public static class SwaggerOptionsExtensions
    {
        public static SwaggerOptions FlattenAllOfs(this SwaggerOptions options)
        {
            options
                .PreSerializeFilters
                .Add
                (
                    (swagger, httpReq) =>
                    {
                        foreach (var schema in swagger.Components.Schemas)
                        {
                            var flatReferences = new Dictionary<string, OpenApiSchema>();
                            
                            foreach (var property in schema.Value.Properties)
                            {
                                if (property.Value.AllOf?.Count == 1)
                                {
                                    var newSchema = new OpenApiSchema
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.Schema, 
                                            Id = property.Value.AllOf.First().Reference.Id
                                        }
                                    };

                                    flatReferences.Add(property.Key, newSchema);
                                }
                                else
                                {
                                    flatReferences.Add(property.Key, property.Value);
                                }
                            }

                            schema.Value.Properties = flatReferences;
                        }
                    }
                );

            return options;
        }
    }
}
