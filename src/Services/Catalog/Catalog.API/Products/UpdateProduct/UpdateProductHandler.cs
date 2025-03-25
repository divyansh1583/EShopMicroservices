
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Product Product):ICommand<UpdateProductResult>;
    public record UpdateProductResult(Product product);
    public class UpdateProductCommandHandler(IDocumentSession session,ILogger<UpdateProductCommandHandler> logger) : 
        ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler.Handle called with {@Command}",command);
            var product = command.Product;

            session.Update<Product>(product);
            await session.SaveChangesAsync();

            return new UpdateProductResult(product);
        }
    }
}
