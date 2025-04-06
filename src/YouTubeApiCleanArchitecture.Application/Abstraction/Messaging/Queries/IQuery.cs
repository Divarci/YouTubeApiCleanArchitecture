using MediatR;
using YouTubeApiCleanArchitecture.Domain.Abstraction.ResultPattern;

namespace YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    where TResponse : IResult;