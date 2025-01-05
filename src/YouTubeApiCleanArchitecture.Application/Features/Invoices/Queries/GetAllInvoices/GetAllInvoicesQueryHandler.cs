using AutoMapper;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices.Queries.GetAllInvoices;
internal sealed class GetAllInvoicesQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IQueryHandler<GetAllInvoicesQuery, InvoiceResponseCollection>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<InvoiceResponseCollection>> Handle(
        GetAllInvoicesQuery request,
        CancellationToken cancellationToken)
    {
        var invoices = await _unitOfWork.Repository<Invoice>()
            .GetAllAsync<InvoiceResponse>(
                enableTracking: false,
                mapper: _mapper,
                cancellationToken: cancellationToken);

        var response = new InvoiceResponseCollection
        {
            Invoices = invoices.AsReadOnly()
        };

        return Result<InvoiceResponseCollection>
            .Success(response, 200);
    }
}
