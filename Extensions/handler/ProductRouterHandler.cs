using jest.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Extensions;
public static class ProductExtension
{
    private const string ApiEndPoint = "/api/products";
    public static void ProductEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet(ApiEndPoint, () =>
        {
            return "Hello";

        });

        app.MapGet("/api/products/{id:Guid}", (Guid id) =>
        {
        });
        app.MapPut("/api/products/", ([FromBody] Product product) =>
       {
       });
        app.MapPost("/api/products/", ([FromBody] Product product) =>
        {
        });
        app.MapDelete("/api/products/{id:Guid}", (Guid id) =>
       {
       });


    }
}