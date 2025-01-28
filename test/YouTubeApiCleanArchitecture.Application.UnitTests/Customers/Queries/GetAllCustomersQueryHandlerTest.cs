using AutoMapper;
using FluentAssertions;
using NSubstitute;
using YouTubeApiCleanArchitecture.Application.Features.Customers;
using YouTubeApiCleanArchitecture.Application.Features.Customers.Queries.GetAllCustomers;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.ValueObject;
using YouTubeApiCleanArchitecture.Domain.UnitTests.Customers;

namespace YouTubeApiCleanArchitecture.Application.UnitTests.Customers.Queries;
public class GetAllCustomersQueryHandlerTest
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly GetAllCustomersQueryHandler _handler;
    private readonly IMapper _mapper;

    public GetAllCustomersQueryHandlerTest()
    {
        var customerOne = new CustomerResponse
        {
            Id = Guid.NewGuid(),
            Address = new Address(
                CustomerData.FirstLineAddressList[0],
                CustomerData.SecondLineAddressList[0],
                CustomerData.PostcodeList[1],
                CustomerData.CityList[2],
                "UK"),
            Balance = 55,
            Title = "Customer One"
        };

        var customerTwo = new CustomerResponse
        {
            Id = Guid.NewGuid(),
            Address = new Address(
                CustomerData.FirstLineAddressList[2],
                CustomerData.SecondLineAddressList[1],
                CustomerData.PostcodeList[2],
                CustomerData.CityList[0],
                "UK"),
            Balance = 35,
            Title = "Customer Two"
        };

        _mapper = Substitute.For<IMapper>();

        _unitOfWork = Substitute.For<IUnitOfWork>();

        var customerRepository = Substitute.For<IGenericRepository<Customer>>();

        _unitOfWork
            .Repository<Customer>()
            .Returns(customerRepository);

        customerRepository
            .GetAllAsync<CustomerResponse>(
                mapper: _mapper,
                cancellationToken: Arg.Any<CancellationToken>(),
                enableTracking: false)
            .Returns([customerOne, customerTwo]);

        _handler = new GetAllCustomersQueryHandler(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handler_ShouldReturn_Success()
    {
        //arrange

        var query = new GetAllCustomersQuery();

        //act

        var result = await _handler.Handle(query, default);

        //assert

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
    }


}
