
namespace Basket.API.Basket.GetBasket
{
    public record GetBasketRequest(string UserName);

    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{username}", 
                async (string username, ISender sender) =>
            {   var request = new GetBasketRequest(username);
                var query = request.Adapt<GetBasketQuery>();
                var result = await sender.Send(query);

                return Results.Ok(result);
            });
        }
    }
}
