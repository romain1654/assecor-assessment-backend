using Core.Entities;
using Core.Enums;

namespace Core.Dtos;

public static class PersonMapper
{
    public static PersonReadDto ToDto(this Person entity)
    {
        var color = GetColorStringFromInt(entity.Color);

        return new PersonReadDto(entity.Id, entity.Name, entity.LastName, entity.ZipCode, entity.City, color.ToString());
    }

    public static Person ToEntity(this PersonCreateDto dto)
    {
        var color = GetIntFromColorString(dto.Color);

        return new Person
        {
            Name = dto.Name,
            LastName = dto.LastName,
            ZipCode = dto.ZipCode,
            City = dto.City,
            Color = color
        };
    }
    
    private static int GetIntFromColorString(string colorString)
    {
        if (Enum.TryParse<Color>(colorString.ToLower(), out var colorEnum))
        {
            return (int)colorEnum;
        }
        else
        {
            return (int)Color.unbekannt;
        }
    }

    private static string GetColorStringFromInt(int colorInt)
    {
        var color = Enum.IsDefined(typeof(Color), colorInt) ? (Color)colorInt : Color.unbekannt; 
        return color.ToString();      
    }
}