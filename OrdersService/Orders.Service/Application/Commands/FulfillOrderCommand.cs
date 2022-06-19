using Dapr.Client;
using MediatR;
using Orders.Domain;
using Orders.Domain.Enums;

namespace Orders.Service.Application.Commands;

public class FulfillOrderCommand : IRequest
{
    public int Id { get; set; }
    public class FulfillOrderCommandHandler : IRequestHandler<FulfillOrderCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DaprClient _daprClient;
        private readonly ILogger<FulfillOrderCommandHandler> _logger;

        public FulfillOrderCommandHandler(ApplicationDbContext dbContext, DaprClient daprClient, ILogger<FulfillOrderCommandHandler> logger)
        {
            _dbContext = dbContext;
            _daprClient = daprClient;
            _logger = logger;
        }
        public Task<Unit> Handle(FulfillOrderCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var order = _dbContext.Orders.First(x => x.Id == request.Id);
                order.Status = OrderStatus.Fulfilled;

                _dbContext.SaveChanges();
                _daprClient.PublishEventAsync("pubsub", "payments/process", new
                {
                    Quantity = order.Quantity,
                    Price = order.UnitPrice,
                    ReferenceId = order.Id
                }).Wait();
                _logger.LogInformation($"[FulfillOrderCommand] - payments/process published for order '{order.Id}'...");

                transaction.Commit();
            }
            return Task.FromResult(Unit.Value);
        }
    }
}