using NSubstitute;
using YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.UpdateCustomer;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.UnitTests.Customers;
using FluentAssertions;

namespace YouTubeApiCleanArchitecture.Application.UnitTests.Customers.Commands;
public class UpdateCustomerCommandHandlerTest
{
    private readonly Customer Customer = CustomerData.CreateQuickCustomer(Guid.NewGuid());

    private readonly IUnitOfWork _unitOfWork;
    private readonly UpdateCustomerCommandHandler _handler;

    public UpdateCustomerCommandHandlerTest()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();

        var customerRepository = Substitute.For<IGenericRepository<Customer>>();

        customerRepository
            .GetByIdAsync(Customer.Id)
            .Returns(Customer);

        _unitOfWork
            .Repository<Customer>()
            .Returns(customerRepository);

        _handler = new UpdateCustomerCommandHandler(_unitOfWork);
    }

    [Fact]
    public async Task Handler_ShouldReturn_Success()
    {
        //Arrange

        var dto = new UpdateCustomerDto
        {
            Title = CustomerData.TitleList[1],
            FirstLineAddress = CustomerData.FirstLineAddressList[0],
            SecondLineLineAddress = CustomerData.SecondLineAddressList[2],
            City = CustomerData.CityList[1],
            Country = CustomerData.Country,
            Postcode = CustomerData.PostcodeList[0]
        };

        //Act

        var result = await _handler.Handle(
            new UpdateCustomerCommand(Customer.Id, dto),
            default);

        //Assert

        result.Should()
           .NotBeNull().And.As<IResult>();

        result.Data.Should()
            .BeNull();

        result.IsNotSuccessfull.Should()
            .BeFalse();

        result.Errors.Should()
            .BeNull();

        result.StatusCode.Should()
            .Be(204);
    }

    [Fact]
    public async Task Handler_NullCustomer_Should_ReturnFailed()
    {
        //Arrange

        var dto = new UpdateCustomerDto
        {
            Title = CustomerData.TitleList[1],
            FirstLineAddress = CustomerData.FirstLineAddressList[0],
            SecondLineLineAddress = CustomerData.SecondLineAddressList[2],
            City = CustomerData.CityList[1],
            Country = CustomerData.Country,
            Postcode = CustomerData.PostcodeList[0]
        };

        //Act

        var result = await _handler.Handle(
            new UpdateCustomerCommand(Guid.NewGuid(), dto),
            default);

        //Assert

        result.Should()
           .NotBeNull().And.As<IResult>();

        result.Data.Should()
            .BeNull();

        result.IsNotSuccessfull.Should()
            .BeTrue();

        result.Errors.Should()
            .NotBeNull();

        result.StatusCode.Should()
            .Be(400);

    }
}
