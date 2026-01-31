using System.Text;
using Core.Entities;
using Core.Exceptions;
using Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Infrastructure.Data.Csv;

public class PersonCsvReader 
{
    private readonly string _csvPath;
    private readonly ILogger<PersonCsvReader> _logger;

    public PersonCsvReader(ILogger<PersonCsvReader> logger, IOptions<CsvOptions> options)
    {
        _logger = logger;
        _csvPath = options.Value.FilePath;
    }

    public List<Person> ReadCsv()
    {
        if (!File.Exists(_csvPath))
        {
            throw new DataSourceUnavailableException(_csvPath);
        }
        
        var people = new List<Person>();
        var currentRow = 0;

        var skippedLines = new List<int>();

        foreach (var row in File.ReadLines(_csvPath))
        {
            currentRow++;

            var parts = row.Split(',');
            if (parts.Length != 4) 
            {
                skippedLines.Add(currentRow); 
                continue; 
            }

            var newPerson = GetPersonFromSplitLine(parts, currentRow);
            people.Add(newPerson);
        }

        if (skippedLines.Count > 0)
        {
            _logger.LogInformation("Die folgenden CSV Zeilen konnten aufgrund eines ungültigen Formats nicht verarbeitet werden: {values}",string.Join(", ", skippedLines));
        }

        return people;
    }

    private Person GetPersonFromSplitLine(string[] parts, int Id)
    {
        var (zipCode, city) = SplitZipCity(parts[2]);

        if (!int.TryParse(parts[3], out int color))
        {
            _logger.LogInformation("Ungültiger Farbe '{parts[3]}' in CSV-Zeile {number}. Farbe auf 'unbekannt' gesetzt", parts[3], Id);
        }

        return new Person
        {
            Id = Id,
            LastName = CleanTextInput(parts[0]),
            Name = CleanTextInput(parts[1]),
            ZipCode = CleanNumberInput(zipCode),
            City = CleanTextInput(city),
            Color = color
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
            if (char.IsDigit(c) || c == ' ') 
            { 
                cleanedInput.Append(c); 
            } 
        }  

        return cleanedInput.ToString().Trim();    
    }

    private string CleanTextInput(string? input)
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