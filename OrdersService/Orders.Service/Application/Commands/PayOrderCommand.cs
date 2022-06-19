using MediatR;
using Orders.Domain;
using Orders.Domain.Enums;

namespace Orders.Service.Application.Commands;

public class PayOrderCommand : IRequest
{
    public int OrderId { get; set; }
    public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PayOrderCommandHandler> _logger;

        public PayOrderCommandHandler(ApplicationDbContext dbContext, ILogger<PayOrderCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public Task<Unit> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[PayOrderCommand] - Order paid!");
            var order = _dbContext.Orders.First(x => x.Id == request.OrderId);

            order.Status = OrderStatus.Paid;

            _dbContext.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}