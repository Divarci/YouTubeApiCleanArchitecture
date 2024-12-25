using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
using YouTubeApiCleanArchitecture.Domain.Entities.Products.DTOs;

namespace YouTubeApiCleanArchitecture.Application.Features.Products.Commands.CreateProduct;
public record CreateProductCommand(
    CreateProductDto Dto) : ICommand<ProductResponse>;