using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Entities.Customers.DTOs;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.UpdateCustomer;
public record UpdateCustomerCommand(
    Guid CustomerId,
    UpdateCustomerDto Dto) : ICommand;