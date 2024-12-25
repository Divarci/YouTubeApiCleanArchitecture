using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.CreateCustomer;
public record CreateCustomerCommand(
    CreateCustomerDto Dto) : ICommand<CustomerResponse>;
