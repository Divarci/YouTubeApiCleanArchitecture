﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Products;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.RemoveCustomer;
internal sealed class RemoveCustomerCommandHandler(
    IUnitOfWork unitOfWork) : ICommandHandler<RemoveCustomerCommand>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<NoContentDto>> Handle(
        RemoveCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        var customer = await _unitOfWork.Repository<Customer>()
          .GetAll()
          .Include(x => x.Invoices)
          .FirstOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);

        if (customer is null)
            return Result<NoContentDto>
                .Failed(400, "Null.Error", $"The customer with the id: {request.CustomerId} not exist");

        if(customer.Invoices.Count > 0)
            return Result<NoContentDto>
                .Failed(400, "Invalid.Error", $"The customer with the id: {request.CustomerId} has invoices.");

        _unitOfWork.Repository<Customer>()
            .Delete(customer);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<NoContentDto>
            .Success(204);
    }
}
