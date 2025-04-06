using MediatR;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;

namespace YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
public interface ICommand : IRequest<Result<NoContentDto>>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    where TResponse : IResult;


