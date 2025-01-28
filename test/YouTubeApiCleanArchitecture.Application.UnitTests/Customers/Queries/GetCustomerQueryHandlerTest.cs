using AutoMapper;
using FluentAssertions;
using NSubstitute;
using YouTubeApiCleanArchitecture.Application.Features.Customers;
using YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.RemoveCustomer;
using YouTubeApiCleanArchitecture.Application.Features.Customers.Queries.GetCustomer;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.UnitTests.Invoices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace YouTubeApiCleanArchitecture.Application.UnitTests.Customers.Queries;
public class GetCustomerQueryHandlerTest
{
    private static readonly Customer Customer = InvoiceData.Customer;

    private readonly GetCustomerQueryHandler _handler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerQueryHandlerTest()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();

        _mapper = Substitute.For<IMapper>();

        var customerRepository = Substitute.For<IGenericRepository<Customer>>();

        _unitOfWork
            .Repository<Customer>()
            .Returns(customerRepository);

        customerRepository
            .GetByIdAsync(Customer.Id, Arg.Any<CancellationToken>())
            .Returns(Customer);

        _mapper
            .Map<CustomerResponse>(Arg.Any<Customer>())
            .Returns(callInfo =>
            {
                var input = callInfo.Arg<Customer>();
                return new CustomerResponse
                {
                    Id = input.Id,
                    Title = input.Title.Value,
                    Address = input.Address,
                    Balance = input.Balance.Value
                };
            });

        _handler = new GetCustomerQueryHandler(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handler_ShouldHandleSuccess()
    {
        //Arrange

        var query = new GetCustomerQuery(Customer.Id);

        //Act

        var result = await _handler.Handle(query, default);

        //Asert

        result.Should()
            .NotBeNull().And.As<IResult>();

        result.Data.Should()
            .NotBeNull();

        result.IsNotSuccessfull.Should()
            .BeFalse();

        result.Errors.Should()
            .BeNull();

        result.StatusCode.Should()
            .Be(200);

        result.Data!.Title.Should()
           .Be(Customer.Title.Value);

        result.Data!.Address.FirstLineAddress.Should()
            .Be(Customer.Address.FirstLineAddress);

        result.Data!.Address.SecondLineAddress.Should()
            .Be(Customer.Address.SecondLineAddress);

        result.Data!.Address.Postcode.Should()
            .Be(Customer.Address.Postcode);

        result.Data!.Address.City.Should()
            .Be(Customer.Address.City);

        result.Data!.Address.Country.Should()
            .Be(Customer.Address.Country);

        result.Data!.Balance.Should()
            .Be(Customer.Balance.Value);
    }
}
