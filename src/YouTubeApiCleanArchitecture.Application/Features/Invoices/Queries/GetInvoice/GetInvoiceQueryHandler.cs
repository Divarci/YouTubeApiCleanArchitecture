using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices.Queries.GetInvoice;
internal sealed class GetInvoiceQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IQueryHandler<GetInvoiceQuery, InvoiceResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<InvoiceResponse>> Handle(
        GetInvoiceQuery request,
        CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Repository<Invoice>()
            .GetAsync(
                enableTracking: false,
                predicates: [x => x.Id == request.InvoiceId],
                include: [x => x.Include(x => x.PurchasedProducts)],
                cancellationToken: cancellationToken);
            
        var response = _mapper.Map<InvoiceResponse>(invoice);             

        if (response == null)
            return Result<InvoiceResponse>
                .Failed(400, "Null.Error", $"The invoice with the id: {request.InvoiceId} not exist");

        return Result<InvoiceResponse>
            .Success(response, 200);
    }
}
