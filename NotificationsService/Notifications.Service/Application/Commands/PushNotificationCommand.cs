using MediatR;
using Microsoft.AspNetCore.SignalR;
using Notifications.Service.Hubs;

namespace Notifications.Service.Application.Commands;

public class PushNotificationCommand : IRequest
{
    public string Message { get; set; }
    public class PushNotificationCommandHandler : IRequestHandler<PushNotificationCommand>
    {
        private readonly ILogger<PushNotificationCommandHandler> _logger;
        private readonly IHubContext<NotificationHub> _hubContext;

        public PushNotificationCommandHandler(ILogger<PushNotificationCommandHandler> logger, IHubContext<NotificationHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
        public Task<Unit> Handle(PushNotificationCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[PushNotificationCommand] - notification received, message: {request.Message}");
            _hubContext.Clients.All.SendAsync("notification", new
            {
                message = request.Message
            }, cancellationToken).Wait();

            return Task.FromResult(Unit.Value);
        }
    }
}