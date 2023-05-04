using AutoMapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace TaskMaster.Tests.Unit.Api;

public class StartupTests
{
    [Fact]
    public async Task AutoMapperConfiguration_ShouldBeValid()
    {
        // Arrange
        await using var application = new AutoMapperAppWebApplication();
        var mapper = application.Services.GetRequiredService<IMapper>();
        mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }
}

internal class AutoMapperAppWebApplication : WebApplicationFactory<Program>{}