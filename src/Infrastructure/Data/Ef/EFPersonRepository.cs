using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data.Ef;

public class EfPersonRepository : IPersonRepository
{
    private readonly PersonDb _db;

    public EfPersonRepository(PersonDb db)
    {
        _db = db;
    }

    public Task<List<Person>> GetAllPeopleAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<List<Person>> GetPeopleByColorAsync(int colourNum, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Person?> GetPersonByIdAsync(int id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}