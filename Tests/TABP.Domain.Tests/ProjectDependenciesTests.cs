using System.Reflection;
using NetArchTest.Rules;
using FluentAssertions;

namespace TABP.Domain.Tests.Architecture;

public class ProjectDependenciesTests
{

    private const string DomainNamespace = "TABP.Domain";
    private const string InfrastructureNamespace = "TABP.Infrastructure";
    private const string ApplicationNamespace = "TABP.Application";
    private const string APINamespace = "TABP.API";
    private const string EFCoreNamespace = "Microsoft.EntityFrameworkCore";

    [Fact]
    public void Domain_ShouldNotHaveDependenciesoN_ExternalAssemblies()
    {
        // Arrange
        var domain = Assembly.Load(DomainNamespace);

        var externalAssemblies = new[] {InfrastructureNamespace, APINamespace};

        // Act
        var result = Types
            .InAssembly(domain)
            .ShouldNot()
            .HaveDependencyOnAll(externalAssemblies)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_ShouldNotDependOn_ApplicationOrAPI()
    {
        // Arrange
        var infrastructureAssembly = Assembly.Load(InfrastructureNamespace);
        
        var disallowedNamespaces = new[] { ApplicationNamespace, APINamespace };

        // Act
        var failingTypes = Types
            .InAssembly(infrastructureAssembly)
            .That()
            .HaveDependencyOnAny(disallowedNamespaces)
            .GetTypes();

        // Assert
        failingTypes.Should().BeEmpty(
            $"Infrastructure layer should not depend on {string.Join(" or ", disallowedNamespaces)}, " +
            $"but the following types do: {FormatTypes(failingTypes)}");
    }

    private static string FormatTypes(IEnumerable<Type> types) // very good helper method. check later.
    {
        return types.Any() 
            ? string.Join(", ", types.Select(t => t.FullName)) 
            : "none";
    }

    [Fact]
    public void Application_ShouldDependOnlyOn_Domain()
    {
        // Arrange
        var applicationAssembly = Assembly.Load(ApplicationNamespace);

        var unallowedNamespaces = new[] { InfrastructureNamespace, APINamespace }; 

        // Act
        var result = Types
            .InAssembly(applicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(unallowedNamespaces)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Domain_ShouldNotDependOn_EFCore()
    {
        // Arrange
        var domainAssembly = Assembly.Load(DomainNamespace);
        var efCoreAssembly = new[] { EFCoreNamespace };

        // Act
        var result = Types
            .InAssembly(domainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(efCoreAssembly)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_ShouldNotHaveDependencyOn_ExternalAssemblies()
    {
        // Arrange
        var domain = Assembly.Load(InfrastructureNamespace);

        // Act
        var result = Types
            .InAssembly(domain)
            .ShouldNot()
            .HaveDependencyOn(APINamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}