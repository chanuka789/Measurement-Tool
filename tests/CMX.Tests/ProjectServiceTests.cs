using CMX.Application.Services;
using Xunit;

namespace CMX.Tests;

public class ProjectServiceTests
{
    [Fact]
    public void Create_AddsProjectToList()
    {
        var service = new ProjectService();
        var project = service.Create("Test");
        Assert.Single(service.GetAll());
        Assert.Equal("Test", project.Name);
    }
}
