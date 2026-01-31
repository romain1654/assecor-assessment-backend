using Core.Entities;

namespace Infrastructure.Data.Csv;

public class PersonCsvReader
{
    private readonly string _csvPath;

    public PersonCsvReader(string path)
    {
        _csvPath = path;
    }

    public List<Person> ReadCsv()
    {
        var people = new List<Person>();
        var currentRow = 0;

        foreach (var row in File.ReadLines(_csvPath))
        {
            currentRow++;

            var parts = row.Split(',');
            if (parts.Length != 4) 
            {
                continue; 
            }

            var newPerson = GetPersonFromSplitLine(parts, currentRow);
            people.Add(newPerson);
        }

        return people;
    }

    private Person GetPersonFromSplitLine(string[] parts, int Id)
    {
        return new Person();
    }
}