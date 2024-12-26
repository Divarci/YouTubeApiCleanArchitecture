using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices;

namespace YouTubeApiCleanArchitecture.Application.Features.Invoices.Commands.UpdateInvoice;
internal sealed class UpdateInvoiceCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateInvoiceCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<NoContentDto>> Handle(
        UpdateInvoiceCommand request,
        CancellationToken cancellationToken)
    {
        var invoice = await _unitOfWork.Repository<Invoice>()
            .GetByIdAsync(request.InvoiceId, cancellationToken);

        if (invoice is null)
            return Result<NoContentDto>
                .Failed(400, "Null.Error", $"The invoice with the id: {request.InvoiceId} not exist");

        invoice.Update(request.Dto);

        _unitOfWork.Repository<Invoice>()
            .Update(invoice);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<NoContentDto>
            .Success(204);
    }
}
