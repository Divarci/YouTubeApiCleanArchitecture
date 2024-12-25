using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Entities.Products.DTOs;

namespace YouTubeApiCleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
public record UpdateProductCommand(
    UpdateProductDto Dto) : ICommand;
