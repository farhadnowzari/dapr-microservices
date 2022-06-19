using MediatR;
using Orders.Domain;
using Orders.Domain.Entities;

namespace Orders.Service.Application.Commands;

public class CreateProductCommand : IRequest
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateProductCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Quantity = request.Quantity,
                UnitPrice = request.Price
            };
            _dbContext.Products.Add(product);

            _dbContext.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}