using AutoMapper;
using FluentAssertions;
using NSubstitute;
using YouTubeApiCleanArchitecture.Application.Features.Customers;
using YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.CreateCustomer;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;

namespace YouTubeApiCleanArchitecture.Application.UnitTests.Customers.Commands;
public class CreateCustomerCommandHandlerTest
{
    private static readonly CreateCustomerCommand Command = new(
       new CreateCustomerDto
       {
           Title = "Painting Company Ltd",
           FirstLineAddress = "452 Garden road",
           City = "London",
           Postcode = "WM12 5PL",
           Country = "UK"
       });

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private readonly CreateCustomerCommandHandler _handler;

    public CreateCustomerCommandHandlerTest()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();

        var customerRepository = Substitute.For<IGenericRepository<Customer>>();

        _unitOfWork
            .Repository<Customer>()
            .Returns(customerRepository);

        _mapper = Substitute.For<IMapper>();

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

        _handler = new CreateCustomerCommandHandler(_unitOfWork, _mapper);
    }

    [Fact]
    public async Task Handler_ShouldHandleSuccess()
    {
        //act

        var result = await _handler.Handle(Command, default);

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
            .Be(Command.Dto.Title);

        result.Data!.Address.FirstLineAddress.Should()
            .Be(Command.Dto.FirstLineAddress);

        result.Data!.Address.SecondLineAddress.Should()
            .Be(Command.Dto.SecondLineLineAddress);

        result.Data!.Address.Postcode.Should()
            .Be(Command.Dto.Postcode);

        result.Data!.Address.City.Should()
            .Be(Command.Dto.City);

        result.Data!.Address.Country.Should()
            .Be(Command.Dto.Country);
    }
}
