using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Ef;

public class PersonDb : DbContext
{
    public DbSet<Person> Persons => Set<Person>();

    public PersonDb(DbContextOptions<PersonDb> options) : base(options) { }
}
