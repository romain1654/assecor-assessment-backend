using Core.Interfaces;
using Moq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Core.Dtos;

namespace API.Tests;

public class EndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<IPersonService> _svc;
    private readonly HttpClient _client;

    public EndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _svc = new Mock<IPersonService>();
        _client = CreateClientWithService();       
    }

    private HttpClient CreateClientWithService()
    {
        return _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IPersonService>();
                services.AddSingleton(_svc.Object);
            });
        }).CreateClient();
    }

    private List<PersonReadDto> GetPersons() => new List<PersonReadDto>
    {
        new(1, "Hans", "Müller", "67742", "Lauterecken", "blau"),
        new(3, "Romain", "Jean", "70190", "Stuttgart", "grün"),
        new(3, "Jonas", "Müller", "32323", "Hansstadt", "blau")
    };
}