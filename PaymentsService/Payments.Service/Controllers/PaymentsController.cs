using Dapr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Payments.Service.Application.Commands;

namespace Payments.Service.Controllers;

[ApiController]
[Route("payments")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Topic("pubsub", "payments/process")]
    public async Task CreatePayment(CreatePaymentCommand request)
    {
        await _mediator.Send(request);
    }

    [HttpPut]
    public async Task UpdatePaymentStatus(UpdatePaymentStatusCommand request)
    {
        await _mediator.Send(request);
    }
}