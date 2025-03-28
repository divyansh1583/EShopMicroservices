
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Product Product):ICommand<UpdateProductResult>;
    public record UpdateProductResult(Product product);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Product).NotNull().WithMessage("Product is required");
            RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Product.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
            RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
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
