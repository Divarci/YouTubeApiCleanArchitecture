using Moq;
using System.Threading.Tasks;
using YouTubeApiCleanArchitecture.Domain.Abstraction;
using YouTubeApiCleanArchitecture.Domain.Entities.Products;
using YouTubeApiCleanArchitecture.DomainTests.Data.Customers;
using YouTubeApiCleanArchitecture.DomainTests.Data.Products;
using YouTubeApiCleanArchitecture.DomainTests.Test.Shared;

namespace YouTubeApiCleanArchitecture.DomainTests.Test.Customers;
public class Domain_Customer_RemoveInvoice: BaseTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IGenericRepository<Product>> _productRepositoryMock;

    public Domain_Customer_RemoveInvoice()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _productRepositoryMock = new Mock<IGenericRepository<Product>>();
    }

    [Fact]
    public async Task Customer_InvoiceRemove_ShouldRemoveInvoice_Successfully()
    {
        // Arrange

        var product = ProductData.CreateProduct();

        _productRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        _unitOfWorkMock
            .Setup(x => x.Repository<Product>())
            .Returns(_productRepositoryMock.Object);

        var customer = await CustomerData
            .CreateCustomer()
            .AddInvoice(_unitOfWorkMock.Object);

        var invoiceToBeDeleted = customer.Invoices.First();

        var previousInvoiceCount = customer.Invoices.Count;

        // Act

        customer.RemoveInvoice(invoiceToBeDeleted);

        var currenInvoiceIdList = customer.Invoices.Select(x=>x.Id).ToList();   
        // Assert

        Assert.Multiple(() =>
        {            
            Assert.Equal(customer.Invoices.Count, previousInvoiceCount - 1);
            Assert.DoesNotContain(invoiceToBeDeleted.Id, currenInvoiceIdList);
        });
    }
}
