using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query;
using NSubstitute;
using System.Linq.Expressions;
using YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.RemoveCustomer;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers;
using YouTubeApiCleanArchitecture.Domain.Entities.Products;
using YouTubeApiCleanArchitecture.Domain.UnitTests.Customers;
using YouTubeApiCleanArchitecture.Domain.UnitTests.Invoices;
using YouTubeApiCleanArchitecture.Domain.UnitTests.Products;

namespace YouTubeApiCleanArchitecture.Application.UnitTests.Customers.Commands;
public class RemoveCustomerCommandHandlerTest
{
    private static readonly Customer Customer = InvoiceData.Customer;

    private readonly RemoveCustomerCommandHandler _handler;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCustomerCommandHandlerTest()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();

        var productRepository = Substitute.For<IGenericRepository<Product>>();

        var products = ProductData.Catalogue;

        foreach (var product in products)
        {
            productRepository
                .GetByIdAsync(product.Id, Arg.Any<CancellationToken>())
                .Returns(product);
        }

        _unitOfWork
            .Repository<Product>()
            .Returns(productRepository);

        var customerRepository = Substitute.For<IGenericRepository<Customer>>();

        _unitOfWork
            .Repository<Customer>()
            .Returns(customerRepository);

        _unitOfWork
            .Repository<Customer>()
            .GetAsync(
                predicates: Arg.Is<Expression<Func<Customer, bool>>[]?>(x => 
                    x != null && 
                    x[0].Compile().Invoke(Customer) == true),
                includes: Arg.Any<Func<IQueryable<Customer>, IIncludableQueryable<Customer, object>>[]?>(),
                cancellationToken: Arg.Any<CancellationToken>(),
                enableTracking: false)
            .Returns(Customer);

        _unitOfWork
            .Repository<Customer>()
            .GetByIdAsync(Customer.Id, Arg.Any<CancellationToken>())
            .Returns(Customer);

        _handler = new RemoveCustomerCommandHandler(_unitOfWork);        
    }

    [Fact]
    public async Task Handler_ShouldHandleSuccess()
    {
        //Arrange

        var command = new RemoveCustomerCommand(Customer.Id);

        //Act

        var result = await _handler.Handle(command, default);

        //Asert

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
    public async Task Handler_ShouldReturnFailed_IfCustomerHasInvoice()
    {
        //Arrange   

        var customer = CustomerData
            .CreateQuickCustomer(Guid.NewGuid());

        var command = new RemoveCustomerCommand(customer.Id);

        var invoice = await InvoiceData.CreateInvoice(
            customer.Id,
            InvoiceData.InvoiceId,
            InvoiceData.ValidPurchasedProducts,
            _unitOfWork);

        customer.Invoices.Add(invoice);

        //Act

        var result = await _handler.Handle(command, default);

        //Asert

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

    [Fact]
    public async Task Handler_ShouldReturnFailed_WithNullCustomer()
    {
        //Arrange

        var command = new RemoveCustomerCommand(Guid.NewGuid());

        //Act

        var result = await _handler.Handle(command, default);

        //Asert

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

