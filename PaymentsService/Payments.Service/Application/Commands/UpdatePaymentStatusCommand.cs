using Dapr.Client;
using MediatR;
using Payments.Domain;
using Payments.Domain.Enums;

namespace Payments.Service.Application.Commands;

public class UpdatePaymentStatusCommand : IRequest
{
    public PaymentStatus Status { get; set; }
    public int Id { get; set; }
    public class UpdatePaymentStatusCommandHandler : IRequestHandler<UpdatePaymentStatusCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DaprClient _daprClient;
        private readonly ILogger<UpdatePaymentStatusCommandHandler> _logger;

        public UpdatePaymentStatusCommandHandler(ApplicationDbContext dbContext, DaprClient daprClient, ILogger<UpdatePaymentStatusCommandHandler> logger)
        {
            _dbContext = dbContext;
            _daprClient = daprClient;
            _logger = logger;
        }
        public Task<Unit> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var payment = _dbContext.Payments.First(x => x.Id == request.Id);

            if (payment.Status != PaymentStatus.Pending)
            {
                throw new Exception("Only the status of a pending payment can get updated!");
            }

            payment.Status = request.Status;

            _daprClient.PublishEventAsync("pubsub", "notify", new
            {
                Message = $"Payment status changed: {request.Status}. Order: {payment.ReferenceId}"
            }).Wait();

            _logger.LogInformation("[UpdatePaymentStatusCommand] - Published notification event");

            if (request.Status == PaymentStatus.Paid)
            {
                _daprClient.PublishEventAsync("pubsub", "orders/pay", new
                {
                    OrderId = payment.ReferenceId
                }).Wait();
            }

            _logger.LogInformation("[UpdatePaymentStatusCommand] - Published orders/pay event");

            _dbContext.SaveChanges();
            return Task.FromResult(Unit.Value);
        }
    }
}