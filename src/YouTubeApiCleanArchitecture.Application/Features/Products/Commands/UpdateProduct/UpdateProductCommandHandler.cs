﻿using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Products;

namespace YouTubeApiCleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
internal sealed class UpdateProductCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateProductCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<NoContentDto>> Handle(
        UpdateProductCommand request, 
        CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Repository<Product>()
            .GetByIdAsync(request.Dto.ProductId, cancellationToken);

        if (product is null)
            return Result<NoContentDto>
                .Failed(400, "Null.Error", $"The prouct with the id: {request.Dto.ProductId} not exist");

        product.Update(request.Dto);

        _unitOfWork.Repository<Product>()
            .Update(product);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<NoContentDto>
            .Success(204);
    }
}
