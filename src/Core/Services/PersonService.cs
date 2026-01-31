using Core.Dtos;
using Core.Enums;
using Core.Exceptions;
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

    public async Task CreatePersonAsync(PersonCreateDto person, CancellationToken token)
    {
        var personColor = person.Color.ToLower();

        if (!Enum.TryParse<Color>(personColor, out _))
        {
            _logger.LogInformation("Farbe {color} ist unbekannt.", personColor);
            
            throw new UnknownColorException(personColor);
        }

       await _repo.AddPersonAsync(person.ToEntity(), token);
    }

    public async Task<List<PersonReadDto>> GetAllPeopleAsync(CancellationToken token)
    {
        var persons = await _repo.GetAllPeopleAsync(token);

        return persons.Select(p => p.ToDto()).ToList();
    }

    public async Task<List<PersonReadDto>?> GetPeopleByColorAsync(string color, CancellationToken token)
    {
        if (!Enum.TryParse<Color>(color.ToLower(), out var colorVal))
        {
            _logger.LogInformation("Farbe {color} ist unbekannt.", color);
            
            throw new UnknownColorException(color);
        }

        var persons = await _repo.GetPeopleByColorAsync((int)colorVal, token);

        return persons.Select(p => p.ToDto()).ToList();
    }

    public async Task<PersonReadDto?> GetPersonByIdAsync(int Id, CancellationToken token)
    {
        var person = await _repo.GetPersonByIdAsync(Id, token);

        return person?.ToDto();
    }
}