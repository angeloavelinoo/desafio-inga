﻿using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DesafioIngaCodeApi.Extensions
{
    public static class SwaggerConfig
    {
        public static void AddServiceSwagger(this IServiceCollection services)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
                c.IncludeXmlComments(xmlPath);
                c.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JTW Authorization header usando Bearer.
                                    Entre com 'Bearer' [Espaço] entao coloque seu Token.
                                    Exemplo: 'Bearer 123456abcdefg'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
