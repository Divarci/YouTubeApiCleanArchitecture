using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices.Queries.GetInvoice;
public record GetInvoiceQuery(
    Guid InvoiceId) : IQuery<InvoiceResponse>;
