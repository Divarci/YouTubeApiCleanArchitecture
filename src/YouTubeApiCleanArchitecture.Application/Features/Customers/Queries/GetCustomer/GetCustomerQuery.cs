using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Queries.GetCustomer;
public record GetCustomerQuery(
    Guid CustomerId) : IQuery<CustomerResponse>;
