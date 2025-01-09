using FluentAssertions;
using NSubstitute;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.Events;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.DTOs;
using YouTubeApiCleanArchitecture.Domain.Entities.Invoices.Events;
using YouTubeApiCleanArchitecture.Domain.Entities.Products;
using YouTubeApiCleanArchitecture.Domain.Exceptions;
using YouTubeApiCleanArchitecture.Domain.UnitTests.Products;

namespace YouTubeApiCleanArchitecture.Domain.UnitTests.Invoices;
public class Invoicetest
{
    private readonly IUnitOfWork _unitOfWork;

    public Invoicetest()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();

        var productRepository = Substitute.For<IGenericRepository<Product>>();

        _unitOfWork
            .Repository<Product>()
            .Returns(productRepository);

        var catalogue = ProductData.Catalogue;

        foreach (var product in catalogue)
            _unitOfWork
                .Repository<Product>()
                .GetByIdAsync(product.Id)
                .Returns(product);
    }

    [Fact]
    public async Task Create_ShouldReturn_Success()
    {
        //Act

        var invoice = await InvoiceData.CreateInvoice(
            InvoiceData.Customer.Id,
            InvoiceData.InvoiceId,
            InvoiceData.ValidPurchasedProducts,
        _unitOfWork);

        var @event = invoice.GetDomainEvents()[0] as InvoiceCreatedDomainEvent;

        //Assert

        invoice.Should()
            .NotBeNull();

        invoice.PoNumber.Value.Should()
            .Be(InvoiceData.PoNumber);

        invoice.CustomerId.Should()
            .Be(InvoiceData.Customer.Id);

        invoice.PurchasedProducts.Count.Should()
            .Be(InvoiceData.ValidPurchasedProducts.Count);

        foreach (var product in invoice.PurchasedProducts)
        {
            product.Quantity.Should()
                .NotBeNull();
            product.Quantity.Value.Should()
                .BeGreaterThan(0);
        }

        invoice.TotalBalance.Value.Should()
            .BeGreaterThan(0);

        invoice.GetDomainEvents().Should()
        .HaveCount(1);

        @event.Should()
            .NotBeNull();

        @event.Should()
            .BeAssignableTo<InvoiceCreatedDomainEvent>();

        @event!.InvoiceId.Should()
            .Be(InvoiceData.InvoiceId);
    }

    [Fact]
    public async Task Create_WithEmptyCustomerId_ShouldReturn_Failed()
    {
        //Act

        Func<Task> act = async () => await InvoiceData.CreateInvoice(
            Guid.Empty,
            InvoiceData.InvoiceId,
            InvoiceData.ValidPurchasedProducts,
            _unitOfWork);

        //Assert

        await act.Should()
            .ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Create_WithNullProductList_ShouldReturn_Failed()
    {
        //Act

        Func<Task> act = async () => await InvoiceData.CreateInvoice(
            InvoiceData.Customer.Id,
            InvoiceData.InvoiceId,
            null!,
            _unitOfWork);

        //Assert

        await act.Should()
            .ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Create_WithEmptyProductList_ShouldReturn_Failed()
    {
        //Act

        Func<Task> act = async () => await InvoiceData.CreateInvoice(
            InvoiceData.Customer.Id,
            InvoiceData.InvoiceId,
            [],
            _unitOfWork);

        //Assert

        await act.Should()
            .ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Create_WithInvalidIdProductList_ShouldReturn_Failed()
    {
        //Act

        Func<Task> act = async () => await InvoiceData.CreateInvoice(
            InvoiceData.Customer.Id,
            InvoiceData.InvoiceId,
            InvoiceData.PurchasedProductsWithInvalidId,
            _unitOfWork);

        //Assert

        await act.Should()
            .ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Create_WithInvalidQuantityProductList_ShouldReturn_Failed()
    {
        //Act

        Func<Task> act = async () => await InvoiceData.CreateInvoice(
            InvoiceData.Customer.Id,
            InvoiceData.InvoiceId,
            InvoiceData.PurchasedProductsWithInvalidQuantity,
            _unitOfWork);

        //Assert

        await act.Should()
            .ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task Update_ShouldReturn_Success()
    {
        //Arrange

        var updateto = new UpdateInvoiceDto
        {
            PoNumber = "PO45678"
        };

        //Act

        var invoice = await InvoiceData.CreateInvoice(
            InvoiceData.Customer.Id,
            InvoiceData.InvoiceId,
            InvoiceData.ValidPurchasedProducts,
            _unitOfWork);

        invoice.Update(updateto);

        //Assert

        invoice.PoNumber.Value.Should()
            .Be(updateto.PoNumber);
    }
}
