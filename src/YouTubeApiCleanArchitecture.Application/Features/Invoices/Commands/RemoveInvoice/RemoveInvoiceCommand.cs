using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices.Commands.RemoveInvoice;
public record RemoveInvoiceCommand(
    Guid InvoiceId) : ICommand;