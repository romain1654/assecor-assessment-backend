namespace Core.Exceptions;

public sealed class DataSourceUnavailableException : Exception
{
    public DataSourceUnavailableException(string dataSource) : base(BuildErrorMessage(dataSource)) { }

    private static string BuildErrorMessage(string dataSource)
    {   
        return $"Die folgende Datenquelle wurde nicht gefunden: '{dataSource}'. Bitte überprüfen Sie die Konfiguration oder den angegebenen Pfad";
    }
}