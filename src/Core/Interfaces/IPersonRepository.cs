using Core.Entities;

namespace Core.Interfaces;

public interface IPersonRepository
{
    Task<Person?> GetPersonByIdAsync(int id, CancellationToken token);
    Task<List<Person>> GetPeopleByColorAsync(int colourNum, CancellationToken token);
    Task<List<Person>> GetAllPeopleAsync(CancellationToken token);
}