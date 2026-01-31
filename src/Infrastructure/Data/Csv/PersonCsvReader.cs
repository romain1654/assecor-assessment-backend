using System.Text;
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
        var (zipCode, city) = SplitZipCity(parts[2]);

        return new Person
        {
            Id = Id,
            LastName = parts[0],
            Name = parts[1],
            ZipCode = zipCode,
            City = city,
            Color = int.Parse(parts[3])
        };
    }

    private (string zip, string city) SplitZipCity(string part)
    {
        var spaceIndex = part.Trim().IndexOf(' ');

        var zip = part.Substring(0, spaceIndex + 1).Trim();
        var city = part.Substring(spaceIndex + 1).Trim();

        return (zip, city);
    }

    private string CleanNumberInput(string? input)
    {
        if (string.IsNullOrEmpty(input)) { return string.Empty; }

        var cleanedInput = new StringBuilder(input.Length);

        foreach (var c in input)
        {
            if (char.IsNumber(c) || c == ' ') 
            { 
                cleanedInput.Append(c); 
            } 
        }  

        return cleanedInput.ToString().Trim();    
    }

    private string CleanStringInput(string? input)
    {
        if (string.IsNullOrEmpty(input)) { return string.Empty; }

        var cleanedInput = new StringBuilder(input.Length);

        foreach (var c in input)
        {
            if (char.IsLetter(c) || c == ' ') 
            { 
                cleanedInput.Append(c); 
            } 
        }

        return cleanedInput.ToString().Trim();
    }
}