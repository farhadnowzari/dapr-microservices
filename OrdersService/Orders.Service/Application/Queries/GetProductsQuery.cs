using MediatR;
using Orders.Domain;
using Orders.Domain.Entities;

namespace Orders.Service.Application.Queries;

public class GetProductsQuery : IRequest<List<Product>>
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProductsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _dbContext.Products.ToList();
            return Task.FromResult(products);
        }
    }
}