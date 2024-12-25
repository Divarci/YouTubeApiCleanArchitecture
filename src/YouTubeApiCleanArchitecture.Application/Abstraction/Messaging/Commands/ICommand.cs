using MediatR;
using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;
public interface ICommand : IRequest<Result<NoContentDto>>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    where TResponse : IResult;

public interface IBaseCommand;


