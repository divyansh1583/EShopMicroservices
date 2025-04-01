
namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketRequest(string UserName);
    public record DeleteBasketResponse(bool IsDeleted);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{username}",
                async (string username, ISender sender) =>
                {
                    var request = new DeleteBasketRequest(username);
                    var command = request.Adapt<DeleteBasketCommand>();
                    var result = await sender.Send(command);
                    var response = result.Adapt<DeleteBasketResponse>();
                    return Results.Ok(response);
                })
                .WithName("DeleteBasket")
                .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete Basket")
                .WithDescription("Delete Basket");
        }
    }
}
