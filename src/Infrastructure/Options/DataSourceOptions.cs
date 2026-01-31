namespace Infrastructure.Options;

public sealed class DataSourceOptions
{
    public string Provider { get; init; } = "Csv";
    public CsvOptions Csv { get; init; } = new();
    public EfOptions Ef { get; init; } = new();
}