using System.Reflection;
using Application;
using Domain;
using Infrastructure;
using NetArchTest.Rules;

namespace ArchitectureTests;

public class ArchitectureTests
{
    [Fact]
    public void DomainLayerCantDependOnAnyOtherLayer()
    {
        var result = Types.InAssembly(typeof(Chicken).Assembly)
            .Should()
            .NotHaveDependencyOn("Application")
            .And().NotHaveDependencyOn("Infrastructure")
            .GetResult();
        
        Assert.True(result.IsSuccessful, String.Join(",", result.FailingTypes));
    }
    
    [Fact]
    public void ApplicationLayerCantDependOnInfrastructureLayer()
    {
        var result = Types.InAssembly(typeof(EggHandler).Assembly)
            .ShouldNot()
            .HaveDependencyOn("Infrastructure")
            .GetResult();
        
        Assert.True(result.IsSuccessful, String.Join(",", result.FailingTypes));
    }
    
    [Fact]
    public void HandlersShouldOnlyResideInApplicationLayer()
    {
        var result = Types.InAssemblies(_getAssembliesUnderTest())
            .That().HaveNameEndingWith("Handler")
            .Should().ResideInNamespaceMatching("Application")
            .GetResult();
        
        Assert.True(result.IsSuccessful, String.Join(",", result.FailingTypes));
    }
    
    [Fact]
    public void CommandsShouldOnlyResideInApplicationLayer()
    {
        var result = Types.InAssemblies(_getAssembliesUnderTest())
            .That().HaveNameEndingWith("Commands")
            .Should().ResideInNamespaceMatching("Application")
            .GetResult();
        
        Assert.True(result.IsSuccessful, String.Join(",", result.FailingTypes));
    }
    
    [Fact]
    public void ControllersShouldOnlyResideInInfrastructureLayer()
    {
        var result = Types.InAssemblies(_getAssembliesUnderTest())
            .That().HaveNameEndingWith("Controller")
            .Should().ResideInNamespaceMatching("Infrastructure")
            .GetResult();
        
        Assert.True(result.IsSuccessful, String.Join(",", result.FailingTypes));
    }
    
    [Fact]
    public void ControllersShouldInheritBaseController()
    {
        var result = Types.InAssemblies(_getAssembliesUnderTest())
            .That().HaveNameEndingWith("Controller")
            .And().DoNotHaveNameStartingWith("Base")
            .Should().Inherit(typeof(BaseController))
            .GetResult();
        
        Assert.True(result.IsSuccessful, String.Join(",", result.FailingTypes));
    }

    private IEnumerable<Assembly> _getAssembliesUnderTest()
    {
        return new[]
        {
            typeof(Chicken).Assembly,
            typeof(EggHandler).Assembly,
            typeof(BaseController).Assembly
        };
    }
}