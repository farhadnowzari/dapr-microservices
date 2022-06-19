using Dapr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notifications.Service.Application.Commands;

namespace Notifications.Service.Controllers;

[Route("dapr/notifications")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
}

    [HttpPost]
    [Topic("pubsub", "notify")]
    public async Task PushNotification(PushNotificationCommand request)
    {
        await _mediator.Send(request);
    }
}