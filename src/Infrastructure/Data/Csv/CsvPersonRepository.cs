using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data.Csv;

public class CsvPersonRepository : IPersonRepository
{
    private readonly PersonCsvReader _reader; 

    public CsvPersonRepository(PersonCsvReader reader)
    {
        _reader = reader;
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