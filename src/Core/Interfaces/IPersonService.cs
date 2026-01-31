using Core.Dtos;

namespace Core.Interfaces;

public interface IPersonService
{
    Task<List<PersonReadDto>> GetAllPeopleAsync(CancellationToken token);
    Task<PersonReadDto?> GetPersonByIdAsync(int Id, CancellationToken token);
    Task<List<PersonReadDto>?> GetPeopleByColorAsync(string color, CancellationToken token);
    Task CreatePersonAsync(PersonCreateDto person, CancellationToken token);
}