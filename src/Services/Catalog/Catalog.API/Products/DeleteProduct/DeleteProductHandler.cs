﻿
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool Success);

    public class DeleteProductCommandValidatior : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidatior()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Id is required");
        }
    }
    public class DeleteProductCommandHandler(IDocumentSession session)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id);
            if (product is null)
            {
                throw new ProductNotFoundException(command.Id);
            }
            session.Delete<Product>(product);
            await session.SaveChangesAsync();
            return new DeleteProductResult(true);

        }
    }
}
