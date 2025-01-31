using FluentAssertions;
using YouTubeApiCleanArchitecture.Application.Features.Customers;
using YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.CreateCustomer;
using YouTubeApiCleanArchitecture.Application.Features.Customers.Queries.GetAllCustomers;
using YouTubeApiCleanArchitecture.Application.IntegrationTests.Infrastructure;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;
using YouTubeApiCleanArchitecture.Domain.UnitTests.Customers;

namespace YouTubeApiCleanArchitecture.Application.IntegrationTests.Customers;
public class CustomerTests : BaseIntegrationTest
{
    public CustomerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CustomerCreate_ShouldReturn_Success()
    {
        //arrange

        var command = new CreateCustomerCommand(
            new CreateCustomerDto()
            {
                Title = CustomerData.TitleList[1],
                City = CustomerData.CityList[1],
                Country = "UK",
                FirstLineAddress = CustomerData.FirstLineAddressList[1],
                Postcode = CustomerData.PostcodeList[1],
                SecondLineLineAddress = CustomerData.SecondLineAddressList[1]
            });

        //act

        var result = await Sender.Send(command);

        //assert

        result.Should()
            .NotBeNull().And.As<IResult>();

        result.Data.Should()
            .NotBeNull().And.BeOfType<CustomerResponse>();

        result.IsNotSuccessfull.Should()
            .BeFalse();

        result.Errors.Should()
            .BeNull();

        result.StatusCode.Should()
            .Be(201);

        result.Data!.Title.Should()
            .Be(command.Dto.Title);

        result.Data!.Address.FirstLineAddress.Should()
            .Be(command.Dto.FirstLineAddress);

        result.Data!.Address.SecondLineAddress.Should()
            .Be(command.Dto.SecondLineLineAddress);

        result.Data!.Address.Postcode.Should()
            .Be(command.Dto.Postcode);

        result.Data!.Address.City.Should()
            .Be(command.Dto.City);

        result.Data!.Address.Country.Should()
            .Be(command.Dto.Country);
    }
}
