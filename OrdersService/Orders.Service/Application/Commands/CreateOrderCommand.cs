using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orders.Domain;
using Orders.Domain.Entities;

namespace Orders.Service.Application.Commands;

public class CreateOrderCommand : IRequest<int>
{
    public int ProductId { get; set; }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly ApplicationDbContext _dbContext;

        public CreateOrderCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                var product = _dbContext.Products.First(x => x.Id == request.ProductId);
                if (product.Quantity == 0)
                {
                    throw new Exception("The product quantity is 0");
                }
                var order = new Order
                {
                    ProductId = product.Id,
                    Quantity = 1,
                    UnitPrice = product.UnitPrice
                };
                product.Quantity -= 1;
                _dbContext.Orders.Add(order);

                _dbContext.SaveChanges();

                transaction.Commit();

                return Task.FromResult(order.Id);
            }
        }
    }
}