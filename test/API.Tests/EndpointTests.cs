using Core.Interfaces;
using Moq;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Core.Dtos;
using System.Net;
using System.Net.Http.Json;

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

    #region GetAllPeopleAsync

    [Fact]
    public async Task GetAllPeopleAsync_WhenServiceReturnsList_ReturnsOkWithList()
    {
        var persons = GetPersons();
                        
        _svc.Setup(s => s.GetAllPeopleAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(persons);

        var response = await _client.GetAsync("/persons");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<List<PersonReadDto>>();
        Assert.NotNull(result);
        Assert.Equal(3, result!.Count);
        Assert.Contains(result, p => p.Name == "Hans");
        Assert.Contains(result, p => p.City == "Stuttgart");   

        _svc.Verify(s => s.GetAllPeopleAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetAllPeopleAsync_WhenNoPeople_ReturnsOkWithEmptyList()
    {                        
        _svc.Setup(s => s.GetAllPeopleAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<PersonReadDto>());

        var response = await _client.GetAsync("/persons");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<List<PersonReadDto>>();
        Assert.NotNull(result);
        Assert.Empty(result);

        _svc.Verify(s => s.GetAllPeopleAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region GetPersonByIdAsync

    [Fact]
    public async Task GetPersonByIdAsync_IdFound_ReturnsRelevantPerson()
    {        
        var persons = GetPersons();
        var targetPerson = persons[1];
        var id = targetPerson.Id;
        
        _svc.Setup(svc => svc.GetPersonByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(targetPerson);

        var response = await _client.GetAsync($"/persons/{id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<PersonReadDto>();
        Assert.NotNull(result);
        Assert.Equal(result.Id, id);
        Assert.Equal(result.Name, targetPerson.Name);
        Assert.Equal(result.ZipCode, targetPerson.ZipCode);

        _svc.Verify(s => s.GetPersonByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetPersonByIdAsync_IdNotExisting_ReturnsNotFound()
    {        
        var id = 2;   

        _svc.Setup(svc => svc.GetPersonByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((PersonReadDto?)null);

        var response = await _client.GetAsync($"/persons/{id}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        _svc.Verify(s => s.GetPersonByIdAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

}