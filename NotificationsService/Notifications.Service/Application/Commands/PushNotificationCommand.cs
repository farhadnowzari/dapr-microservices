using MediatR;

namespace Notifications.Service.Application.Commands;

public class PushNotificationCommand : IRequest
{
    public string Message { get; set; }
    public class PushNotificationCommandHandler : IRequestHandler<PushNotificationCommand>
    {
        private readonly ILogger<PushNotificationCommandHandler> _logger;

        public PushNotificationCommandHandler(ILogger<PushNotificationCommandHandler> logger)
        {
            _logger = logger;
        }
        public Task<Unit> Handle(PushNotificationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[PushNotificationCommand] - notification received, message: {request.Message}");
            throw new NotImplementedException();
        }
    }
}