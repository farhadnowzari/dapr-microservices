using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Domain.Entities;
using Orders.Service.Application.Commands;
using Orders.Service.Application.Queries;

namespace Orders.Service.Controllers;

[Route("products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<List<Product>> GetProducts([FromQuery] GetProductsQuery request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    public async Task CreateProduct(CreateProductCommand request)
    {
        await _mediator.Send(request);
    }
}