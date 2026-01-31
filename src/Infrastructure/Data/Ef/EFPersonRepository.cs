using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Ef;

public class EfPersonRepository : IPersonRepository
{
    private readonly PersonDb _db;

    public EfPersonRepository(PersonDb db)
    {
        _db = db;
    }

    public async Task<List<Person>> GetAllPeopleAsync(CancellationToken token)
    {
        return await _db.Persons
            .AsNoTracking()
            .ToListAsync(token);
    }

    public async Task<List<Person>> GetPeopleByColorAsync(int colourNum, CancellationToken token)
    {
        return await _db.Persons
            .AsNoTracking()
            .Where(p => p.Color == colourNum)
            .OrderBy(p => p.Id)
            .ToListAsync(token);
    }

    public async Task<Person?> GetPersonByIdAsync(int id, CancellationToken token)
    {
        return await _db.Persons.FindAsync(id, token);
    }
}