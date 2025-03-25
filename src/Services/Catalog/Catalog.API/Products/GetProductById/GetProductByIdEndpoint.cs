public record GetProductByIdResponse(Product Product);

namespace Catalog.API.Products.GetProductById
{
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}",
                async (Guid id,ISender sender) =>
                {
                    var result =  await sender.Send(new GetProductByIdQuery(id));
                    var response = result.Adapt<GetProductByIdResponse>();
                    return Results.Ok(result);
                })
                .WithName("GetProductsById")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetProductsById")
                .WithDescription("GetProductsById");
        }
    }
}
