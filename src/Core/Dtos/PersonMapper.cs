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

    private static string GetColorStringFromInt(int colorInt)
    {
        var color = Enum.IsDefined(typeof(Color), colorInt) ? (Color)colorInt : Color.unbekannt; 
        return color.ToString();      
    }
}