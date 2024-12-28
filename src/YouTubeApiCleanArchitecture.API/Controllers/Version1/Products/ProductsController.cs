using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTubeApiCleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using YouTubeApiCleanArchitecture.Application.Features.Products.Commands.RemoveProduct;
using YouTubeApiCleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using YouTubeApiCleanArchitecture.Application.Features.Products.Queries.GetAllProducts;
using YouTubeApiCleanArchitecture.Application.Features.Products.Queries.GetProduct;
using YouTubeApiCleanArchitecture.Domain.Entities.Products.DTOs;

namespace YouTubeApiCleanArchitecture.API.Controllers.Version1.Products;

[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController(
    ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(
     CreateProductDto request,
     CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new CreateProductCommand(request),
            cancellationToken);
        return CreateResult(response);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProductAsync(
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new GetProductQuery(productId),
            cancellationToken);

        return CreateResult(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new GetAllProductsQuery(),
            cancellationToken);

        return CreateResult(response);
    }

    [HttpPut("{productId}")]
    public async Task<IActionResult> UpdateProductAsync(
        Guid productId,
        UpdateProductDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new UpdateProductCommand(productId, request),
            cancellationToken);

        return CreateResult(response);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> RemoveProductAsync(
       Guid productId,
       CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new RemoveProductCommand(productId),
            cancellationToken);

        return CreateResult(response);
    }
}
