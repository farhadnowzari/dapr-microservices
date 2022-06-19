using MediatR;
using Payments.Domain;
using Payments.Domain.Entities;

namespace Payments.Service.Application.Commands;

public class CreatePaymentCommand : IRequest
{
    public int ReferenceId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CreatePaymentCommandHandler> _logger;

        public CreatePaymentCommandHandler(ApplicationDbContext dbContext, ILogger<CreatePaymentCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public Task<Unit> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment {
                ReferenceId = request.ReferenceId,
                Price = request.Price * request.Quantity,
            };
            _logger.LogInformation($"[CreatePaymentCommand] - payment creation request with reference '{request.ReferenceId}' received!");
            _dbContext.Payments.Add(payment);

            _dbContext.SaveChanges();

            return Task.FromResult(Unit.Value);
        }
    }
}