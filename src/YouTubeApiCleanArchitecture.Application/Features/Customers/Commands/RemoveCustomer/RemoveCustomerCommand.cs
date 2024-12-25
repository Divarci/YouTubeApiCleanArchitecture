
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;

namespace YouTubeApiCleanArchitecture.Application.Features.Customers.Commands.RemoveCustomer;
public record RemoveCustomerCommand(
    Guid CustomerId) : ICommand;
