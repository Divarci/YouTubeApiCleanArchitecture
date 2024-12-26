using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.DTOs;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices.Commands.UpdateInvoice;
public record UpdateInvoiceCommand(
    Guid InvoiceId,
    UpdateInvoiceDto Dto) : ICommand;
