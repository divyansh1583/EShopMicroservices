
namespace Catalog.API.Products.GetProductsByCategories
{
    public record GetProductByCategoriesQuery(string Categories) : IQuery<GetProductByCategoriesResult>;
    public record GetProductByCategoriesResult(IEnumerable<Product> Products);
    public class GetProductByCategoriesQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByCategoriesQuery, GetProductByCategoriesResult>
    {
        public async Task<GetProductByCategoriesResult> Handle(GetProductByCategoriesQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().Where(prd =>
                prd.Categories.Contains(query.Categories)).ToListAsync(cancellationToken);
            return new GetProductByCategoriesResult(products);
        }
    }
}
