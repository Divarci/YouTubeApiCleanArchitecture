using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.DTOs;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices.Commands.CreateInvoice;
public record CreateInvoiceCommand(
    CreateInvoiceDto Dto): ICommand<InvoiceResponse>;