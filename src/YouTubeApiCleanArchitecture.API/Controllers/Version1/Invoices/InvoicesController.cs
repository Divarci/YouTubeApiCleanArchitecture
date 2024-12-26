using MediatR;
using Microsoft.AspNetCore.Mvc;
using YouTubeApiCleanArchitecture.Application.Features.Invoices.Commands.CreateInvoice;
using YouTubeApiCleanArchitecture.Application.Features.Invoices.Commands.RemoveInvoice;
using YouTubeApiCleanArchitecture.Application.Features.Invoices.Commands.UpdateInvoice;
using YouTubeApiCleanArchitecture.Application.Features.Invoices.Queries.GetAllInvoices;
using YouTubeApiCleanArchitecture.Application.Features.Invoices.Queries.GetInvoice;
using YouTubeApiCleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using YouTubeApiCleanArchitecture.Application.Features.Products.Commands.RemoveProduct;
using YouTubeApiCleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using YouTubeApiCleanArchitecture.Application.Features.Products.Queries.GetAllProducts;
using YouTubeApiCleanArchitecture.Application.Features.Products.Queries.GetProduct;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Products.DTOs;

namespace YouTubeApiCleanArchitecture.API.Controllers.Version1.Invoices;
[Route("api/[controller]")]
[ApiController]
public class InvoicesController(ISender sender) : BaseController
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateInvoiceAsync(
    CreateInvoiceDto request,
    CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new CreateInvoiceCommand(request),
            cancellationToken);
        return CreateResult(response);
    }

    [HttpGet("{invoiceId}")]
    public async Task<IActionResult> GetInvoiceAsync(
        Guid invoiceId,
        CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new GetInvoiceQuery(invoiceId),
            cancellationToken);

        return CreateResult(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInvoiceAsync(
        CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new GetAllInvoicesQuery(),
            cancellationToken);

        return CreateResult(response);
    }

    [HttpPut("{invoiceId}")]
    public async Task<IActionResult> UpdateInvoiceAsync(
        Guid invoiceId,
        UpdateInvoiceDto request,
        CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new UpdateInvoiceCommand(invoiceId, request),
            cancellationToken);

        return CreateResult(response);
    }

    [HttpDelete("{invoiceId}")]
    public async Task<IActionResult> RemoveInvoiceAsync(
       Guid invoiceId,
       CancellationToken cancellationToken = default)
    {
        var response = await _sender.Send(
            new RemoveInvoiceCommand(invoiceId),
            cancellationToken);

        return CreateResult(response);
    }
}
