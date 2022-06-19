using Dapr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Domain.Entities;
using Orders.Service.Application.Commands;
using Orders.Service.Application.Queries;

namespace Orders.Service.Controllers;


[Route("orders")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<int> CreateOrder(CreateOrderCommand request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet]
    public async Task<Order> GetOrder([FromQuery] GetOrderQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("fulfill")]
    public async Task FulfillOrder(FulfillOrderCommand request)
    {
        await _mediator.Send(request);
    }

    [HttpPost("pay")]
    [Topic("pubsub", "orders/pay")]

    public async Task PayOrder(PayOrderCommand request)
    {
        await _mediator.Send(request);
    }
}