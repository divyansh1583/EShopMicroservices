
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", 
              async  (Guid id,ISender sender) =>
            {
                var result= await sender.Send(new DeleteProductCommand(id));
                return Results.Ok(result);
            })
            .WithName("")
            .WithDescription("")
            .WithSummary("")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
