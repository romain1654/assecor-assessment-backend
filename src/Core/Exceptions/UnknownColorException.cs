using Core.Enums;

namespace Core.Exceptions;

public sealed class UnknownColorException : Exception
{
    public UnknownColorException(string color) : base(BuildErrorMessage(color)) { }

    private static string BuildErrorMessage(string color)
    {
        var availableColors = string.Join(", ", Enum.GetNames<Color>());
        
        return $"Unbekannte Farbe '{color}'. Folgende Farben sind verfügbar: {availableColors}";
    }
}
