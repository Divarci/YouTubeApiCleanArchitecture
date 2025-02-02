using System.Reflection;
using YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;
using YouTubeApiCleanArchitecture.Infrastructure;
using YouTubeApiCleanArchitecture.Application.Abstraction.Messaging.Commands;

namespace YouTubeApiCleanArchitecture.ArchitectureTests.Infrastructure;
public abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(BaseEntity).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(ICommand).Assembly;
    protected static readonly Assembly InfrastuctureAssembly = typeof(AppDbContext).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(Program).Assembly;
}
