using Core.Dtos;
using Core.Enums;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Core.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _repo;
    private readonly ILogger<PersonService> _logger;

    public PersonService(ILogger<PersonService> logger, IPersonRepository repo)
    {
        _logger = logger;
        _repo = repo;
    }

    public async Task<List<PersonReadDto>> GetAllPeopleAsync(CancellationToken token)
    {
        var persons = await _repo.GetAllPeopleAsync(token);

        return persons.Select(p => p.ToDto()).ToList();
    }

    public Task<List<PersonReadDto>?> GetPeopleByColorAsync(string color, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<PersonReadDto?> GetPersonByIdAsync(int Id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}