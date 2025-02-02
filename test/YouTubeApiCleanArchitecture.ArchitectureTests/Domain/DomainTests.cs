using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using YouTubeApiCleanArchitecture.ArchitectureTests.Infrastructure;
using YouTubeApiCleanArchitecture.Domain.Abstraction.DomainEvents;
using YouTubeApiCleanArchitecture.Domain.Abstraction.Entity;

namespace YouTubeApiCleanArchitecture.ArchitectureTests.Domain;
public class DomainTests : BaseTest
{
    [Fact]
    public void DomainEvents_Should_BeSealed()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
    [Fact]
    public void DomainEvents_ShouldHave_DomainEventPostfix()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IDomainEvent))
            .Should()
            .HaveNameEndingWith("DomainEvent")
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Entities_ShouldHave_PrivateParameterlessContstructor()
    {
        IEnumerable<Type> etityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(BaseEntity))
            .GetTypes();

        var failingTypes = new List<Type>();

        foreach (var etityType in etityTypes)
        {
            ConstructorInfo[] constructors = etityType
                .GetConstructors(
                    BindingFlags.NonPublic |
                    BindingFlags.Instance);

            if (!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
                failingTypes.Add(etityType);
        }

        failingTypes.Should().BeEmpty();
    }

}
