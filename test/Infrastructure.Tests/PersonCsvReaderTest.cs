using Core.Exceptions;
using Infrastructure.Data.Csv;
using Microsoft.Extensions.Logging.Abstractions;

public class PersonCsvReaderTests
{
    private NullLogger<PersonCsvReader> _logger; 

    public PersonCsvReaderTests()
    {
        _logger = NullLogger<PersonCsvReader>.Instance;
    }

    [Fact]
    public void ReadCsv_WhenFileDoesNotExist_ThrowsDataSourceUnavailableException()
    {
        var path =  "non-existing-file.csv";
        var reader = new PersonCsvReader(_logger, path);

        Assert.Throws<DataSourceUnavailableException>(() => reader.ReadCsv());
    }

    [Fact]
    public void ReadCsv_WhenAllLinesValid_ParsesPeopleCorrectly()
    {
        var tempFile = Path.GetTempFileName();

        try
        {
            File.WriteAllLines(tempFile, new[]
            {
                "Mustermann,Max,67742 Lauterecken,3",
                "Millenium, Milly, 10115!- made up too*, 4"
            });

            var reader = new PersonCsvReader(_logger, tempFile);

            var people = reader.ReadCsv();

            Assert.Equal(2, people.Count);

            Assert.Equal(1, people[0].Id);
            Assert.Equal("Mustermann", people[0].LastName);
            Assert.Equal("Max", people[0].Name);
            Assert.Equal("67742", people[0].ZipCode);
            Assert.Equal("Lauterecken", people[0].City);
            Assert.Equal(3, people[0].Color);

            Assert.Equal(2, people[1].Id);
            Assert.Equal("Millenium", people[1].LastName);
            Assert.Equal("Milly", people[1].Name);
            Assert.Equal("10115", people[1].ZipCode);
            Assert.Equal("made up too", people[1].City);
            Assert.Equal(4, people[1].Color);
        }

        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void ReadCsv_WhenOneLineIsMalformed_SkipsThatLine()
    {
        var tempFile = Path.GetTempFileName();

        try
        {
            File.WriteAllLines(tempFile, new[]
            {
                "Mustermann,Max,67742 Lauterecken,3",
                "This,line,has,too,Many,Columns",
                "Too,Few",
                "Millenium, Milly, 10115!- made up too*, 4"
            });

            var reader = new PersonCsvReader(_logger, tempFile);

            var people = reader.ReadCsv();

            Assert.Equal(2, people.Count);
            Assert.Equal("Mustermann", people[0].LastName);
            Assert.Equal("Millenium", people[1].LastName);
        }

        finally
        {
            File.Delete(tempFile);
        }
    }
}
