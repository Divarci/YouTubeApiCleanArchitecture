using AutoMapper;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Products;

namespace YouTubeApiCleanArchitecture.Application.Features.Products.Queries.GetProduct;
internal sealed class GetProductQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IQueryHandler<GetProductQuery, ProductResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<ProductResponse>> Handle(
        GetProductQuery request,
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Product>()
            .GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
            return Result<ProductResponse>
                .Failed(400, "Null.Error", $"The prouct with the id: {request.ProductId} not exist");

        var reponse = _mapper.Map<ProductResponse>(product);

        return Result<ProductResponse>
            .Success(reponse, 200);
    }
}
