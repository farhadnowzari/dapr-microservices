using Dapr.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orders.Domain;
using Orders.Domain.Entities;

namespace Orders.Service.Application.Queries;

public class GetOrderQuery : IRequest<Order>
{
    public int Id { get; set; }
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly ApplicationDbContext _dbContext;
        public GetOrderQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = _dbContext.Orders
                .Include(x => x.Product)
                .FirstOrDefault(x => x.Id == request.Id);
            
            var product = _dbContext.Products
                .FirstOrDefault(x => x.Id == order.ProductId);

            order.Product = product;

            return Task.FromResult(order);
        }
    }
}