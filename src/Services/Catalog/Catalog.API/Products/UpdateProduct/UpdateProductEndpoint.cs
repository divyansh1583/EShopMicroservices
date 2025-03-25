
namespace Catalog.API.Products.UpdateProduct
{
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products",
                async(Product product,ISender sender) =>
                {
                    var result = await sender.Send(new UpdateProductCommand(product));
                    return Results.Ok(result);
                })
                .WithName("UpdateProduct")
                .WithDescription("UpdateProduct")
                .WithSummary("UpdateProduct")
                .Produces<UpdateProductResult>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
