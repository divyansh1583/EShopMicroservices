
namespace Catalog.API.Products.GetProductsByCategories
{
    public class GetProductByCategoriesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/Categories/{Categories}",
                async (string Categories, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductByCategoriesQuery(Categories));
                    return Results.Ok(result);
                });
        }
    }
}
