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
}
