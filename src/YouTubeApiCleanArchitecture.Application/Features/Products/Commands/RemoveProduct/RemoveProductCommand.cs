using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;

namespace YouTubeApiCleanArchitecture.Application.Features.Products.Commands.RemoveProduct;
public record RemoveProductCommand(
    Guid ProductId) : ICommand;
