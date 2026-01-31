using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data.Csv;

public class CsvPersonRepository : IPersonRepository
{
    private readonly PersonCsvReader _reader; 

    private List<Person>? _people;

    private List<Person> People
    {
        get
        {
            if (_people != null) 
            { 
                return _people;
            }

            _people = _reader.ReadCsv();   
             
            return _people;        
        }
    }

    public CsvPersonRepository(PersonCsvReader reader)
    {
        _reader = reader;
    }

    public Task<List<Person>> GetAllPeopleAsync(CancellationToken token)
    {
        return Task.FromResult(People);
    }

    public Task<List<Person>> GetPeopleByColorAsync(int colourNum, CancellationToken token)
    {
        var peopleByColour = People.Where(p => p.Color == colourNum).OrderBy(p => p.Id).ToList();

        return Task.FromResult(peopleByColour);
    }

    public Task<Person?> GetPersonByIdAsync(int id, CancellationToken token)
    {
        var person = People.FirstOrDefault(p => p.Id == id);

        return Task.FromResult(person);
    }
}