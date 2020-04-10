﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TokenTOTP.Infra.Swagger.Filters
{
    [ExcludeFromCodeCoverage]
    public class LowerCaseDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();

            foreach (var (key, value) in swaggerDoc.Paths)
            {
                paths.Add(LowercaseEverythingButParameters(key), value);
            }

            swaggerDoc.Paths = paths;
        }

        private static string LowercaseEverythingButParameters(string key) => key.ToLower();
    }
}
