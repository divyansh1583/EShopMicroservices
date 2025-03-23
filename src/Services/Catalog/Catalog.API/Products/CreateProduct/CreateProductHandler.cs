using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(
        string Name, 
        List<string> Category, 
        string Description, 
        string ImageFile, decimal Price
     ) :ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create product entity
            var product = new Product
            {
                Name = command.Name,
                Categories = command.Category,
                Description = command.Description,
                Price = command.Price
            };
            
            //save it in the database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //return result     
            return new CreateProductResult(product.Id); 
        }
    }
}
