using Core.Dtos;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Endpoints;

public static class PersonEndpoints
{
    public static IEndpointRouteBuilder MapPersonEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/persons");

        group.MapGet("/", async Task<Ok<List<PersonReadDto>>> (IPersonService svc, CancellationToken token) => 
        {
            var allPeople = await svc.GetAllPeopleAsync(token);

            return TypedResults.Ok(allPeople);
        });

        group.MapGet("/{id:int}", async Task<IResult> (int id, IPersonService svc, CancellationToken token) => 
        {
            var foundPerson = await svc.GetPersonByIdAsync(id, token);

            return foundPerson is null ? TypedResults.NotFound() : TypedResults.Ok(foundPerson);
        });

        group.MapGet("/color/{color}", async Task<IResult> (string color, IPersonService svc, CancellationToken token) => 
        {
            try
            {
                var foundPeople = await svc.GetPeopleByColorAsync(color, token);

                return TypedResults.Ok(foundPeople);
            }
            
            catch (UnknownColorException ex)
            {
                return TypedResults.BadRequest(new 
                { 
                    error = ex.Message 
                });
            }
        });     
        return group;
    }
}