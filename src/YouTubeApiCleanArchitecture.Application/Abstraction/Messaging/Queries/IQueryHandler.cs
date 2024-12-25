using MediatR;
using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Queries;
public interface IQueryHandler<TQuery ,TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse: IResult;